using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemScripts
{
    public abstract class Item : MonoBehaviour
    {
        public float strength;

        public void Init(float itemStrength)
        {
            strength = itemStrength;
        }

        public abstract void UseItem(PlayerAttributes playerAttributes);
    }
}