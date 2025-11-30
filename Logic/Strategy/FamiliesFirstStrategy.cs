using Logic.Composite;
using Logic.Iterator;

namespace Logic.Strategy;

public class FamiliesFirstStrategy : ISeatingStrategy
{
    public void Assign(List<IComponent> tables, List<IComponent> components,
        HashSet<Tuple<string, string>> conflicts)
    {
        foreach (var family in components.OfType<Family>())
        {
            foreach (var table in tables.OfType<Table>())
            {
                if (!table.Contains(family) && !HasFamilyConflict(family, table.CreateIterator(), conflicts))
                {
                    table.AddComponent(family);
                    break;
                }
            }
        }

        foreach (var person in components.OfType<Person>())
        {
            foreach (var table in  tables.OfType<Table>())
            {
                if (!table.Contains(person))
                {
                    table.AddComponent(person);
                    break;
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