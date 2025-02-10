using UnityEngine;

namespace ItemScripts
{
    public class ThirdEye : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate ThirdEye: +Range +Bullet Speed";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackRange(strength);
            
            playerAttributes.IncreaseAttackProjectileSpeed(strength);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}