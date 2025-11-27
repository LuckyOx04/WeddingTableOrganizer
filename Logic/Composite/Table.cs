namespace Logic.Composite;

public class Table : IComponent
{
    private readonly List<IComponent> _components = new ();
    private readonly string _tableName;
    private int _currentFamilies;
    private int _currentTakenSeats;
    private const int MaxSeats = 10;
    private const int MaxFamilies = 2;

    public Table(string tableName)
    {
        _tableName = tableName;
    }

    public void AddComponent(IComponent component)
    {
        if (component is Family && _currentFamilies < MaxFamilies)
        {
            _components.Add(component);
            _currentFamilies++;
            _currentTakenSeats += component.GetSize();
        }
        else if (component is Person && _currentTakenSeats < MaxSeats)
        {
            _components.Add(component);
            _currentTakenSeats++;
        }
        else
        {
            throw new InvalidOperationException($"This exceeds family or people capacity for table {_tableName}");
        }
    }

    public void RemoveComponent(IComponent component)
    {
        _components.Remove(component);
    }
    
    public int GetSize() => _components.Sum(c => c.GetSize());
    public string GetName() => _tableName;
}