using System;
using System.Collections.Generic;

namespace Chinchillada.UI
{
    /// <summary>
    /// <see cref="IView{T}"/> that presents multiple options of a given type,
    /// and invokes <see cref="SelectionMade"/> when one of those option sis selected.
    /// </summary>
    public interface ISelectionView<T> : IView<IList<T>>
    {
        event Action<T> SelectionMade;

        void SelectOption(int index);
    }
}