using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.EfClasses.CrUDOnly;
using DataLayer.EfCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PocoRepository;

namespace RazorPageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PocoOnlyController : ControllerBase
    {
        private readonly Repository _repository;

        public PocoOnlyController(PocoOnlyDbContext context)
        {
            _repository = new Repository( context);
        }

        [HttpPatch]
        public IActionResult AddReview(int id, int numStars, string comment, string voterName)
        {
            var ok = _repository.AddReview(id, numStars, comment, voterName);
            return ok ? (IActionResult) Ok() : NoContent();
        }
    }
}