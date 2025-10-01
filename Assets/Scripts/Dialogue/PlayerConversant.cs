using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<string> GetChoices()
        {
            yield return "You complete uter bastard";
            yield return "It's a laugh ain't it?";
            yield return "So we have a really long... string of bollocks here I don't know why.";
        }

        public void Next()
        {
            DialogueNode[] children = currentDialogue.GetAllChildren(currentNode).ToArray();
            currentNode = children[Random.Range(0, children.Length)];
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