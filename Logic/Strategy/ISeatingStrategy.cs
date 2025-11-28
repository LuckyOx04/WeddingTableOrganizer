using Logic.Composite;
using Logic.Iterator;

namespace Logic.Strategy;

public interface ISeatingStrategy
{
    public void Assign(IIterator<IComponent> tables, IIterator<IComponent> components,
        HashSet<(string, string)> conflicts);
}