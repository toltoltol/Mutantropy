using UnityEngine;

namespace ItemScripts
{
    public class Steroids : Item
    {
        private void Start()
        {
            itemEffectDescription = "Used Steroids: -Max Health +DMG +Atk.Speed +Atk.Range +Movespeed";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            if (playerAttributes.currentHealth <= 1 || playerAttributes.maxHealth <= 1)
            {
                playerAttributes.IncreaseAttackPower(strength);

                playerAttributes.IncreaseAttackSpeed(strength / 4);

                playerAttributes.IncreaseAttackRange(strength / 10);

                playerAttributes.IncreaseMoveSpeed(strength / 20);
            }
            else
            {
                playerAttributes.currentHealth -= strength;

                playerAttributes.maxHealth -= strength;

                playerAttributes.IncreaseAttackPower(strength);

                playerAttributes.IncreaseAttackSpeed(strength / 4);

                playerAttributes.IncreaseAttackRange(strength / 10);

                playerAttributes.IncreaseMoveSpeed(strength / 20);
            }
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}