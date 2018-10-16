using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.EfClasses.Standard;
using DataLayer.EfCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RazorPageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandardController : ControllerBase
    {
        private readonly StandardDbContext _context;

        public StandardController(StandardDbContext context)
        {
            _context = context;
        }

        [HttpPatch]
        public IActionResult SetOrderDispatched(int id)
        {
            var order = _context.Find<Order>(id);
            if (order == null)
            {
                return NoContent();
            }
            order.Status = OrderStatuses.Dispatched;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch]
        public IActionResult AddReview(int id, int numStars, string comment, string voterName)
        {
            var book = _context.Books.Include(x => x.Reviews )
                .SingleOrDefault(x => x.BookId == id);
            if (book == null)
            {
                return NoContent();
            }
            book.Reviews.Add(new Review
            {
                NumStars = numStars,
                Comment = comment,
                VoterName = voterName
            });
            _context.SaveChanges();
            return Ok();
        }
    }
}