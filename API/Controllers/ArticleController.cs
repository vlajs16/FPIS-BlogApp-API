using DataAccessLayer;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly BlogContext _context;

        public ArticleController(BlogContext context)
        {
            _context = context;
        }

        // GET: api/<ArticleController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var articles = await _context.Articles.Include(x => x.Category).ToListAsync();
            if (articles == null)
                return NotFound("Clanci nisu pronadjeni");

            return Ok(articles);
        }

        // GET api/<ArticleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await _context.Articles
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.ArticleID == id);

            if (article == null)
                return NotFound("Nije pronadjen odgovarajuci artikal");

            return Ok(article);
        }

        // POST api/article
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Article articleToInsert)
        {
            try
            {
                articleToInsert.Category = await _context.Categories
                        .FirstOrDefaultAsync(x => x.CategoryID == articleToInsert.Category.CategoryID);

                _context.Add(articleToInsert);

                if (await _context.SaveChangesAsync() > 0)
                    return Ok();
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Article articleToUpdate)
        {
            try
            {
                var article = await _context.Articles.FirstOrDefaultAsync(x => x.ArticleID == id);
                if (article == null)
                    return NotFound();

                article.Category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryID == articleToUpdate.Category.CategoryID);
                article.Title = articleToUpdate.Title;
                article.Content = articleToUpdate.Content;
                article.PhotoURL = articleToUpdate.PhotoURL;

                _context.Update(article);

                if (await _context.SaveChangesAsync() > 0)
                    return Ok();

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var article = await _context.Articles.FirstOrDefaultAsync(x => x.ArticleID == id);
                if (article == null)
                    return NotFound();

                _context.Articles.Remove(article);

                return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
