using Logic.Composite;

namespace Logic.Strategy;

public interface ISeatingStrategy
{
    public void Assign(List<Table> tables, List<IComponent> components);
}