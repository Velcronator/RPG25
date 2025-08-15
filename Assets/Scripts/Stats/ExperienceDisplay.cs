using TMPro;
using UnityEngine;

namespace RPG.Stats
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
            text.SetText("{0:0}", experience.GetPoints());
        }
    }
}