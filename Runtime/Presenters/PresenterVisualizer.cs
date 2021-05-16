namespace Chinchillada.UI
{
    using Sirenix.Serialization;

    public class PresenterVisualizer<T> : IVisualizer<T>
    {
        [OdinSerialize] private IPresenter<T> presenter;
        
        public void Visualize(T content) => this.presenter.Present(content);
    }
}