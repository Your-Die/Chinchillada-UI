using System;
using UnityEngine;

namespace Chinchillada.Foundation.UI
{
    [Serializable]
    public class WrapDecorator : ITextDecorator
    {
        [SerializeField] private string prefix;
        [SerializeField] private string postfix;
        
        public string Decorate(string text) => $"{this.prefix}{text}{this.postfix}";
    }
}