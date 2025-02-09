using UnityEngine;

namespace ItemScripts
{
    public class CatFood : Item
    {
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.currentHealth = Mathf.Clamp(playerAttributes.attackPower + strength, 
                                                        playerAttributes.minAttackPower, 
                                                        playerAttributes.maxAttackPower);
            Destroy(gameObject);
        }
    }
}