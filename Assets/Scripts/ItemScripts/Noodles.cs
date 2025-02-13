using UnityEngine;

namespace ItemScripts
{
    public class Noodles : Item
    {
        private void Start()
        {
            itemEffectDescription = "Ate Noodles: +Max Health +Health";
        }
        
        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.maxHealth += strength;

            playerAttributes.currentHealth += strength;
            
            UpdateItemInfoBox();
            
            Destroy(gameObject);
        }
    }
}