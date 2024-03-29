﻿using Chinchillada;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chinchillada.UI
{
    public class SelectOnHover : AutoRefBehaviour, IPointerEnterHandler
    {
        [SerializeField] private GameObject target;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(this.target);
        }

        protected override void FindComponents()
        {
            base.FindComponents();
            
            if (this.target == null) 
                this.target = this.gameObject;
        }
    }
}