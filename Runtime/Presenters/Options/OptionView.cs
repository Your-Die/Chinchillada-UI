using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada;
using Chinchillada.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Thesis.UI
{
    /// <summary>
    /// Generic implementation of <see cref="Chinchillada.UI.ISelectionView{T}"/> that hides the type implementation behind
    /// a <see cref="IOption"/> interface.
    /// </summary>
    public class OptionView : AutoRefBehaviour, ISelectionView<IOption>
    {
        /// <summary>
        /// The buttons used to present the <see cref="IOption"/>.
        /// </summary>
        [SerializeField] private ComponentPoolList<TextButton> buttons = new ComponentPoolList<TextButton>();

        /// <summary>
        /// Callback invoked when an <see cref="IOption"/> has been selected.
        /// </summary>
        private Action<IOption> selectionCallback;
        
        private const string EventFoldoutGroup = "Events";
        [SerializeField, FoldoutGroup(EventFoldoutGroup)] private UnityEvent presentEvent;
        [SerializeField, FoldoutGroup(EventFoldoutGroup)] private UnityEvent hideEvent;
        
        private readonly Dictionary<TextButton, IOption> optionLookup
            = new Dictionary<TextButton, IOption>();

        private readonly Dictionary<TextButton, ButtonListener> listenerLookup
            = new Dictionary<TextButton, ButtonListener>();

        /// <summary>
        /// Event invoked when an <see cref="IOption"/> has been selected.
        /// </summary>
        public event Action<IOption> SelectionMade;

        public void SelectOption(int index)
        {
            var button = this.buttons[index];
            this.listenerLookup[button].OnButtonClicked();
        }

        /// <inheritdoc />
        public void Show(IList<IOption> options)
        {
            this.buttons.ApplyWith(options, PresentOption);

            this.presentEvent.Invoke();
            
            void PresentOption(IOption option, TextButton button)
            {
                option.Present(button);
                this.optionLookup[button] = option;
            }
        }

        /// <inheritdoc />
        public void Hide()
        {
            this.selectionCallback = null;

            this.optionLookup.Clear();
            this.buttons.Clear();
            
            this.hideEvent.Invoke();
        }

        protected override void Awake()
        {
            base.Awake();

            this.buttons.ItemActivated += this.OnButtonActivated;
            this.buttons.ItemDeactivated += this.OnButtonDeactivated;
        }

        private void OnDestroy()
        {
            this.buttons.ItemActivated -= this.OnButtonActivated;
            this.buttons.ItemDeactivated -= this.OnButtonDeactivated;
        }

        /// <summary>
        /// Invoked when a new button is added to the <see cref="buttons"/>.
        /// </summary>
        private void OnButtonActivated(TextButton button)
        {
            // Create a new button listener.
            this.listenerLookup[button] = new ButtonListener(button, this.OnButtonClicked);
        }

        /// <summary>
        /// Called when a button is deactivated out of <see cref="buttons"/>.
        /// </summary>
        private void OnButtonDeactivated(TextButton button)
        {
            // Deactivate and remove the listener.
            var listener = this.listenerLookup[button];
            listener.Deactivate();
            this.listenerLookup.Remove(button);
        }

        /// <summary>
        /// Called when one of the <see cref="buttons"/> is clicked.
        /// </summary>
        /// <param name="button">The clicked button.</param>
        private void OnButtonClicked(TextButton button)
        {
            // Get the corresponding option.
            var option = this.optionLookup[button];

            // Invoke events.
            this.selectionCallback?.Invoke(option);
            this.SelectionMade?.Invoke(option);
        }

        /// <summary>
        /// Listens to a button, and calls a callback with the button reference when the button is clicked.
        /// </summary>
        private class ButtonListener
        {
            private readonly Action<TextButton> callback;
            private readonly TextButton button;

            public ButtonListener(TextButton button, Action<TextButton> callback)
            {
                this.button = button;
                this.callback = callback;

                button.Button.onClick.AddListener(this.OnButtonClicked);
            }

            public void Deactivate() => this.button.Button.onClick.RemoveListener(this.OnButtonClicked);

            public void OnButtonClicked() => this.callback.Invoke(this.button);
        }
    }
}