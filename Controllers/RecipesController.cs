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
    public class RecipesController : ControllerBase
    {
        private readonly RecipedbContext _context;
        public static int sno=2;
       

        public RecipesController(RecipedbContext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }
      
        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Rid)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.Rid }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Rid == id);
        }



        // GET: api/Recipes/5
        [HttpGet("item/{ingre}")]
        public dynamic FindRecipe(string ingre)
        {
            // var recipe = await _context.Recipes.FindAsync(ingre);
          //  var q = (from i in _context.Recipes
            //         select i).ToList();
           var q = (from i in _context.Recipes
                     join j in _context.IngredientsIndices on i.Rid equals j.Rid
                     join k in _context.Ingredients on j.Iid equals k.Iid
                     where k.Iname == ingre
                     select new
                     {
                         i.Rid,
                         i.Rname,
                         i.Instructions,
                     }).ToList();

            return q;

            //return Task.FromResult((IEnumerable<Recipe>)q);
        }

        [HttpPost("/myrecipes/{id}")]
        public async Task<ActionResult<Recipe>> MyRecipe(int id)
        {
            MyRecipe obj = new MyRecipe();
            sno = sno + 1;
            obj.Id = sno;
            obj.RId = id;
            _context.MyRecipes.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("/myrecipes")]
        public dynamic MyRecipes()
        {
            var q = (from i in _context.Recipes
                     join j in _context.MyRecipes on i.Rid equals j.RId
                   select i);
            return q;

        }
    }
}
