using Logic.Iterator;

namespace Logic.Composite;

public class Table : IComponent, IIterable<IComponent>
{
    private readonly List<IComponent> _components = new ();
    private int _currentFamilies;
    private int _currentTakenSeats;
    private const int MaxSeats = 10;
    private const int MaxFamilies = 2;
    
    public int Size => _components.Sum(c => c.Size);
    public string Name { get; }

    public Table(string tableName)
    {
        Name = tableName;
    }

    public void AddComponent(IComponent component)
    {
        if (component is Family && _currentFamilies < MaxFamilies)
        {
            _components.Add(component);
            _currentFamilies++;
            _currentTakenSeats += component.Size;
        }
        else if (component is Person && _currentTakenSeats < MaxSeats)
        {
            _components.Add(component);
            _currentTakenSeats++;
        }
        else
        {
            Console.Error.WriteLine($"This exceeds family or people capacity for table {Name}\nSkipped.");
        }
    }

    public bool Contains(IComponent component)
    {
        return _components.Contains(component);
    }
    
    public IIterator<IComponent> CreateIterator() => new ComponentIterator(_components);
}