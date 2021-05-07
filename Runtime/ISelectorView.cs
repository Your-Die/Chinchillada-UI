using System;

namespace Chinchillada.UI
{
    public interface ISelectorView<out T>
    {
        event Action<T> Selected;
    }
}