namespace Chinchillada.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using Chinchillada;
    using TMPro;
    using UnityEngine;

    public class TextListDisplay : AutoRefBehaviour, IVisualizer<IEnumerable<string>>
    {
        [SerializeField] private UPoolingList<TMP_Text> textElements;

        public void Visualize(IEnumerable<string> content)
        {
            this.textElements.ApplyWith(content.ToArray(), Present);

            static void Present(string text, TMP_Text textElement) => textElement.SetText(text);
        }
    }
}