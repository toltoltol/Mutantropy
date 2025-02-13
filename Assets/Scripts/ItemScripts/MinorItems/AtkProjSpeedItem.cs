using System;
using UnityEngine;


namespace ItemScripts.MinorItems
{

    public class AtkProjSpeedItem : Item
    {
        private void Start()
        {
            itemEffectDescription = "Gulped Proj.Speed Item: +=Proj.Speed";
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackProjectileSpeed(strength / 4);

            UpdateItemInfoBox();

            Destroy(gameObject);
        }
    }
}
