using UnityEngine;

namespace ItemScripts
{
    public class Steroids : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate Steroids: -Max Health +DMG +Atk.Speed +Atk.Range +Movespeed";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            if (playerAttributes.currentHealth <= 1 || playerAttributes.maxHealth <= 1)
            {
                playerAttributes.IncreaseAttackPower(strength);

                playerAttributes.IncreaseAttackSpeed(strength / 4);

                playerAttributes.IncreaseAttackRange(strength / 5);

                playerAttributes.IncreaseMoveSpeed(strength / 10);
            }
            else
            {
                playerAttributes.currentHealth -= strength;

                playerAttributes.maxHealth -= strength;

                playerAttributes.IncreaseAttackPower(strength);

                playerAttributes.IncreaseAttackSpeed(strength / 4);

                playerAttributes.IncreaseAttackRange(strength / 5);

                playerAttributes.IncreaseMoveSpeed(strength / 10);
            }
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}