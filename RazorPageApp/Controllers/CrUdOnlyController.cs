﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.EfClasses.CrUDOnly;
using DataLayer.EfCode;
using GenericServices;
using GenericServices.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.WebApiControllers.Dtos;

namespace RazorPageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrUdOnlyController : ControllerBase
    {
        private readonly CrUDOnlyDbContext _context;

        public CrUdOnlyController(CrUDOnlyDbContext context)
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
            order.MarkOrderAsDispatched();
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch]
        public IActionResult AddReview(int id, int numStars, string comment, string voterName)
        {
            var book = _context.Find<Book>(id);
            if (book == null)
            {
                return NoContent();
            }
            book.AddReview(numStars, comment, voterName);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch]
        public ActionResult<WebApiMessageOnly> AddReview2(
            AddReviewDto dto, [FromServices] ICrudServices service)
        {
            service.UpdateAndSave(dto);
            return service.Response();
        }
    }
}