using Logic.Composite;
using Logic.Iterator;

namespace Logic.Strategy;

public class PeopleFirstStrategy : ISeatingStrategy
{
    public void Assign(IIterator<IComponent> tables, IIterator<IComponent> components)
    {
        while (components.HasNext())
        {
            if (components.Next() is Person person)
            {
                while (tables.HasNext())
                {
                    Table table = (Table)tables.Next();
                    table.AddComponent(person);
                }
            }
        }
        
        while (components.HasNext())
        {
            if (components.Next() is Family family)
            {
                while (tables.HasNext())
                {
                    Table table = (Table)tables.Next();
                    table.AddComponent(family);
                }
            }
        }
    }
}