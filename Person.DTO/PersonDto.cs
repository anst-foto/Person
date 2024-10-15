namespace Person.DTO;

/*public class PersonDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
}*/

public record PersonDto(Guid Id, string FirstName, string LastName, DateOnly BirthDate)
{
    public string FullName => $"{LastName} {FirstName}";
}