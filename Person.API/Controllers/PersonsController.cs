using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.DTO;
using Person.API.Mappers;
using Person.DAL.Models;

namespace Person.API.Controllers;

[ApiController]
[Route("api/v0/persons")]
public class PersonsController
{
    private readonly PersonsContext _context;

    public PersonsController()
    {
        var connection = WebApplication
            .CreateBuilder()
            .Configuration
            .GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<PersonsContext>();
        var options = optionsBuilder
            .UseSqlite(connection)
            .Options;
        _context = new PersonsContext(options);
    }

    [HttpGet]
    [Route("all")]
    public IEnumerable<PersonDto> GetPersons()
    {
        return _context.Persons.Select(Mapper.ToDto);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public PersonDto? GetPerson(Guid id)
    {
        var person = _context.Persons.SingleOrDefault(p => p.Id == id);
        
        return person is null 
            ? null 
            : Mapper.ToDto(person);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public bool DeletePerson(Guid id)
    {
        var person = _context.Persons.SingleOrDefault(p => p.Id == id);
        if (person == null)
        {
            return false;
        }
        
        _context.Remove(person);
        _context.SaveChanges();
        
        return true;
    }
    
    [HttpPost]
    [Route("new")]
    public bool CreatePerson(PersonDto person)
    {
        _context.Persons.Add(Mapper.ToDal(person));
        _context.SaveChanges();
        
        return true;
    }
}