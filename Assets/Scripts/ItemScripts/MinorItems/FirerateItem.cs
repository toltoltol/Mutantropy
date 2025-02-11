using System;
using UnityEngine;

namespace ItemScripts.MinorItems
{
    public class FirerateItem : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate Firerate Item: +Firerate";
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackSpeed(strength);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}