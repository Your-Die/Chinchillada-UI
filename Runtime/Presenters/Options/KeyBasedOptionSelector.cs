namespace Presenters.Options
{
    using System.Collections.Generic;
    using Chinchillada.Foundation;
    using Chinchillada.Foundation.UI;
    using UnityEngine;

    public class KeyBasedOptionSelector :  ChinchilladaBehaviour
    {
        [SerializeField] private IMultipleChoicePresenter<IOption> optionPresenter;

        [SerializeField] private List<KeyCode> keys;

        private void LateUpdate()
        {
            for (var i = 0; i < this.keys.Count; i++)
            {
                var key = this.keys[i];
                
                if (Input.GetKeyDown(key))
                    this.optionPresenter.SelectOption(i);
            }
        }
    }
}