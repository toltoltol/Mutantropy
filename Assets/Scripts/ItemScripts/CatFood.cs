using System;
using UnityEngine;

namespace ItemScripts
{
    public class CatFood : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate catfood: +Firerate +Bullet Speed -DMG";
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackPower(-(strength / 2));
            
            playerAttributes.IncreaseAttackSpeed(strength);
            
            playerAttributes.IncreaseAttackProjectileSpeed(strength / 2);

            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}