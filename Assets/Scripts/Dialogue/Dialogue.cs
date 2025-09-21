using UnityEngine;

namespace RPG.Dialogues
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField]
        DialogueNode[] nodes;
    }
}