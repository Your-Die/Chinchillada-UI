namespace Chinchillada.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class SimpleOptionPresenter : AutoRefBehaviour, ISelectionView<IOption>
    {
        [SerializeField] private List<TextButton> buttons = new List<TextButton>();

        [SerializeField] private bool prependNumbers;

        private readonly Dictionary<TextButton, IOption> optionLookup =
            new Dictionary<TextButton, IOption>();

        public event Action<IOption> SelectionMade;

        public void SelectOption(int index)
        {
            var button = this.buttons[index];
            this.OnButtonClicked(button);
        }

        public void Show(IList<IOption> content)
        {
            var options = content.ToArray();

            var count = Mathf.Min(options.Length, this.buttons.Count);
            if (count != options.Length)
                Debug.LogWarning("More options than buttons.");

            for (var index = 0; index < count; index++)
            {
                var button = this.buttons[index];
                button.gameObject.SetActive(true);
                var option = options[index];

                this.optionLookup[button] = option;
                
                if (this.prependNumbers)
                    option.Present(button, index);
                else
                    option.Present(button);
            }

            for (var i = count; i < this.buttons.Count; i++)
            {
                var button = this.buttons[i];
                button.gameObject.SetActive(false);
            }
        }

        public void Hide()
        {
            this.optionLookup.Clear();
            foreach (var button in this.buttons)
                button.gameObject.SetActive(false);
        }

        protected override void Awake()
        {
            base.Awake();
            this.Hide();
        }

        private void OnButtonClicked(TextButton button)
        {
            var option = this.optionLookup[button];
            this.SelectionMade?.Invoke(option);
        }

        private void OnEnable()
        {
            foreach (var button in this.buttons)
                button.Clicked += this.OnButtonClicked;
        }

        private void OnDisable()
        {
            foreach (var button in this.buttons)
                button.Clicked -= this.OnButtonClicked;
        }
    }
}