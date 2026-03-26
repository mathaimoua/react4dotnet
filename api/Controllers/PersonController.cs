using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController : ControllerBase
{
    private readonly AppDbContext _db;

    public PersonController(AppDbContext db)
    {
        _db = db;
    }

    // GET api/person
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var people = await _db.People.ToListAsync();
        return Ok(people);
    }
}