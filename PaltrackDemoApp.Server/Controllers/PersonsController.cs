using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaltrackDemoApp.Server.Data;
using PaltrackDemoApp.Server.Models;

namespace PaltrackDemoApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly AppDbContext _context;

        //Constructor to initialize database context
        public PersonsController(AppDbContext context)
        {
            _context = context;
        }

        #region CRUD Operations
        //GET: api/Persons
        //Retrieves all persons from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        //POST: api/Persons
        //Creates a new person in the database
        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson(Person person)
        {
            //Add new person
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            //Return created response
            return CreatedAtAction(nameof(GetPersons), new { id = person.Id }, person);
        }

        //PUT: api/Persons/{id}
        //Updates an existing person entry
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(Guid id, Person person)
        {
            //Check if the provided id matches the person's id
            if (id != person.Id)
            {
                return BadRequest();
            }
            //Mark as modified
            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //If the person does not exist
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            //Return nothing after person was updated
            return NoContent();
        }

        //DELETE: api/Persons/{id}
        //Deletes a person from the database
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            //Find person to delete
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            //Delete person
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Helper Methods
        //Check if a person with the given id exists
        private bool PersonExists(Guid id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
        #endregion
    }
}
