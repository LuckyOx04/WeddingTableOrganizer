using Logic.Composite;

namespace Logic.Iterator;

public class TableIterator : IIterator<Table>
{
    private readonly List<Table> _tables;
    private int _position;
    
    public TableIterator(List<Table> tables)
    {
        _tables = tables;
    }
    
    public bool HasNext() => _position < _tables.Count;

    public Table Next()
    {
        if (HasNext())
        {
            return _tables[_position++];
        }
        throw new IndexOutOfRangeException();
    }
}