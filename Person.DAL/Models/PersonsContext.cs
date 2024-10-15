using Microsoft.EntityFrameworkCore;

namespace Person.DAL.Models;

public record PersonDal(Guid Id, string FirstName, string LastName, DateOnly BirthDate);

public class PersonsContext : DbContext
{
    public DbSet<PersonDal> Persons { get; set; } = null!;
    
    public PersonsContext(DbContextOptions<PersonsContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonDal>().HasData(
            new PersonDal(Guid.NewGuid(), "John", "Doe", new DateOnly(1990, 1, 2)),
            new PersonDal(Guid.NewGuid(), "John", "Smith", new DateOnly(1980, 1, 2)),
            new PersonDal(Guid.NewGuid(), "Anni", "Doe", new DateOnly(1970, 1, 2)),
            new PersonDal(Guid.NewGuid(), "Denis", "Doe", new DateOnly(1960, 1, 2))
        );
    }
}