namespace Chinchillada.Foundation.UI
{
    public interface IOption
    {
        void Present(ButtonController button);
        void Present(ButtonController button, int index);
    }
}