// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Linq;
using DataLayer;
using DataLayer.EfClasses.PocoOnly;
using DataLayer.EfClasses.PocoOnly.Support;
using DataLayer.EfCode;
using DummyServices;
using GenericServices;
using Microsoft.EntityFrameworkCore;

namespace PocoRepository
{
    public class Repository
    {
        private readonly PocoOnlyDbContext _context;
        private readonly IMailService _mailService;

        public Repository(PocoOnlyDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        public bool AddReview(int id, int numStars, string comment, string voterName)
        {
            var book = _context.Books.Include(x => x.Reviews)
                .SingleOrDefault(x => x.Id == id);
            if (book == null)
                return false;

            book.AddReview(numStars, comment, voterName);
            _context.SaveChanges();
            return true;
        }

        public IStatusGeneric PlaceOrder(PlaceOrderDto dto)
        {
            var bookOrders = dto.LineItems.Select(x => 
                new OrderBooksDto(x.BookId, _context.Find<Book>(x.BookId), x.NumBooks));
            var user = _context.Find<User>(dto.UserId);
            var orderStatus = Order.CreateOrder(user.UserName, bookOrders);
            if (!orderStatus.IsValid)
                return orderStatus;

            _context.Add(orderStatus.Result);
            _context.SaveChanges();
            _mailService.SendMail(user, "...");
            return orderStatus;
        }


    }
}