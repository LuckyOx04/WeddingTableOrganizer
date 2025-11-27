namespace Logic.Composite;

public class Person : IComponent
{
    private readonly string _name;

    public Person(string name)
    {
        _name = name;
    }
    
    public int GetSize() => 1;
    public string GetName() => _name;
}