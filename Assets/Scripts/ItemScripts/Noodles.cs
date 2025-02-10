using UnityEngine;

namespace ItemScripts
{
    public class Noodles : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate Noodles: +Health +Max Health";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.currentHealth += strength;

            playerAttributes.maxHealth += strength;
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}