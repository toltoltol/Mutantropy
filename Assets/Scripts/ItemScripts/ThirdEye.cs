using UnityEngine;

namespace ItemScripts
{
    public class ThirdEye : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate ThirdEye: +Range";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackRange(strength);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}