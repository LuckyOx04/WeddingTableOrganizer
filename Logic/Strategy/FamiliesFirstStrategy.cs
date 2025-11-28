using Logic.Composite;
using Logic.Iterator;

namespace Logic.Strategy;

public class FamiliesFirstStrategy : ISeatingStrategy
{
    public void Assign(IIterator<IComponent> tables, IIterator<IComponent> components)
    {
        while (components.HasNext())
        {
            if (components.Next() is Family family)
            {
                while (tables.HasNext())
                {
                    Table table = (Table)tables.Next();
                    if (!table.Contains(family))
                    {
                        table.AddComponent(family);
                    }
                }
            }
        }

        while (components.HasNext())
        {
            if (components.Next() is Person person)
            {
                while (tables.HasNext())
                {
                    Table table = (Table)tables.Next();
                    if (!table.Contains(person))
                    {
                        table.AddComponent(person);
                    }
                }
            }
        }
    }
}