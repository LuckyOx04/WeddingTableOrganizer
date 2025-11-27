namespace Logic.Composite;

public class Family : IComponent
{
    private readonly List<IComponent> _components = new();
    private readonly string _familyName;

    public Family(string familyName)
    {
        _familyName = familyName;
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
    public string GetName() => _familyName;
}