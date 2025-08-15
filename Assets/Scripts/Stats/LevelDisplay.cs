using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        TextMeshProUGUI text;
        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            text = GameObject.Find("Level Value").GetComponent<TextMeshProUGUI>();
        }
        private void Update()
        {
            text.SetText("{0:0}", baseStats.GetLevel());
        }
    }
}
