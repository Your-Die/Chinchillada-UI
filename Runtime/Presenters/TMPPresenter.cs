using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chinchillada.UI
{
    public class TMPPresenter : AutoRefBehaviour, IPresenter<string>
    {
        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private TMP_Text textElement;

        [SerializeField] private HashSet<ITextDecorator> decorators = new HashSet<ITextDecorator>();

        private string content;
        private string rawContent;

        public void Present(string newContent)
        {
            this.rawContent = newContent;
            this.BuildText();
        }

        public void Hide() => this.textElement.text = string.Empty;

        public void AddDecorator(ITextDecorator decorator)
        {
            this.decorators.Add(decorator);
            this.BuildText();
        }

        public void RemoveDecorator(ITextDecorator decorator)
        {
            this.decorators.Remove(decorator);
            this.BuildText();
        }

        private void BuildText()
        {
            this.content =  this.decorators != null && this.decorators.Any()
                ? this.decorators.Aggregate(this.rawContent, ExecuteDecorator)
                : this.rawContent;

            this.textElement.text = this.content;

            string ExecuteDecorator(string text, ITextDecorator decorator) => decorator.Decorate(text);
        }
    }
}