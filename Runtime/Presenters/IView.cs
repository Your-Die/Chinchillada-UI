namespace Chinchillada.UI
{
    /// <summary>
    /// Interface for UI presenters.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Content"/> being presented.</typeparam>
    public interface IView<in T> 
    {
        /// <summary>
        /// Presents the <paramref name="content"/>.
        /// </summary>
        void Show(T content);

        /// <summary>
        /// Hide this <see cref="IView{T}"/>.
        /// </summary>
        void Hide();
    }
}