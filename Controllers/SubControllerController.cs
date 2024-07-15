using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZadanieRekrutacyjne.Models;
using ZadanieRekrutacyjne.Data;
using Microsoft.AspNetCore.Authorization;

namespace ZadanieRekrutacyjne.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ApiContext _context;

        public SubCategoryController(ApiContext context)
        {
            _context = context;
        }

        // Create or edit
        [AllowAnonymous]
        [HttpPost]
        public JsonResult CreateEdit(SubCategory subCategory)
        {
            if (subCategory.Id == 0)
            {
                _context.SubCategories.Add(subCategory);
            }
            else
            {
                var subCategoryInDb = _context.SubCategories.Find(subCategory.Id);
                if (subCategoryInDb == null)
                {
                    return new JsonResult(NotFound());
                }
                _context.Entry(subCategoryInDb).CurrentValues.SetValues(subCategory);
            }

            _context.SaveChanges();

            return new JsonResult(Ok(subCategory));
        }

        // Get
        [AllowAnonymous]
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.SubCategories.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }

        // Get all
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.SubCategories.ToList();

            return new JsonResult(Ok(result));
        }

        // Delete
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public JsonResult Delete(int id)
        {
            var result = _context.SubCategories.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.SubCategories.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok(result));

        }
    }
}
