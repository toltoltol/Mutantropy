using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float strength;
    
    public abstract void UseItem(PlayerAttributes playerAttributes);
}
