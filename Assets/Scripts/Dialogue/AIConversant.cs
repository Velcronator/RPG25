using RPG.Attributes;
using RPG.Control;
using UnityEngine;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable, IConversant
    {
        [SerializeField] Dialogue dialogue = null;
        [SerializeField] string conversantName;

        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool CanConverse()
        {
            if (dialogue == null) return false;
            Health health = GetComponent<Health>();
            return !(health && health.IsDead());
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (!CanConverse()) return false;
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            }
            return true;
        }

        public string GetName()
        {
            return conversantName;
        }
    }
}