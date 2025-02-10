using UnityEngine;

namespace ItemScripts
{
    public class ChocBar : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate chocbar: +Firerate +Bullet Speed -Range";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackSpeed(strength);
            
            playerAttributes.IncreaseAttackProjectileSpeed(strength);
            
            playerAttributes.IncreaseAttackRange(- strength);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}