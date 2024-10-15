using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using Person.DTO;
using ReactiveUI;

namespace Person.CrossPlatform.ViewModels;

public class MainViewModel : ViewModelBase
{
    private static readonly HttpClient Client = new();
    public ObservableCollection<PersonDto> Persons { get; set; }
    
    private PersonDto _selectedPerson;
    public PersonDto SelectedPerson
    {
        get => _selectedPerson;
        set
        {
            var newValue = this.RaiseAndSetIfChanged(ref _selectedPerson, value);

            Id = newValue.Id;
            FirstName = newValue.FirstName;
            LastName = newValue.LastName;
            BirthDate = newValue.BirthDate;

        } 
    }
    
    private Guid? _id;
    public Guid? Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    private string? _firstName;
    public string? FirstName
    {
        get => _firstName;
        set => this.RaiseAndSetIfChanged(ref _firstName, value);
    }
    
    private string? _lastName;
    public string? LastName
    {
        get => _lastName;
        set => this.RaiseAndSetIfChanged(ref _lastName, value);
    }
    
    private DateOnly? _birthDate;
    public DateOnly? BirthDate
    {
        get => _birthDate;
        set
        {
            var newValue = this.RaiseAndSetIfChanged(ref _birthDate, value);
            
            Age = DateTime.Now.Year - newValue.Value.Year;
        } 
    }
    
    private int? _age;
    public int? Age
    {
        get => _age;
        set => this.RaiseAndSetIfChanged(ref _age, value);
    }
    

    public MainViewModel()
    {
        var persons = Client.GetFromJsonAsync<IEnumerable<PersonDto>>("https://localhost:44347/api/v0/persons/all").Result;
        Persons = new ObservableCollection<PersonDto>(persons);
    }
}