using System;
using UnityEngine;

namespace ItemScripts.MinorItems
{
    public class SpeedItem : Item
    {
        private void Start()
        {
            itemEffectDescription = "Chewed Movespeed Item: +Movespeed";
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseMoveSpeed(strength / 10);

            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}