using System;
using UnityEngine;

namespace ItemScripts.MinorItems
{

    public class AtkRangeItem : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate Atk.Range Item: +Atk.Range";
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackRange(strength / 5);

            UpdateItemInfoBox();

            Destroy(gameObject);
        }
    }
}