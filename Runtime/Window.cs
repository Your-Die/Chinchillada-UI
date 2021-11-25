namespace Chinchillada.UI
{
    using UnityEngine;

    public abstract class Window : ChinchilladaBehaviour, IWindow
    {
        [SerializeField] private bool hideOnAwake = true;

        protected override void Awake()
        {
            base.Awake();

            if (this.hideOnAwake) this.Hide();
        }

        public abstract void Show();

        public abstract void Hide();
    }
}