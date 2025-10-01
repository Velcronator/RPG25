using System.Linq;
using TMPro;
using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue currentDialogue;
        DialogueNode currentNode = null;

        private void Awake()
        {
            currentNode = currentDialogue.GetRootNode();
        }
        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }

            return currentNode.GetText();
        }

        public void Next()
        {
            DialogueNode[] children = currentDialogue.GetAllChildren(currentNode).ToArray();
            currentNode = children[Random.Range(0, children.Length)];
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
            return currentDialogue.GetAllChildren(currentNode).Any();
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