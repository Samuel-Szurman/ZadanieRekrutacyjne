using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZadanieRekrutacyjne.Models;
using ZadanieRekrutacyjne.Data;
using Microsoft.AspNetCore.Authorization;

namespace ZadanieRekrutacyjne.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApiContext _context;

        public CategoryController(ApiContext context)
        {
            _context = context;
        }

        // Create/Edit
        [AllowAnonymous]
        [HttpPost]
        public JsonResult CreateEdit(Category category)
        {
            if (category.Id == 0)
            {
                _context.Categories.Add(category);
            }
            else
            {
                var categoryInDb = _context.Categories.Find(category.Id);
                if (categoryInDb == null)
                {
                    return new JsonResult(NotFound());
                }
                _context.Entry(categoryInDb).CurrentValues.SetValues(category);
            }

            _context.SaveChanges();

            return new JsonResult(Ok(category));
        }

        // Get
        [AllowAnonymous]
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Categories.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Categories.ToList();

            return new JsonResult(Ok(result));
        }

        // Delete
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public JsonResult Delete(int id)
        {
            var result = _context.Categories.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Categories.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok(result));

        }
    }
}
