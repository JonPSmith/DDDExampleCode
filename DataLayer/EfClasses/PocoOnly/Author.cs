// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.EfClasses.PocoOnly.Support;

namespace DataLayer.EfClasses.PocoOnly
{

    public class Author : Entity
    {
        public const int NameLength = 100;
        public const int EmailLength = 100;

        public Author(string name, string email)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        private Author() { }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(NameLength)]
        public string Name { get; private set; }

        [MaxLength(EmailLength)]
        public string Email { get; private set; }

        //------------------------------
        //Relationships

        public ICollection<BookAuthor> BooksLink { get; private set; }
    }

}