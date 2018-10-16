// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.EfClasses.CrUDOnly.Support;
using GenericServices;

namespace DataLayer.EfClasses.CrUDOnly
{
    public class Order
    {
        private HashSet<LineItem> _lineItems;

        public int OrderId { get; private set; }

        public DateTime OrderedUtc { get; private set; }

        public OrderStatuses Status { get; private set; }

        public string CustomerName { get; private set; }

        // relationships

        public IEnumerable<LineItem> LineItems => _lineItems?.ToList();

        // ctors/factories

        private Order() { } //Needed by EF Core

        public static IStatusGeneric<Order> CreateOrder(string customerName,
            IEnumerable<OrderBooksDto> bookOrders)
        {
            var status = new StatusGenericHandler<Order>();
            var order = new Order
            {
                CustomerName = customerName,
                Status = OrderStatuses.Created,
                OrderedUtc = DateTime.UtcNow
            };

            byte lineNum = 1;
            order._lineItems = new HashSet<LineItem>(bookOrders
                .Select(x => new LineItem(x.numBooks, x.ChosenBook, lineNum++)));
            if (!order._lineItems.Any())
                status.AddError("No items in your basket.");

            return status.SetResult(order); //don't worry, the Result will return default(T) if there are errors
        }

        public void MarkOrderAsDispatched()
        {
            Status = OrderStatuses.Dispatched;
        }
    }
}