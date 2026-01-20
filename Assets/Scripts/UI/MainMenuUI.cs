using UnityEngine;
using RPG.SceneManagement;
using UnityEngine.UI;
using GameDevTV.Utils;
using System;

namespace RPG.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        LazyValue<SavingWrapper> savingWrapper;

        private void Awake()
        {
            savingWrapper = new LazyValue<SavingWrapper>(GetSavingWrapper);
        }

        private SavingWrapper GetSavingWrapper()
        {
            return FindFirstObjectByType<SavingWrapper>();
        }

        public void ContinueGame()
        {
            savingWrapper.value.ContinueGame();
        }

        public void LoadGame()
        {
            Debug.Log("Starting load game panel...");
        }

        public void NewGame()
        {
            Debug.Log("Starting new game panel...");
        }

        public void QuitGame()
        {
            Debug.Log("Quitting game...");
        }
    }
}
