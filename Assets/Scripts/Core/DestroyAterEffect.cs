using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        private ParticleSystem particleSystemInstance;

        private void Awake()
        {
            particleSystemInstance = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (particleSystemInstance != null && !particleSystemInstance.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
