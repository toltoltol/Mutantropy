using UnityEngine;

namespace ItemScripts
{
    public class CatFood : Item
    {
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.SetAttackPower(-(strength / 2));
            
            playerAttributes.SetAttackSpeed(strength);
            
            playerAttributes.SetAttackProjectileSpeed(strength / 2);
            
            Destroy(gameObject);
        }
    }
}