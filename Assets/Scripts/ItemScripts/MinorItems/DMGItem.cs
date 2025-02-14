using System;
using UnityEngine;

namespace ItemScripts.MinorItems
{
    public class DMGItem : Item
    {
        private void Start()
        {
            itemEffectDescription = "Munched DMG Item: +DMG";
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackPower(strength / 4);
            
            UpdateItemInfoBox();

            Destroy(gameObject);
        }
    }
}