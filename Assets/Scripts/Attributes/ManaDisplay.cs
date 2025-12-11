using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class ManaDisplay : MonoBehaviour
    {
        Mana mana;
        TextMeshProUGUI text;

        private void Awake()
        {
            mana = GameObject.FindWithTag("Player").GetComponent<Mana>();
            text = GameObject.Find("Mana Value").GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {   // todo if there is less Mana arrays than Experience to level up, then this will break and get a NaN value
            text.SetText("{0:0}/{1:0}", mana.GetMana(), mana.GetMaxMana());
        }
    }
}