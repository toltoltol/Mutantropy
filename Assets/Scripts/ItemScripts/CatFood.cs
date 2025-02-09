using UnityEngine;

namespace ItemScripts
{
    public class CatFood : Item
    {
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.attackPower = Mathf.Clamp(playerAttributes.attackPower - strength / 2, 
                                                        playerAttributes.minAttackPower, 
                                                        playerAttributes.maxAttackPower);
            playerAttributes.attackSpeed = Mathf.Clamp(playerAttributes.attackSpeed + strength, 
                playerAttributes.minAttackSpeed, 
                playerAttributes.maxAttackSpeed);
            
            
            Destroy(gameObject);
        }
    }
}