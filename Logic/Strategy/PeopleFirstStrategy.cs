using Logic.Composite;
using Logic.Iterator;

namespace Logic.Strategy;

public class PeopleFirstStrategy : ISeatingStrategy
{
    public void Assign(IIterator<IComponent> tables, IIterator<IComponent> components,
        HashSet<(string, string)> conflicts)
    {
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
    }
    
    private bool HasFamilyConflict(IComponent newFamily, IIterator<IComponent> families,
        HashSet<(string, string)> conflicts)
    {
        while (families.HasNext()){
            IComponent currentFamily = families.Next();
            if (conflicts.Contains((newFamily.GetName(), currentFamily.GetName()))
                || conflicts.Contains((currentFamily.GetName(), newFamily.GetName())))
            {
                return true;
            }
        }
        return false;
    }
}