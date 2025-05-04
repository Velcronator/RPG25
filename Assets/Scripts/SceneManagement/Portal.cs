using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneIndex = -1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if(sceneIndex == -1)
                {
                    Debug.LogError("Scene index not set for portal.");
                    return;
                }
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}
