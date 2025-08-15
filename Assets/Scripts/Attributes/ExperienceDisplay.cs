using System;
using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        TextMeshProUGUI text;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            text = GameObject.Find("Experience Value").GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            text.SetText("{0:0}", experience.GetExperiencePoints());
        }
    }
}