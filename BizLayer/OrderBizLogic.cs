// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Linq;
using DataLayer;
using DataLayer.EfClasses.CrUDOnly;
using DataLayer.EfClasses.CrUDOnly.Support;
using DataLayer.EfCode;
using DummyServices;
using GenericBizRunner;
using IStatusGeneric = GenericServices.IStatusGeneric;

namespace BizLayer
{
    public class OrderBizLogic : BizActionStatus, IGenericActionInOnlyWriteDb<PlaceOrderDto>
    {
        private readonly CrUDOnlyDbContext _context;
        private readonly IMailService _mailService;

        public OrderBizLogic(CrUDOnlyDbContext context, IMailService mailService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        public void BizAction(PlaceOrderDto dto)
        {
            var bookOrders = dto.LineItems.Select(x =>
                new OrderBooksDto(x.BookId, _context.Find<Book>(x.BookId), x.NumBooks));
            var user = _context.Find<User>(dto.UserId);
            var orderStatus = Order.CreateOrderViaBizLogic(user.UserName, bookOrders);
            CombineErrors(orderStatus);
            if (orderStatus.HasErrors)
                return;

            _context.Add(orderStatus.Result);
            _context.SaveChanges();
            _mailService.SendMail(user, "...");
        }
    }
}