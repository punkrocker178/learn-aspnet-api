using demo_asp.net.Entities;
using demo_asp.net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demo_asp.net.Controllers;
[ApiController]
[Route("api/pizzas")]
public class PizzaController : ControllerBase
{
    private PizzaDb _pizzaDb;
    public PizzaController(PizzaDb pizzaDb)
    {
        _pizzaDb = pizzaDb;
    }

    // GET all action
    [HttpGet]
    public async Task<IEnumerable<Pizza>> GetAll()
    {
        return await _pizzaDb.Pizzas.ToListAsync();
        // return PizzaService.GetAll();
    }

    // GET by Id action
    [HttpGet("{id}")]
    public async Task<ActionResult<Pizza>> Get(int id)
    {
        // var pizza = PizzaService.Get(id);

        var pizza = await _pizzaDb.Pizzas.FindAsync(id);

        if (pizza == null)
        {
            return NotFound();
        }

        return pizza;
    }

    // POST action
    [HttpPost]
    public async Task<IActionResult> Create(Pizza pizza)
    {
        await _pizzaDb.Pizzas.AddAsync(pizza);
        await _pizzaDb.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { pizza.Id }, pizza);
        // PizzaService.Add(pizza);
    }

    // PUT action
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Pizza pizzaInput)
    {
        // if (id != pizza.Id)
        //     return BadRequest();

        // var existingPizza = PizzaService.Get(id);
        // if (existingPizza is null)
        //     return NotFound();

        // PizzaService.Update(pizza);

        // return NoContent();
        var pizza = await _pizzaDb.Pizzas.FindAsync(id);

        if (pizza is null)
        {
            return NotFound();
        }

        pizza.Name = pizzaInput.Name;
        pizza.Description = pizzaInput.Description;
        await _pizzaDb.SaveChangesAsync();

        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // var pizza = PizzaService.Get(id);
        var pizza = await _pizzaDb.Pizzas.FindAsync(id);

        if (pizza is null)
        {
            return NotFound();
        }

        // PizzaService.Delete(id);
        _pizzaDb.Pizzas.Remove(pizza);
        await _pizzaDb.SaveChangesAsync();
        

        return NoContent();
    }
}
