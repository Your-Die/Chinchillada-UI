using UnityEngine;

namespace Chinchillada.UI
{
    public class TribunePresenter<T> : FreezableTribune<T>
    {
        [SerializeField] private IPresenter<T> presenter;
        
        protected override void Show(T content) => this.presenter.Present(content);

        protected override void Hide() => this.presenter.Hide();
    }
}