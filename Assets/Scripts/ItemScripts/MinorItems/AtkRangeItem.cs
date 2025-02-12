using System;
using UnityEngine;

namespace ItemScripts.MinorItems
{

    public class AtkRangeItem : Item
    {
        private void Start()
        {
            itemEffectDescription = "Gulped Atk. Range Item: +Atk.Range";
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackRange(strength);

            UpdateItemInfoBox();

            Destroy(gameObject);
        }
    }
}