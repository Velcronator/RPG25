using TMPro;
using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue currentDialogue;

        public string GetText()
        {
            if (currentDialogue == null)
            {
                return "";
            }

            return currentDialogue.GetRootNode().GetText();
        }

        public void Next()
        {
            Debug.Log("Next.");
        }

        public string[] GetChoices()
        {
            return new string[] { "Choice 1", "Choice 2", "Choice 3" };
        }

        public void SelectChoice(int index)
        {
            Debug.Log("Selected choice: " + index);
        }

        public bool IsChoosing()
        {
            return false;
        }

        public bool HasNext()
        {
            return true;
        }

        public void StartDialogue()
        {
            Debug.Log("Dialogue started.");
        }

        public void Quit()
        {
            Debug.Log("Quit.");
        }

    }
}