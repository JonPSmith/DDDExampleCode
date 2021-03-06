﻿// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.EfClasses.PocoOnly.Support;

namespace DataLayer.EfClasses.PocoOnly
{
    public class BookAuthor
    {
        private BookAuthor() { }

        internal BookAuthor(Book book, Author author, byte order)
        {
            Book = book;
            Author = author;
            Order = order;
        }

        public byte Order { get; private set; }

        //-----------------------------
        //Relationships

        public Book Book { get; private set; }
        public Author Author { get; private set; }
    }
}