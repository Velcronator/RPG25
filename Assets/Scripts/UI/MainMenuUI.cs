using UnityEngine;
using RPG.SceneManagement;
using UnityEngine.UI;
using GameDevTV.Utils;
using System;
using TMPro;

namespace RPG.UI
{
    public class MainMenuUI : MonoBehaviour
    {

        [SerializeField] TMP_InputField newGameNameField;
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

        public void NewGame()
        {
            savingWrapper.value.NewGame(newGameNameField.text);
        }
    }
}
