// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Linq;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;

namespace PocoRepository
{
    public class Repository
    {
        private readonly PocoOnlyDbContext _context;

        public Repository(PocoOnlyDbContext context)
        {
            _context = context;
        }

        public bool AddReview(int id, int numStars, string comment, string voterName)
        {
            var book = _context.Books.Include(x => x.Reviews)
                .SingleOrDefault(x => x.Id == id);
            if (book == null)
                return false;

            book.AddReview(numStars, comment, voterName);
            _context.SaveChanges();
            return true;
        }
    }
}