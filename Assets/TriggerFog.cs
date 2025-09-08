using UnityEngine;

namespace RPG.SceneManagement
    {
    public class TriggerFog : MonoBehaviour
    {
        [SerializeField] private GameObject fogEffect; // Reference to the fog effect GameObject
        [SerializeField] private string playerTag = "Player"; // Tag to identify the player
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                fogEffect.SetActive(true); // Activate the fog effect when the player enters the trigger
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                fogEffect.SetActive(false); // Deactivate the fog effect when the player exits the trigger
            }
        }
    }
}
