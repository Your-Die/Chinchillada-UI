using UnityEngine;

namespace Chinchillada.UI
{
    public abstract class FreezableTribune<T> : AutoRefBehaviour, IPriorityExecutor<T>, IFreezableTribune
    {
        [SerializeField] private int freezePriority = 2;

        [SerializeField] private LogHandler tribuneLogHandler;

        private T currentContent;

        public bool IsSummoned { get; private set; }

        public PrioritySystem<T> PrioritySystem { get; private set; }

        public void Summon(object summoner, int priority, T content)
        {
            this.PrioritySystem.AddOption(summoner, content, priority);
        }

        public void Unsummon(object summoner)
        {
            this.PrioritySystem.RemoveOption(summoner);
        }

        public virtual void ForceHide() => this.PrioritySystem.Clear();

        public virtual void Freeze()
        {
            if (this.IsSummoned == false)
                return;

            this.PrioritySystem.AddOption(this, this.currentContent, this.freezePriority);
        }


        public virtual void Unfreeze()
        {
            this.PrioritySystem.RemoveOption(this);
        }

        protected override void Awake()
        {
            base.Awake();
            this.PrioritySystem = new PrioritySystem<T>(this)
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
        
        void IPriorityExecutor<T>.ExecuteOption(T option)
        {
            if (option == null)
                this.HideInternal();
            else
                this.ShowInternal(option);
        }

        void IPriorityExecutor<T>.Stop() => this.Hide();
    }
}