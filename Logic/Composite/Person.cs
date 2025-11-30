namespace Logic.Composite;

public class Person : IComponent
{
    public int Size => 1;
    public string Name { get; }
    
    public Person(string name)
    {
        Name = name;
    }
}