using System.Collections.Generic;
using Logic.Composite;
using Logic.Iterator;

namespace WeddingTableOrganizer.Model;

public class SeatingArrangement : IIterable<IComponent>
{
    private List<IComponent> _tables = new();
    
    public List<IComponent> Tables => _tables;
    public void AddTable(Table table) => _tables.Add(table);
    public void ClearTables() => _tables.Clear();
    
    public IIterator<IComponent> CreateIterator() => new ComponentIterator(_tables);
}