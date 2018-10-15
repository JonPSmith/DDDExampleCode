// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataLayer.Dtos;
using DataLayer.EfClasses.CrUDOnly;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ServiceLayer.DatabaseCode.Services
{
    public enum DbStartupModes { UseExisting, EnsureCreated, EnsureDeletedCreated, UseMigrations}

    public static class SetupHelpers
    {
        public const string DefaultUserId = "me@GenericLibs.com";

        public const string SeedDataSearchName = "Apress books*.json";
        public const string SeedFileSubDirectory = "seedData";
        private const decimal DefaultBookPrice = 40;    //Any book without a price is set to this value

        //------------------------------------------------------
        //private methods


        private static readonly string[] DummyUsersIds = new[]
        {
            DefaultUserId,
            "albert@einstein.com",
            "ada@lovelace.co.uk"
        };

        public static void DevelopmentEnsureDeleted(this CrUDOnlyDbContext db)
        {
            db.Database.EnsureDeleted();
        }

        public static void DevelopmentEnsureCreated(this CrUDOnlyDbContext db)
        {
            db.Database.EnsureCreated();
        }

        public static int SeedDatabase(this CrUDOnlyDbContext context, string dataDirectory)
        {
            if (!(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                throw new InvalidOperationException("The database does not exist. If you are using Migrations then run PMC command update-database to create it");

            var numBooks = context.Books.Count();
            if (numBooks == 0)
            {
                //the database is emply so we fill it from a json file
                var books = BookJsonLoader.LoadBooks(Path.Combine(dataDirectory, SeedFileSubDirectory),
                    SeedDataSearchName).ToList();
                context.Books.AddRange(books);
                context.SaveChanges();
                numBooks = books.Count + 1;

                context.ResetOrders(books);
            }

            return numBooks;
        }

        /// <summary>
        /// This wipes all the existing orders and creates a new set of orders
        /// </summary>
        /// <param name="context"></param>
        /// <param name="books"></param>
        public static void ResetOrders(this CrUDOnlyDbContext context, List<Book> books = null)
        {
            context.RemoveRange(context.Orders.ToList());        //remove the existing orders (the lineitems will go too)
            context.AddDummyOrders(books);              //add a dummy set of orders
            context.SaveChanges();
        }

        private static void AddDummyOrders(this CrUDOnlyDbContext context, List<Book> books = null)
        {
            if (books == null)
                books = context.Books.ToList();

            var orders = new List<Order>();
            var i = 0;
            foreach (var usersId in DummyUsersIds)
            {
                orders.Add(BuildDummyOrder(usersId, DateTime.UtcNow.AddDays(-10), books[i++]));
                orders.Add(BuildDummyOrder(usersId, DateTime.UtcNow, books[i++]));
            }
            context.AddRange(orders);
        }

        private static Order BuildDummyOrder(string userId, DateTime orderDate, Book bookOrdered)
        {
            var bookOrders = new List<OrderBooksDto>() {new OrderBooksDto(1, bookOrdered, 1)};
            return Order.CreateOrder(userId, bookOrders)?.Result;
        }
    }
}