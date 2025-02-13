using System;
using UnityEngine;


namespace ItemScripts.MinorItems
{

    public class NoItem : Item
    {
        private void Start()
        {
            Destroy(gameObject);
        }

        public override void UseItem(PlayerAttributes playerAttributes)
        {
            playerAttributes.IncreaseNothing();

            Destroy(gameObject);
        }
    }
}