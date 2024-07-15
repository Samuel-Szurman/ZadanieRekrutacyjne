using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZadanieRekrutacyjne.Models;
using ZadanieRekrutacyjne.Data;
using Microsoft.AspNetCore.Authorization;

namespace ZadanieRekrutacyjne.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ApiContext _context;

        public ContactController(ApiContext context)
        {
            _context = context;
        }

        // Create/Edit
        [AllowAnonymous]
        [HttpPost]
        public JsonResult CreateEdit(Contact contact)
        {
            var existingContact = _context.Contacts.FirstOrDefault(c => c.Email == contact.Email);
            if (existingContact != null && existingContact.Id != contact.Id)
            {
                return new JsonResult(BadRequest("Kontakt o podanym adresie email już istnieje."));
            }

            if (contact.Id == 0)
            {
                _context.Contacts.Add(contact);
            }
            else
            {
                var contactInDb = _context.Contacts.Find(contact.Id);
                if (contactInDb == null)
                {
                    return new JsonResult(NotFound());
                }
                _context.Entry(contactInDb).CurrentValues.SetValues(contact);
            }

            _context.SaveChanges();

            return new JsonResult(Ok(contact));
        }

        // Get
        [AllowAnonymous]
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Contacts.Find(id);
            if(result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }

        // Get all
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetAllPublic()
        {
            var result = _context.Contacts
            .Select(c => new { c.FirstName, c.LastName, c.Email })
            .ToList();

            return new JsonResult(Ok(result));
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetAllPrivate()
        {
            var result = _context.Contacts.ToList();

            return new JsonResult(Ok(result));
        }

        // Delete
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public JsonResult Delete(int id)
        {
            var result = _context.Contacts.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Contacts.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok(result));

        }
    }
}
