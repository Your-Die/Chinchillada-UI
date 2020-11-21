namespace Chinchillada.Foundation.UI
{
    using Foundation;
    using JetBrains.Annotations;
    using Sirenix.OdinInspector;
    using UnityEngine;

    /// <summary>
    /// <remarks>
    /// Todo: Rewrite to use <see cref="FreezableTribune{T}"/>
    /// </remarks>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PresenterPopup<T> : Popup, ITribunePresenter<T>
    {
        [SerializeField] private int freezePriority = 2;

        [SerializeField] private LogHandler tribuneLogHandler;
        
        [SerializeField, FindComponent, Required]
        private IPresenter<T> presenter;

        private T currentContent;
        
        public UtilitySystem<T> UtilitySystem { get; private set; }

        public void Summon(object summoner, int priority, T content)
        {
            this.UtilitySystem.AddOption(summoner, content, priority);
        }

        public void Unsummon(object summoner)
        {
            this.UtilitySystem.RemoveOption(summoner);
        }

        [UsedImplicitly]
        public void ForceHide() => this.UtilitySystem.Clear();

        public override void Freeze()
        {
            if (this.IsSummoned == false)
                return;

            this.UtilitySystem.AddOption(this, this.currentContent, this.freezePriority);
            base.Freeze();
        }

        public override void Unfreeze()
        {
            this.UtilitySystem.RemoveOption(this);
            base.Unfreeze();
        }

        protected override void Awake()
        {
            base.Awake();
            this.UtilitySystem = new UtilitySystem<T>(this)
            {
                Logger = this.tribuneLogHandler
            };
        }

        private void ShowInternal(T content)
        {
            this.currentContent = content;
            this.presenter.Present(content);
            base.Summon();
        }

        private void HideInternal()
        {
            this.presenter.Hide();
            this.Hide();
        }

        void IUtilityExecutor<T>.ExecuteOption(T option)
        {
            if (option == null)
                this.HideInternal();
            else
                this.ShowInternal(option);
        }

        void IUtilityExecutor<T>.Stop() => this.HideInternal();
    }
}