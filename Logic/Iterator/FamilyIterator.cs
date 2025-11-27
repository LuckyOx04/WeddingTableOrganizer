using Logic.Composite;

namespace Logic.Iterator;

public class FamilyIterator : IIterator<Family>
{
    private readonly List<Family> _families;
    private int _position;

    public FamilyIterator(List<Family> families)
    {
        _families = families;
    }
    
    public bool HasNext() => _position < _families.Count;

    public Family Next()
    {
        if (HasNext())
        {
            return _families[_position++];
        }
        throw new IndexOutOfRangeException();
    }
}