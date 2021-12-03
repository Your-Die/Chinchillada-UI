namespace Chinchillada.UI
{
    using Chinchillada;
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
        private IView<T> presenter;

        private T currentContent;
        
        public PrioritySystem<T> PrioritySystem { get; private set; }

        public void Summon(object summoner, int priority, T content)
        {
            this.PrioritySystem.AddOption(summoner, content, priority);
        }

        public void Unsummon(object summoner)
        {
            this.PrioritySystem.RemoveOption(summoner);
        }

        [UsedImplicitly]
        public void ForceHide() => this.PrioritySystem.Clear();

        public override void Freeze()
        {
            if (this.IsSummoned == false)
                return;

            this.PrioritySystem.AddOption(this, this.currentContent, this.freezePriority);
            base.Freeze();
        }

        public override void Unfreeze()
        {
            this.PrioritySystem.RemoveOption(this);
            base.Unfreeze();
        }

        protected override void Awake()
        {
            base.Awake();
            this.PrioritySystem = new PrioritySystem<T>(this)
            {
                Logger = this.tribuneLogHandler
            };
        }

        private void ShowInternal(T content)
        {
            this.currentContent = content;
            this.presenter.Show(content);
            base.Summon();
        }

        private void HideInternal()
        {
            this.presenter.Hide();
            this.Hide();
        }

        void IPriorityExecutor<T>.ExecuteOption(T option)
        {
            if (option == null)
                this.HideInternal();
            else
                this.ShowInternal(option);
        }

        void IPriorityExecutor<T>.Stop() => this.HideInternal();
    }
}