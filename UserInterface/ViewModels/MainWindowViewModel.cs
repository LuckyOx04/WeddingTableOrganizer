using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Logic.Composite;
using Logic.Iterator;
using Logic.Strategy;
using IComponent = Logic.Composite.IComponent;

namespace WeddingTableOrganizer.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private ISeatingStrategy _seatingStrategy = new FamiliesFirstStrategy();

    public string NewFamily
    {
        get;
        set => SetField(ref field, value);
    } = "";

    public string NewPersonToFamily
    {
        get;
        set => SetField(ref field, value);
    } = "";

    public string FamilyForNewPerson
    {
        get;
        set => SetField(ref field, value);
    } = "";

    public string FirstFamilyConflict
    {
        get;
        set => SetField(ref field, value);
    } = "";

    public string SecondFamilyConflict
    {
        get;
        set => SetField(ref field, value);
    } = "";

    public int TableNumber
    {
        get;
        set => SetField(ref field, value);
    } = 1;

    public ObservableCollection<Tuple<string, string>> Conflicts
    {
        get;
        set => SetField(ref field, value);
    } = new();

    public ObservableCollection<IComponent> Tables { get; } = new();

    public ObservableCollection<IComponent> Guests { get; } = new();
    public ObservableCollection<Family> Families => new(Guests.OfType<Family>().Append(new Family("None")));

    public ObservableCollection<string> GuestsStrings { get; } = new();
    public ObservableCollection<string> TablesStrings { get; } = new();

    public void AddFamily()
    {
        if (NewFamily != string.Empty)
        {
            Guests.Add(new Family(NewFamily));
            OnPropertyChanged(nameof(Families));
            RefreshGuestsStrings();
        }
    }

    public void AddPersonToFamily()
    {
        if (NewPersonToFamily != string.Empty && FamilyForNewPerson != string.Empty)
        {
            if (FamilyForNewPerson == "None")
            {
                Guests.Add(new Person(NewPersonToFamily));
                RefreshGuestsStrings();
                return;
            }
            foreach (var guest in Guests.OfType<Family>())
            {
                if (guest.Name == FamilyForNewPerson)
                {
                    guest.AddComponent(new Person(NewPersonToFamily));
                }
            }
            RefreshGuestsStrings();
        }
    }

    public void AddConflict()
    {
        if (FirstFamilyConflict != string.Empty && SecondFamilyConflict != string.Empty){
            Conflicts.Add(new Tuple<string, string>(FirstFamilyConflict, SecondFamilyConflict));
        }
    }
    
    public void ArrangeFamilyFirst()
    {
        _seatingStrategy = new FamiliesFirstStrategy();
        _seatingStrategy.Assign(new ComponentIterator(Tables.ToList()),
            new ComponentIterator(Guests.ToList()),
            new HashSet<Tuple<string, string>>(Conflicts));
        RefreshTablesSrings();
    }

    public void ArrangePeopleFirst()
    {
        _seatingStrategy = new PeopleFirstStrategy();
        _seatingStrategy.Assign(new ComponentIterator(Tables.ToList()), 
            new ComponentIterator(Guests.ToList()),
            new HashSet<Tuple<string, string>>(Conflicts));
        RefreshTablesSrings();
    }

    public void AddTable()
    {
        foreach (var table in Tables)
        {
            if (table.Name == $"Table {TableNumber}")
            {
                throw new ArgumentException("Table number already exists", nameof(TableNumber));
            }
        }
        Tables.Add(new Table($"Table {TableNumber}"));
        RefreshTablesSrings();
    }

    private void RefreshGuestsStrings()
    {
        GuestsStrings.Clear();
        foreach (var guest in Guests)
        {
            if (guest is Family family)
            {
                IIterator<IComponent> peopleIterator = family.CreateIterator();
                StringBuilder result = new StringBuilder();
                result.Append($"{family.Name}({family.Size}): ");
                while (peopleIterator.HasNext())
                {
                    result.Append($"{peopleIterator.Next().Name}, ");
                }
                GuestsStrings.Add(result.ToString().Trim(',', ' '));
            }
            else if (guest is Person person)
            {
                GuestsStrings.Add($"{person.Name}({person.Size})");
            }
        }
    }

    private void RefreshTablesSrings()
    {
        TablesStrings.Clear();
        foreach (var currentTable in Tables)
        {
            if (currentTable is Table table)
            {
                StringBuilder result = new StringBuilder();
                result.Append($"{table.Name}: ");
                IIterator<IComponent> guestsIterator = table.CreateIterator();
                while (guestsIterator.HasNext())
                {
                    if (guestsIterator.Next() is Family family)
                    {
                        result.Append($"{family.Name}({family.Size}): ");
                        IIterator<IComponent> peopleIterator = family.CreateIterator();
                        while (peopleIterator.HasNext())
                        {
                            result.Append($"{peopleIterator.Next().Name}, ");
                        }

                        result.Append("; ");
                    }
                    else if (guestsIterator.Next() is Person person)
                    {
                        result.Append($"{person.Name}({person.Size}); ");
                    }
                }
                TablesStrings.Add(result.ToString().Trim(',', ';', ' '));
            }
        }
    }
    
    
    

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}