using Logic.Composite;

namespace Logic.Strategy;

public interface ISeatingStrategy
{
    public void Assign(List<IComponent> tables, List<IComponent> components,
        HashSet<Tuple<string, string>> conflicts);
}