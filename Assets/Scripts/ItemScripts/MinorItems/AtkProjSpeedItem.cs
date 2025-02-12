using System;
using UnityEngine;


namespace ItemScripts.MinorItems
{

    public class AtkProjSpeedItem : Item
    {
        private void Start()
        {
            itemEffectDescription = "Gulped Atk. Projectile Speed Item: +Atk. Projectile Speed";
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackProjectileSpeed(strength);

            UpdateItemInfoBox();

            Destroy(gameObject);
        }
    }
}
