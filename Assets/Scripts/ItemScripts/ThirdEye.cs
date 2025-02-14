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
            playerAttributes.IncreaseAttackSpeed(strength / 4);

            playerAttributes.IncreaseAttackProjectileSpeed(strength / 10);

            playerAttributes.IncreaseAttackRange(strength / 5);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}