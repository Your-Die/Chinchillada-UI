using UnityEngine;

namespace Chinchillada.UI
{
    public class TribunePresenter<T> : FreezableTribune<T>
    {
        [SerializeField] private IView<T> presenter;
        
        protected override void Show(T content) => this.presenter.Show(content);

        protected override void Hide() => this.presenter.Hide();
    }
}