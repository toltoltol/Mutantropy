using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ItemScripts
{
    public abstract class Item : MonoBehaviour
    {
        public float strength;

        public string itemEffectDescription;

        public void Init(float itemStrength)
        {
            strength = itemStrength;
        }

        public abstract void UseItem(PlayerAttributes playerAttributes);

        public void UpdateItemInfoBox()
        {
            
            GameObject textObject = GameObject.Find("item_info_text");

            if (textObject != null)
            {
                TMP_Text itemInfoText = textObject.GetComponent<TMP_Text>();
                if (itemInfoText == null)
                {
                    Debug.LogError("TMP_Text component not found on item_info_text GameObject.");
                }
                else
                {
                    itemInfoText.text = itemEffectDescription;
                }
            }
            else
            {
                Debug.LogError("item_info_text GameObject not found in the scene.");
            }
        }
    }
}