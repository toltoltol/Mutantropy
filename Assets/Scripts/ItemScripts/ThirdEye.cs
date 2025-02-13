using UnityEngine;

namespace ItemScripts
{
    public class ThirdEye : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate ThirdEye: +Atk.Speed +Proj.Speed +Range";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackSpeed(strength / 4);

            playerAttributes.IncreaseAttackProjectileSpeed(strength / 4);

            playerAttributes.IncreaseAttackRange(strength / 2);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}