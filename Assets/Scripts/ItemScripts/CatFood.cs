using UnityEngine;

namespace ItemScripts
{
    public class CatFood : Item
    {
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackPower(-(strength / 2));
            
            playerAttributes.IncreaseAttackSpeed(strength);
            
            playerAttributes.IncreaseAttackProjectileSpeed(strength / 2);
            
            Destroy(gameObject);
        }
    }
}