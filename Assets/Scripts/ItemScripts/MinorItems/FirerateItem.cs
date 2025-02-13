using System;
using UnityEngine;

namespace ItemScripts.MinorItems
{
    public class FirerateItem : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate Atk.Speed Item: +Atk.Speed";
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackSpeed(strength / 4);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}