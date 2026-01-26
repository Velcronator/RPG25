using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RPG.SceneManagement;

namespace RPG.UI
{
    public class SaveLoadUI : MonoBehaviour
    {
        [SerializeField] Transform contentRoot;
        [SerializeField] GameObject buttonPrefab;

        private void OnEnable()
        {
            SavingWrapper savingWrapper = FindFirstObjectByType<SavingWrapper>();
            if (savingWrapper == null) return;

            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (string save in savingWrapper.ListSaves())
            {
                GameObject buttonInstance = Instantiate(buttonPrefab, contentRoot);

                TMP_Text textComp = buttonInstance.GetComponentInChildren<TMP_Text>();
                textComp.text = save;

                Button[] buttons = buttonInstance.GetComponentsInChildren<Button>();

                // Assuming first button is Load
                buttons[0].onClick.AddListener(() =>
                {
                    savingWrapper.LoadGame(save);
                });

                // Assuming second button is Delete
                if (buttons.Length > 1)
                {
                    buttons[1].onClick.AddListener(() =>
                    {
                        savingWrapper.Delete(save);
                        OnEnable(); // Refresh the list
                    });
                }
            }
        }
    }
}