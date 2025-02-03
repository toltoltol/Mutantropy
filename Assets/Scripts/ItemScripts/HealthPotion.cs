using UnityEngine;

public class HealthPotion : Item
{
    public override void UseItem(PlayerAttributes playerAttributes)
    {
        playerAttributes.currentHealth = Mathf.Clamp(playerAttributes.currentHealth + strength, 
                                                    playerAttributes.minHealth, 
                                                    playerAttributes.maxHealth);
    }
}