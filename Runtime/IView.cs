namespace Chinchillada.Foundation.UI
{
    public interface IView<T>
    {
        void Show(T item);

        void Hide();
    }
}