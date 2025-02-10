using UnityEngine;

namespace ItemScripts
{
    public class DamageItem : Item
    {
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackPower(strength);
            
            Destroy(gameObject);
        }
    }
}