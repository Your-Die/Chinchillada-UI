using Chinchillada.Foundation;
using Chinchillada.Foundation.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Packages.Runtime
{
    public class DecoratingSelectable : ChinchilladaBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private ITextDecorator decorator;
        
        [SerializeField, Required, FindComponent]
        private TMPPresenter presenter;


        public void OnSelect(BaseEventData eventData) => this.presenter.AddDecorator(this.decorator);

        public void OnDeselect(BaseEventData eventData) => this.presenter.RemoveDecorator(this.decorator);
    }
}