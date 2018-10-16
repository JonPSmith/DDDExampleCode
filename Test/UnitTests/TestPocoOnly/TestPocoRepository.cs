// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.EfClasses.PocoOnly;
using DataLayer.EfClasses.PocoOnly.Support;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;
using PocoRepository;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests.TestPocoOnly
{
    public class TestPocoRepository
    {


        [Fact]
        public void TestAddReviewToBookOk()
        {
            //SETUP1
            var options = SqliteInMemory.CreateOptions<PocoOnlyDbContext>();
            using (var context = new PocoOnlyDbContext(options))
            {
                context.Database.EnsureCreated();
                var book = new Book("test", null, DateTime.UtcNow, "Me", 123, null,
                    new List<Author> {new Author("Me", "me@me.com")});
                context.Add(book);
                context.SaveChanges();
            }
            using (var context = new PocoOnlyDbContext(options))
            {
                var rep = new Repository(context);

                //ATTEMPT
                var ok = rep.AddReview(1, 2, "OK", "Me");

                //VERIFY
                ok.ShouldBeTrue();
            }
            using (var context = new PocoOnlyDbContext(options))
            {
                context.Set<Review>().Count().ShouldEqual(1);
                context.Books.Include(x => x.Reviews)
                    .Single().Reviews.SingleOrDefault().VoterName.ShouldEqual("Me");
            }
        }



    }
}