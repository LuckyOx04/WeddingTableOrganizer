using System.Collections.Generic;
using Logic.Composite;
using Logic.Iterator;

namespace WeddingTableOrganizer.Model;

public class SeatingArrangement : IIterable<Table>
{
    private List<Table> _tables;
    
    public List<Table> Tables => _tables;
    public void AddTable(Table table) => _tables.Add(table);
    public void ClearTables() => _tables.Clear();
    
    public IIterator<Table> CreateIterator() => new TableIterator(_tables);
}