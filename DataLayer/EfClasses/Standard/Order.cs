// Copyright (c) 2017 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace DataLayer.EfClasses.Standard
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime DateOrderedUtc { get; set; }

        public string CustomerName { get; set; }

        // relationships

        public ICollection<LineItem> LineItems { get; set; }

        public Order()
        {
            DateOrderedUtc = DateTime.UtcNow;
        }
    }
}