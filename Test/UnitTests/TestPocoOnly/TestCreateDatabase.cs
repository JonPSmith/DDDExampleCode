// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.EfClasses.PocoOnly;
using DataLayer.EfClasses.PocoOnly.Support;
using DataLayer.EfCode;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests.TestPocoOnly
{
    public class TestCreateDatabase
    {
        [Fact]
        public void TestCreateOrderAndAddToDbOk()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<PocoOnlyDbContext>();
            using (var context = new PocoOnlyDbContext(options))
            {

                //ATTEMPT
                context.Database.EnsureCreated();

                //VERIFY
            }
        }
    }
}