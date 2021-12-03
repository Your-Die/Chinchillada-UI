using System;

namespace Chinchillada.UI
{
    public interface IEditorView<T> : IView<T>
    {
        bool CanEdit { get; set; }
        
        event Action Edited;
    }
}