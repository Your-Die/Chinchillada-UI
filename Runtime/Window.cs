namespace Chinchillada.UI
{
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class Window : AutoRefBehaviour, IWindow
    {
        [SerializeField] private bool hideOnAwake = true;

        [SerializeField] private List<GameObject> staticElements;
        
        protected override void Awake()
        {
            base.Awake();

            if (this.hideOnAwake) 
                this.Hide();
        }

        [Button]
        public virtual void Show() => this.SetStaticElementsActive(true);

        [Button]
        public virtual void Hide() => this.SetStaticElementsActive(false);

        private void SetStaticElementsActive(bool active)
        {
            foreach (var element in this.staticElements) 
                element.SetActive(active);
        }
    }
}