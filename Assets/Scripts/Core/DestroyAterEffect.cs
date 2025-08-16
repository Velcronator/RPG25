using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] GameObject targetToDestroy = null;
        private ParticleSystem particleSystemInstance;

        private void Awake()
        {
            particleSystemInstance = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (particleSystemInstance != null && !particleSystemInstance.IsAlive())
            {
                if (targetToDestroy != null)
                {
                    Destroy(targetToDestroy);
                }
                else
                {
                    Destroy(gameObject);
                }
                
            }
        }
    }
}
