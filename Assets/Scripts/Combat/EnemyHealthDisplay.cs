using System;
using UnityEngine;
using TMPro;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Health health;
        Fighter fighter;
        TextMeshProUGUI text;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            text = GameObject.Find("Enemy Health Value").GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (fighter.GetTarget() == null || fighter.GetTarget().IsDead())
            {
                text.SetText("N/A");
                return;
            }
            health = fighter.GetTarget().GetComponent<Health>();
            text.SetText("{0:0}%", health.GetPercentage());
        }
    }
}