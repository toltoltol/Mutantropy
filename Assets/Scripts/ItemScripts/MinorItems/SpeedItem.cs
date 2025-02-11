using System;
using UnityEngine;

namespace ItemScripts.MinorItems
{
    public class SpeedItem : Item
    {
        private void Start()
        {
            itemEffectDescription = "Chewed Speed Item: +Speed";
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseMoveSpeed(strength);

            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}