using UnityEngine;

namespace ItemScripts
{
    public class ThirdEye : Item
    {
        private void Start()
        {
            itemEffectDescription = "Attained ThirdEye: +Atk.Speed +Proj.Speed +Atk.Range";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackSpeed(strength / 5);

            playerAttributes.IncreaseAttackProjectileSpeed(strength / 5);

            playerAttributes.IncreaseAttackRange(strength / 10);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}