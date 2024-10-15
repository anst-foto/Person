using Person.DAL.Models;
using Person.DTO;

namespace Person.API.Mappers;

public static class Mapper
{
    public static PersonDto ToDto(PersonDal person)
    {
        return new PersonDto(person.Id, person.FirstName, person.LastName, person.BirthDate);
    }
    
    public static PersonDal ToDal(PersonDto person)
    {
        return new PersonDal(person.Id, person.FirstName, person.LastName, person.BirthDate);
    }
}