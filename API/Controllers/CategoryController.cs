using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly BlogContext _context;

        public CategoryController(BlogContext context)
        {
            _context = context;
        }

        // GET: api/category
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _context.Categories.ToListAsync();
            if (categories == null)
                return NotFound("Kategorije nisu pronadjene");

            return Ok(categories);

        }

        // GET api/category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryID == id);
            if (category == null)
                return NotFound("Kategorija nije pronadjena. ");
            return Ok(category);
        }
    }
}
