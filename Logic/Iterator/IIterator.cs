namespace Logic.Iterator;

public interface IIterator<T>
{
    bool HasNext();
    T Next();
}