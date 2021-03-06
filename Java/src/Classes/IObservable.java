package Classes;

public interface IObservable<Type>
{
    IDisposable subscribe(IObserver<Type> observer);
}
