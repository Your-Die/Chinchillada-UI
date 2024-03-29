namespace Chinchillada.UI
{
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Events;

    public class Window : AutoRefBehaviour, IWindow
    {
        [SerializeField] private bool hideOnAwake = true;

        [SerializeField] private List<GameObject> staticElements;

        [SerializeField] private UnityEvent showEvent;
        
        protected override void Awake()
        {
            base.Awake();

            if (this.hideOnAwake) 
                this.Hide();
        }

        [Button]
        public virtual void Show()
        {
            this.SetStaticElementsActive(true);
            this.showEvent.Invoke();
        }

        [Button]
        public virtual void Hide() => this.SetStaticElementsActive(false);

        private void SetStaticElementsActive(bool active)
        {
            foreach (var element in this.staticElements) 
                element.SetActive(active);
        }
    }
}