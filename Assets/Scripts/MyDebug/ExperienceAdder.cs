using UnityEngine;
using RPG.Stats;
using System;

namespace MyDebug
{
    public class ExperienceAdder : MonoBehaviour
    {
        [SerializeField] bool debugMode = true;
        private string playerTag = "Player";
        private GameObject player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag(playerTag);
        }

        void Update()
        {
            if (!debugMode) return;
            if (Input.GetKey(KeyCode.E))
            {
                player.GetComponent<Experience>().GainExperience(Time.deltaTime * 1000);
            }
        }
    }
}
