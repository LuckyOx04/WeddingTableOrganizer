using Logic.Composite;
using Logic.Iterator;

namespace Logic.Strategy;

public class FamiliesFirstStrategy : ISeatingStrategy
{
    public void Assign(IIterator<IComponent> tables, IIterator<IComponent> components,
        HashSet<Tuple<string, string>> conflicts)
    {
        while (components.HasNext())
        {
            if (components.Next() is Family family)
            {
                while (tables.HasNext())
                {
                    Table table = (Table)tables.Next();
                    if (!table.Contains(family) && !HasFamilyConflict(family, table.CreateIterator(),
                            conflicts))
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
    
    private bool HasFamilyConflict(IComponent newFamily, IIterator<IComponent> families,
        HashSet<Tuple<string, string>> conflicts)
    {
        while (families.HasNext()){
            IComponent currentFamily = families.Next();
            if (conflicts.Contains(new Tuple<string, string>(newFamily.Name, currentFamily.Name))
                || conflicts.Contains(new Tuple<string, string>(currentFamily.Name, newFamily.Name)))
            {
                return true;
            }
        }
        return false;
    }
}