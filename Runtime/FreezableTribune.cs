using UnityEngine;

namespace Chinchillada.Foundation.UI
{
    public abstract class FreezableTribune<T> : ChinchilladaBehaviour, IUtilityExecutor<T>, IFreezableTribune
    {
        [SerializeField] private int freezePriority = 2;

        [SerializeField] private LogHandler tribuneLogHandler;

        private T currentContent;

        public bool IsSummoned { get; private set; }

        public UtilitySystem<T> UtilitySystem { get; private set; }

        public void Summon(object summoner, int priority, T content)
        {
            this.UtilitySystem.AddOption(summoner, content, priority);
        }

        public void Unsummon(object summoner)
        {
            this.UtilitySystem.RemoveOption(summoner);
        }

        public virtual void ForceHide() => this.UtilitySystem.Clear();

        public virtual void Freeze()
        {
            if (this.IsSummoned == false)
                return;

            this.UtilitySystem.AddOption(this, this.currentContent, this.freezePriority);
        }


        public virtual void Unfreeze()
        {
            this.UtilitySystem.RemoveOption(this);
        }

        protected override void Awake()
        {
            base.Awake();
            this.UtilitySystem = new UtilitySystem<T>(this)
            {
                Logger = this.tribuneLogHandler
            };
        }

        protected abstract void Show(T content);

        protected abstract void Hide();

        private void ShowInternal(T content)
        {
            this.IsSummoned = true;
            this.currentContent = content;
            
            this.Show(content);
        }

        private void HideInternal()
        {
            this.IsSummoned = false;
            this.currentContent = default;
            
            this.Hide();
        }
        
        void IUtilityExecutor<T>.ExecuteOption(T option)
        {
            if (option == null)
                this.HideInternal();
            else
                this.ShowInternal(option);
        }

        void IUtilityExecutor<T>.Stop() => this.Hide();
    }
}