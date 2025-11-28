using Logic.Iterator;

namespace Logic.Composite;

public class Table : IComponent, IIterable<IComponent>
{
    private readonly List<IComponent> _components = new ();
    private readonly string _tableName;
    private int _currentFamilies;
    private int _currentTakenSeats;
    private const int MaxSeats = 10;
    private const int MaxFamilies = 2;
    public HashSet<(string, string)> FamilyConflicts { get; set; } = new();

    public Table(string tableName)
    {
        _tableName = tableName;
    }

    private bool HasFamilyConflict(IComponent component)
    {
        foreach (var family in _components.OfType<Family>())
        {
            if (FamilyConflicts.Contains((component.GetName(), family.GetName()))
                || FamilyConflicts.Contains((family.GetName(), component.GetName())))
            {
                return true;
            }
        }
        return false;
    }

    public void AddComponent(IComponent component)
    {
        if (component is Family && _currentFamilies < MaxFamilies)
        {
            if (HasFamilyConflict(component))
            {
                Console.Error.WriteLine($"Table {_tableName} has a conflict with family {component.GetName()}");
                Console.Error.WriteLine($"Family conflict with family {component.GetName()} skipped.");
                return;
            }
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
            Console.Error.WriteLine($"This exceeds family or people capacity for table {_tableName}\nSkipped.");
        }
    }

    public void RemoveComponent(IComponent component)
    {
        _components.Remove(component);
    }
    
    public int GetSize() => _components.Sum(c => c.GetSize());
    public string GetName() => _tableName;
    
    public IIterator<IComponent> CreateIterator() => new ComponentIterator(_components);
}