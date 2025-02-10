using UnityEngine;

namespace ItemScripts
{
    public class DamageItem : Item
    {
        private void Start()
        {
            itemEffectDescription = "Used Damage Item: +DMG";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackPower(strength);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}