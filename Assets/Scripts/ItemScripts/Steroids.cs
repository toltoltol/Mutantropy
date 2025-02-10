using UnityEngine;

namespace ItemScripts
{
    public class Steroids : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate Steroids: +Firerate +Move speed";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackSpeed(strength);

            playerAttributes.IncreaseMoveSpeed(strength / 2);
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}