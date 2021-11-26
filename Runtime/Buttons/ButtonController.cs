using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chinchillada.UI
{
    using System;

    /// <summary>
    /// Utility interface for accessing both the <see cref="Button"/> and associated <see cref="TMP_Text"/>
    /// on a button object.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ButtonController : AutoRefBehaviour
    {
        /// <summary>
        /// The button.
        /// </summary>
        [SerializeField, FindComponent, Required]
        private Button button;

        /// <summary>
        /// The text.
        /// </summary>
        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private TMP_Text textElement;

        /// <summary>
        /// The button.
        /// </summary>
        public Button Button => this.button;

        /// <summary>
        /// The text.
        /// </summary>
        public TMP_Text TextElement => this.textElement;

        public event Action<ButtonController> Clicked;

        private void OnEnable() => this.button.onClick.AddListener(this.OnClicked);
        private void OnDisable() => this.button.onClick.RemoveListener(this.OnClicked);

        public void OnClicked() => this.Clicked?.Invoke(this);
    }
}