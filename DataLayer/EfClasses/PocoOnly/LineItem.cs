// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.EfClasses.PocoOnly.Support;

namespace DataLayer.EfClasses.PocoOnly
{
    public class LineItem : Entity
    {
        internal LineItem(Order order, short numBooks, Book chosenBook, byte lineNum)
        {
            LinkedOrder = order ?? throw new ArgumentNullException(nameof(order));
            NumBooks = numBooks;
            ChosenBook = chosenBook ?? throw new ArgumentNullException(nameof(chosenBook));
            BookPrice = chosenBook.ActualPrice;
            LineNum = lineNum;
        }

        /// <summary>
        /// Used by EF Core
        /// </summary>
        private LineItem() { }

        [Range(1,5, ErrorMessage = "This order is over the limit of 5 books.")] 
        public byte LineNum { get; private set; }

        public short NumBooks { get; private set; }

        /// <summary>
        /// This holds a copy of the book price. We do this in case the price of the book changes,
        /// e.g. if the price was discounted in the future the order is still correct.
        /// </summary>
        public decimal BookPrice { get; private set; }

        // relationships

        public Order LinkedOrder { get; private set; }

        public Book ChosenBook { get; private set; }
    }

}