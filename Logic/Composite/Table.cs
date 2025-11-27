namespace Logic.Composite;

public class Table : IComponent
{
    private readonly List<IComponent> _components = new ();
    private readonly string _tableName;
    public const int MaxSeats = 10;

    public Table(string tableName)
    {
        _tableName = tableName;
    }

    public void AddComponent(IComponent component)
    {
        _components.Add(component);
    }

    public void RemoveComponent(IComponent component)
    {
        _components.Remove(component);
    }
    
    public int GetSize() => _components.Sum(c => c.GetSize());
    public string GetName() => _tableName;
}