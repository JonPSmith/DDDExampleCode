// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.EfClasses.PocoOnly;
using DataLayer.EfClasses.PocoOnly.Support;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests.TestPocoOnly
{
    public class TestCreateDatabase
    {
        private ITestOutputHelper _output;

        public TestCreateDatabase(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact]
        public void TestCreatePocoOnlyDatabaseOk()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<PocoOnlyDbContext>();
            using (var context = new PocoOnlyDbContext(options))
            {
                var log = context.SetupLogging();

                //ATTEMPT
                context.Database.EnsureCreated();

                //VERIFY
                foreach (var logOutput in log)
                {
                    _output.WriteLine(logOutput.ToString());
                }
            }
        }

        [Fact]
        public void TestCreateBookWithAuthorOk()
        {
            //SETUP1
            var options = SqliteInMemory.CreateOptions<PocoOnlyDbContext>();
            using (var context = new PocoOnlyDbContext(options))
            {
                context.Database.EnsureCreated();

                //ATTEMPT
                var book = new Book("test", null, DateTime.UtcNow, "Me", 123, null,
                    new List<Author> {new Author("Me", "me@me.com")});
                context.Add(book);
                context.SaveChanges();
            }
            using (var context = new PocoOnlyDbContext(options))
            {
                //VERIFY
                context.Books.Count().ShouldEqual(1);
                context.Authors.Count().ShouldEqual(1);
                context.Books.Include(x => x.AuthorsLink).ThenInclude(x => x.Author)
                    .Single().AuthorsLink.SingleOrDefault().Author.Name.ShouldEqual("Me");
            }
        }

        [Fact]
        public void TestCreateBookWithAuthorAndReviewOk()
        {
            //SETUP1
            var options = SqliteInMemory.CreateOptions<PocoOnlyDbContext>();
            using (var context = new PocoOnlyDbContext(options))
            {
                context.Database.EnsureCreated();

                //ATTEMPT
                var book = new Book("test", null, DateTime.UtcNow, "Me", 123, null,
                    new List<Author> { new Author("Me", "me@me.com") });
                book.AddReview(2, "Ok", "Me");
                context.Add(book);
                context.SaveChanges();
            }
            using (var context = new PocoOnlyDbContext(options))
            {
                //VERIFY
                context.Books.Count().ShouldEqual(1);
                context.Authors.Count().ShouldEqual(1);
                context.Set<Review>().Count().ShouldEqual(1);
                context.Books.Include(x => x.Reviews)
                    .Single().Reviews.SingleOrDefault().VoterName.ShouldEqual("Me");
            }
        }



        ////This was used to diagnose configuration problems
        //[Fact]
        //public void TestSqlServerCreateBookWithAuthorOk()
        //{
        //    //SETUP1
        //    var options = this.CreateUniqueClassOptions<PocoOnlyDbContext>();
        //    using (var context = new PocoOnlyDbContext(options))
        //    {
        //        context.Database.EnsureCreated();

        //        //ATTEMPT
        //        var book = new Book("test", null, DateTime.UtcNow, "Me", 123, null,
        //            new List<Author> { new Author("Me", "me@me.com") });
        //        context.Add(book);
        //        context.SaveChanges();
        //    }
        //    using (var context = new PocoOnlyDbContext(options))
        //    {
        //        //VERIFY
        //        context.Books.Count().ShouldEqual(1);
        //        context.Authors.Count().ShouldEqual(1);
        //        context.Books.Include(x => x.AuthorsLink).ThenInclude(x => x.Author)
        //            .Single().AuthorsLink.SingleOrDefault().Author.Name.ShouldEqual("Me");
        //    }
        //}
    }
}