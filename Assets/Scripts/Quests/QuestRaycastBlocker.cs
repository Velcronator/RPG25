using UnityEngine;
using RPG.Quests;
using RPG.Control;

namespace RPG.Quests
{
    public class QuestRaycastBlocker : MonoBehaviour, IRaycastable
    {
        [SerializeField] Quest requiredQuest;
        [SerializeField] GameObject blockerObject;
        
        private QuestList playerQuestList;

        private void Start()
        {
            if (blockerObject == null)
            {
                blockerObject = gameObject;
            }

            ValidateBlocker();
            FindAndSubscribeToPlayer();
        }

        private void OnDestroy()
        {
            if (playerQuestList != null)
            {
                playerQuestList.onUpdate -= OnQuestListUpdated;
            }
        }

        private void ValidateBlocker()
        {
            if (blockerObject.GetComponent<Collider>() == null)
            {
                Debug.LogWarning($"QuestRaycastBlocker on '{gameObject.name}': blockerObject '{blockerObject.name}' has no Collider. Raycasts won't be blocked!", gameObject);
            }
        }

        private void FindAndSubscribeToPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerQuestList = player.GetComponent<QuestList>();
                if (playerQuestList != null)
                {
                    playerQuestList.onUpdate += OnQuestListUpdated;
                    OnQuestListUpdated(); // Check immediately
                }
            }
        }

        private void OnQuestListUpdated()
        {
            if (IsQuestComplete())
            {
                blockerObject.SetActive(false);
                playerQuestList.onUpdate -= OnQuestListUpdated; // Unsubscribe
                enabled = false;
            }
        }

        private bool IsQuestComplete()
        {
            foreach (QuestStatus status in playerQuestList.GetStatuses())
            {
                if (status.GetQuest() == requiredQuest)
                {
                    return status.IsComplete();
                }
            }
            return false;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            // Only block raycasts if we're clicking beyond this blocker
            // Check the distance from camera to this blocker vs the mouse raycast hit point
            Ray mouseRay = PlayerController.GetMouseRay();
            RaycastHit hit;
            
            // Raycast specifically to this blocker's collider
            Collider blockerCollider = blockerObject.GetComponent<Collider>();
            if (blockerCollider != null && blockerCollider.Raycast(mouseRay, out hit, Mathf.Infinity))
            {
                // The mouse is hovering over this blocker itself - block the raycast
                return true;
            }
            
            // Mouse is not directly over this blocker - don't block (allow interaction with closer objects)
            return false;
        }

        public CursorType GetCursorType()
        {
            return CursorType.None;
        }
    }
}