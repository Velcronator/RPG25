using System;
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
        {
            text.SetText("{0:0}%", health.GetPercentage());
        }
    }
}