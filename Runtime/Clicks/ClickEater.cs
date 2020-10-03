namespace Chinchillada.Thesis.UI
{
    using UnityEngine.EventSystems;

    public class ClickEater : UIBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            eventData.Use();
        }
    }
}