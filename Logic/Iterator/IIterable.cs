namespace Logic.Iterator;

public interface IIterable<T>
{
    IIterator<T> CreateIterator();
}