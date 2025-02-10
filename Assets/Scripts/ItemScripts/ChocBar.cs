using UnityEngine;

namespace ItemScripts
{
    public class ChocBar : Item
    {
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.SetAttackSpeed(strength);
            
            playerAttributes.SetAttackProjectileSpeed(strength);
            
            playerAttributes.SetAttackRange(- strength);
            
            Destroy(gameObject);
        }
    }
}