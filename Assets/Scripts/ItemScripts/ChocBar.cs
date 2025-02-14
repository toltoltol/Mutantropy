using UnityEngine;

namespace ItemScripts
{
    public class ChocBar : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate Chocbar: +Atk.Speed +Proj.Speed -Atk.Range +Movespeed";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackSpeed(strength / 2);
            
            playerAttributes.IncreaseAttackProjectileSpeed(strength / 10);
            
            playerAttributes.IncreaseAttackRange(-strength / 5);

            playerAttributes.IncreaseMoveSpeed(strength / 20);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}