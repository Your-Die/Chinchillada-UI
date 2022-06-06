using System;

namespace Chinchillada.Foundation.UI
{
    using System.Collections.Generic;

    public interface ISelectorView<T> : IView<IReadOnlyList<T>>
    {
        event Action<T> Selected;
    }
}