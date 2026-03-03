using RPG.Control;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestFleeHandler : MonoBehaviour
    {
        [SerializeField] AIController enemyToFlee;
        [SerializeField] Quest questToMonitor;
        [SerializeField] Transform fleeDestination;
        
        private QuestList questList;

        void Start()
        {
            questList = GameObject.FindGameObjectWithTag("Player")?.GetComponent<QuestList>();
            if (questList != null)
            {
                questList.onQuestCompleted += OnQuestCompleted;
            }
            else
            {
                Debug.LogWarning("QuestFleeHandler: Could not find QuestList on Player.");
            }
        }

        void OnDestroy()
        {
            if (questList != null)
            {
                questList.onQuestCompleted -= OnQuestCompleted;
            }
        }

        private void OnQuestCompleted(Quest completedQuest)
        {
            if (completedQuest == questToMonitor && enemyToFlee != null && fleeDestination != null)
            {
                enemyToFlee.StartFleeing(fleeDestination.position);
                Debug.Log($"QuestFleeHandler: Quest '{completedQuest.GetTitle()}' completed. Enemy '{enemyToFlee.name}' is fleeing to {fleeDestination.position}.");

            }
        }
    }
}