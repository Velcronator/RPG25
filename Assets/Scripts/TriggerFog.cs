using UnityEngine;

namespace RPG.SceneManagement
{
    public class TriggerFog : MonoBehaviour
    {
        [SerializeField] private ParticleSystem fogParticles;
        [SerializeField] private string playerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag) && fogParticles != null)
            {
                fogParticles.Play();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(playerTag) && fogParticles != null)
            {
                fogParticles.Stop();
            }
        }
    }
}

