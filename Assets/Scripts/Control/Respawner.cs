using System;
using System.Collections;
using RPG.Attributes;
using RPG.SceneManagement;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] Transform respawnLocation;
        [SerializeField] float respawnDelay = 3;
        [SerializeField] float fadeTime = 0.2f;
        [SerializeField] float healthRegenPercentage = 20;
        [SerializeField] float enemyHealthRegenPercentage = 20;

        private void Awake()
        {
            GetComponent<Health>().onDie.AddListener(Respawn);
        }

        private void Start()
        {
            if (GetComponent<Health>().IsDead())
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            SavingWrapper savingWrapper = FindFirstObjectByType<SavingWrapper>();
            //savingWrapper.Save(); TODO: just for testing, remove later
            yield return new WaitForSeconds(respawnDelay);
            Fader fader = FindFirstObjectByType<Fader>();
            yield return fader.FadeOut(fadeTime);
            RespawnPlayer();
            ResetEnemies();
            //savingWrapper.Save(); TODO: just for testing, remove later
            yield return fader.FadeIn(fadeTime);
        }

        private void ResetEnemies()
        {
            foreach (AIController enemyControllers in FindObjectsByType<AIController>(FindObjectsSortMode.None))
            {
                Health health = enemyControllers.GetComponent<Health>();
                if (health && !health.IsDead())
                {
                    health.Heal(health.GetMaxHealthPoints() * enemyHealthRegenPercentage / 100);
                    enemyControllers.Reset();
                }
            }
        }

        private void RespawnPlayer()
        {
            Vector3 postionDelta = respawnLocation.position - transform.position;
            GetComponent<NavMeshAgent>().Warp(respawnLocation.position);
            Health health = GetComponent<Health>();
            health.Heal(health.GetMaxHealthPoints() * healthRegenPercentage / 100);

            ICinemachineCamera activeVirtualCamera = FindFirstObjectByType<CinemachineBrain>().ActiveVirtualCamera;
            if (activeVirtualCamera is CinemachineCamera virtualCamera && virtualCamera.Follow == transform)
            {
                virtualCamera.OnTargetObjectWarped(transform, postionDelta);
            }
        }
    }
}