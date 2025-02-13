using UnityEngine;

namespace ItemScripts
{
    public class DamageItem : Item
    {
        private void Start()
        {
            itemEffectDescription = "Used Strength Item: +Max Health +DMG";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.maxHealth += strength;

            playerAttributes.IncreaseAttackPower(strength / 2);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}