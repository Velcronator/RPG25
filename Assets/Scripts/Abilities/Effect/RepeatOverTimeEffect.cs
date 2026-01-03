using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Repeat Over Time Effect", menuName = "Abilities/Effects/Repeat Over Time", order = 0)]
    public class RepeatOverTimeEffect : EffectStrategy
    {
        [SerializeField] float totalDuration = 0f;
        [SerializeField] EffectStrategy effectToRepeat = null;
        [SerializeField] float repeatTime = 0f;
        [SerializeField] float repeatDelay = 0f;


        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(RepeatEffect(data, finished));
        }

        private IEnumerator RepeatEffect(AbilityData data, Action finished)
        {
            float elapsedTime = 0f;
            while (elapsedTime < totalDuration)
            {
                effectToRepeat.StartEffect(data, finished);
                yield return new WaitForSeconds(repeatDelay);
                elapsedTime += repeatTime;
            }
        }
    }
}

