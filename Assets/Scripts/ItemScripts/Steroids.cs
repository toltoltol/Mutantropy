using UnityEngine;

namespace ItemScripts
{
    public class Steroids : Item
    {
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackSpeed(strength);
            
            Destroy(gameObject);
        }
    }
}