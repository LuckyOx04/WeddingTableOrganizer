using Logic.Composite;

namespace Logic.Strategy;

public class FamiliesFirstStrategy : ISeatingStrategy
{
    public void Assign(List<Table> tables, List<IComponent> components)
    {
        foreach (var person in components.OfType<Family>())
        {
            foreach (var table in tables)
            {
                try
                {
                    table.AddComponent(person);
                }
                catch (InvalidOperationException e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }
        }

        foreach (var family in components.OfType<Person>())
        {
            foreach (var table in tables)
            {
                try
                {
                    table.AddComponent(family);
                }
                catch (InvalidOperationException e)
                {
                    Console.Error.WriteLine(e);
                }
            }
        }
    }
}