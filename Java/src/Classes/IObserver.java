package Classes;

import java.awt.*;

public interface IObserver<Type> {
    void update(Type value);
}