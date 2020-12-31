using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ObserverCS_S3.Classes
{
    public class PointPublisher : IObservable<Point>
    {
        private List<IObserver<Point>> observers;
        private Point _point;
        public Point point
        {
            get
            {
                return _point;
            }
            set
            {
               
                _point = value;
                // inform subscribers of change
                NotifySubscribers();
            } 
        }

        public PointPublisher(Point value)
        {
            observers = new List<IObserver<Point>>();
            _point = value;
        }

        public IDisposable Subscribe(IObserver<Point> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observers, observer);
        }

        public void NotifySubscribers()
        {
            foreach (var observer in observers)
                observer.OnNext(point);

        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Point>> _observers;
            private IObserver<Point> _observer;

            public Unsubscriber(List<IObserver<Point>> observers, IObserver<Point> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);
            }
        }
    }
}
