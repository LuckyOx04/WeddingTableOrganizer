using Logic.Composite;

namespace Logic.Iterator;

public class ComponentIterator : IIterator<IComponent>
{
    private readonly List<IComponent> _components;
    private int _position;

    public ComponentIterator(List<IComponent> components)
    {
        _components = components;
    }
    
    public bool HasNext() => _position < _components.Count;

    public IComponent Next()
    {
        if (HasNext())
        {
            return _components[_position++];
        }
        throw new IndexOutOfRangeException();
    }
}