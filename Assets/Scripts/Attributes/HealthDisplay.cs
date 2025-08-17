using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        TextMeshProUGUI text;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            text = GameObject.Find("Health Value").GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {   // todo if there is less Health arrays than Experience to level up, then this will break and get a NaN value
            text.SetText("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}