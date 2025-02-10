using UnityEngine;

namespace ItemScripts
{
    public class ThirdEye : Item
    {
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseAttackRange(strength);
            
            Destroy(gameObject);
        }
    }
}