// Copyright (c) 2017 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.EfClasses.Standard;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EfClasses
{
    public class LineItem : IValidatableObject 
    {
        public int LineItemId { get; set; }

        [Range(1,5, ErrorMessage = "This order is over the limit of 5 books.")] 
        public byte LineNum { get; set; }

        public short NumBooks { get; set; }

        /// <summary>
        /// This holds a copy of the book price. We do this in case the price of the book changes,
        /// e.g. if the price was discounted in the future the order is still correct.
        /// </summary>
        public decimal BookPrice { get; set; }

        // relationships

        public int OrderId { get; set; }
        public int BookId { get; set; }

        public Book ChosenBook { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate 
            (ValidationContext validationContext)                 
        {
            var currContext = validationContext.GetService(typeof(DbContext));

            if (ChosenBook.Price < 0)                      
                yield return new ValidationResult($"Sorry, the book '{ChosenBook.Title}' is not for sale."); 

            if (NumBooks > 100)
                yield return new ValidationResult(
"If you want to order a 100 or more books"+       
" please phone us on 01234-5678-90",              
                    new[] { nameof(NumBooks) });  
        }
    }

}