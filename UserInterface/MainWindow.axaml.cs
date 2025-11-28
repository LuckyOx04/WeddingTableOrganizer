using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Logic.Composite;
using Logic.Iterator;
using Logic.Strategy;

namespace WeddingTableOrganizer;

public partial class MainWindow : Window
{
    private readonly List<IComponent> _components = new();
    private readonly List<IComponent> _tables = new();
    private ISeatingStrategy _strategy;
    
    public MainWindow()
    {
        InitializeComponent();

        var family1 = new Family("Smith");
        family1.AddComponent(new Person("John"));
        family1.AddComponent(new Person("Jane"));

        var family2 = new Family("Brown");
        family2.AddComponent(new Person("Mike"));
        family2.AddComponent(new Person("Anna"));

        var family3 = new Family("Johnson");
        family3.AddComponent(new Person("Peter"));

        var person1 = new Person("Ivan");
        var person2 = new Person("George");
        
        _components.Add(family1);
        _components.Add(family2);
        _components.Add(family3);
        _components.Add(person1);
        _components.Add(person2);
        RefreshGuestsList();
        
        _tables.Add(new Table("Table 1"));
        _tables.Add(new Table("Table 2"));
        _tables.Add(new Table("Table 3"));
        RefreshTablesList();
        
        _strategy = new FamiliesFirstStrategy();
    }

    private void RefreshGuestsList()
    {
        GuestsListBox.Items.Clear();
        foreach (var component in _components)
        {
            GuestsListBox.Items.Add($"{component.GetName()} ({component.GetSize()} guests)");
        }
    }

    private void RefreshTablesList()
    {
        TablesListBox.Items.Clear();
        foreach (var table in _tables)
        {
            var t = (Table)table;
            string line = $"{table.GetName()} - {table.GetSize()} guests";
            IIterator<IComponent> familyIterator = t.CreateIterator();
            while (familyIterator.HasNext())
            {
                if (familyIterator.Next() is Family family)
                {
                    line += "\n " + family.GetName() + ": ";
                    IIterator<IComponent> peopleIterator = family.CreateIterator();
                    while (peopleIterator.HasNext())
                    {
                        if (peopleIterator.Next() is Person person)
                        {
                            line += person.GetName() + ", ";
                        }
                    }
                    line = line.TrimEnd(',', ' ');
                }
            }

            TablesListBox.Items.Add(line);
        }
    }

    private IIterator<IComponent> GetComponentIterator() => new ComponentIterator(_components);
    private IIterator<IComponent> GetTablesIterator() => new ComponentIterator(_tables);

    private void AssignFamiliesFirst_Click(object? sender, RoutedEventArgs e)
    {
        _strategy = new FamiliesFirstStrategy();
        _strategy.Assign(GetTablesIterator(), GetComponentIterator());
        _components.Clear();
        RefreshGuestsList();
        RefreshTablesList();
    }

    private void AssignPeopleFirst_Click(object? sender, RoutedEventArgs e)
    {
        _strategy = new PeopleFirstStrategy();
        _strategy.Assign(GetTablesIterator(), GetComponentIterator());
        _components.Clear();
        RefreshGuestsList();
        RefreshTablesList();
    }

    private void AddFamily_Click(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void AddPerson_Click(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void AddTable_Click(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}