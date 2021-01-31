﻿using Chinchillada.Thesis.UI;

namespace Chinchillada.Foundation.UI
{
    /// <summary>
    /// Generic class for <see cref="IOption"/> being presented in a <see cref="PooledOptionPresenter"/>.
    /// </summary>
    /// <typeparam name="T">The type of content being encapsulated by this option.</typeparam>
    public class Option<T> : IOption
    {
        public T Content { get; }

        public Option(T content)
        {
            this.Content = content;
        }

        /// <summary>
        /// Present this <see cref="Option{T}"/> in the <paramref name="button"/>.
        /// </summary>
        public virtual void Present(ButtonController button)
        {
            var text = this.Content.ToString();
            SetText(button, text);
        }

        public void Present(ButtonController button, int index)
        {
            var text = $"{index + 1}. {this.Content}";
            SetText(button, text);
        }
        
        private static void SetText(ButtonController button, string text)
        {
            button.TextElement.text = text;
        }

        /// <summary>
        /// Extract the <see cref="Content"/> from the <paramref name="option"/>.
        /// </summary>
        public static T GetContent(IOption option)
        {
            var asBase = (Option<T>) option;
            return asBase.Content;
        }
    }
}