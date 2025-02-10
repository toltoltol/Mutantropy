using UnityEngine;

namespace ItemScripts
{
    public class ChocBar : Item
    {
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackSpeed(strength);
            
            playerAttributes.IncreaseAttackProjectileSpeed(strength);
            
            playerAttributes.IncreaseAttackRange(- strength);
            
            Destroy(gameObject);
        }
    }
}