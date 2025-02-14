using System;
using UnityEngine;

namespace ItemScripts
{
    public class CatFood : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate CatFood: -DMG +Atk.Speed +Proj.Speed +Movespeed";
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackPower(-(strength / 2));
            
            playerAttributes.IncreaseAttackSpeed(strength / 2);
            
            playerAttributes.IncreaseAttackProjectileSpeed(strength / 10);

            playerAttributes.IncreaseMoveSpeed(strength / 20);

            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}