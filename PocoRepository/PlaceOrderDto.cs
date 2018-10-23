// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace PocoRepository
{
    public class PlaceOrderDto
    {

        public string UserId { get; set; }

        public List<BookAndNum> LineItems { get; set; }

        public class BookAndNum
        {
            public int BookId { get; set; }
            public short NumBooks { get; set; }
        }
    }
}