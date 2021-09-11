using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeAPI.Models;

namespace RecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsIndexesController : ControllerBase
    {
        private readonly RecipedbContext _context;

        public IngredientsIndexesController(RecipedbContext context)
        {
            _context = context;
        }

        // GET: api/IngredientsIndexes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientsIndex>>> GetIngredientsIndices()
        {
            return await _context.IngredientsIndices.ToListAsync();
        }

        // GET: api/IngredientsIndexes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientsIndex>> GetIngredientsIndex(int id)
        {
            var ingredientsIndex = await _context.IngredientsIndices.FindAsync(id);

            if (ingredientsIndex == null)
            {
                return NotFound();
            }

            return ingredientsIndex;
        }

        // PUT: api/IngredientsIndexes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredientsIndex(int id, IngredientsIndex ingredientsIndex)
        {
            if (id != ingredientsIndex.Id)
            {
                return BadRequest();
            }

            _context.Entry(ingredientsIndex).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientsIndexExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/IngredientsIndexes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IngredientsIndex>> PostIngredientsIndex(IngredientsIndex ingredientsIndex)
        {
            _context.IngredientsIndices.Add(ingredientsIndex);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IngredientsIndexExists(ingredientsIndex.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIngredientsIndex", new { id = ingredientsIndex.Id }, ingredientsIndex);
        }

        // DELETE: api/IngredientsIndexes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientsIndex(int id)
        {
            var ingredientsIndex = await _context.IngredientsIndices.FindAsync(id);
            if (ingredientsIndex == null)
            {
                return NotFound();
            }

            _context.IngredientsIndices.Remove(ingredientsIndex);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientsIndexExists(int id)
        {
            return _context.IngredientsIndices.Any(e => e.Id == id);
        }
    }
}
