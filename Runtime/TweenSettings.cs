namespace Chinchillada.UI
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    [Serializable]
    public class TweenSettings
    {
        [SerializeField] private Ease ease = Ease.InOutSine;

        [SerializeField] private bool isRelative;
        
        public void Apply(Tweener tweener)
        {
            tweener.SetEase(this.ease);
            tweener.SetRelative(this.isRelative);
        }
    }
}