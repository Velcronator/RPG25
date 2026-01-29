using RPG.Control;
using UnityEngine;
using RPG.SceneManagement;

namespace RPG.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        PlayerController playerController;

        private void Awake()
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            if (playerController == null) return;
            Time.timeScale = 0f; 
            playerController.enabled = false;
        }

        private void OnDisable()
        {
            if (playerController == null) return;
            Time.timeScale = 5f; 
            playerController.enabled = true;
        }

        public void Save()
        {
            SavingWrapper savingWrapper = FindFirstObjectByType<SavingWrapper>();
            savingWrapper.Save();
        }

        public void SaveAndQuit()
        {
            SavingWrapper savingWrapper = FindFirstObjectByType<SavingWrapper>();
            savingWrapper.Save();
            savingWrapper.LoadMenu();
        }
    }
}