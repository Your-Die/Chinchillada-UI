namespace DefaultNamespace
{
    using Chinchillada.Foundation;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    public class PointerEvents : ChinchilladaBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private UnityEvent pointerEnterEvent;
        [SerializeField] private UnityEvent pointerExitEvent;
 
        public void OnPointerEnter(PointerEventData eventData)
        {
            this.pointerEnterEvent.Invoke();
        }

        public void OnPointerExit(PointerEventData  eventData)
        {
            this.pointerExitEvent.Invoke();
        }
    }
}