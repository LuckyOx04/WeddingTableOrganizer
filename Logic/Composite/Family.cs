using Logic.Iterator;

namespace Logic.Composite;

public class Family : IComponent, IIterable<IComponent>
{
    private readonly List<IComponent> _components = new();
    
    public int Size => _components.Sum(c => c.Size);
    public string Name { get; }

    public Family(string familyName)
    {
        Name = familyName;
    }

    public void AddComponent(IComponent component)
    {
        if (component is Person)
        {
            _components.Add(component);
            return;
        }
        Console.Error.WriteLine($"You can only add components of type {nameof(Person)} to {nameof(Family)}");
    }

    public IIterator<IComponent> CreateIterator() => new ComponentIterator(_components);
}