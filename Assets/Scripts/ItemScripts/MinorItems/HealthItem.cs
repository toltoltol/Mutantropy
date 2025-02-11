using System;
using UnityEngine;

namespace ItemScripts.MinorItems
{
    public class HealthItem : Item
    {
        private void Start()
        {
            itemEffectDescription = "Sipped Health Item: +Health";
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseHealth(strength);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}