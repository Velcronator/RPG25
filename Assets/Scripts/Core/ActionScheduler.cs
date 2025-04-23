using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        // This class is responsible for scheduling and executing actions in the game.
        // It can be used to manage the execution of various actions, such as animations, events, etc.
        // Add your action scheduling logic here.
        // For example, you might want to add methods to start, stop, or queue actions.
        // Example method to start an action

        MonoBehaviour currentAction;

        public void StartAction(MonoBehaviour action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                Debug.Log("Cancelling " + currentAction);
            }
            currentAction = action;
        }
    }
}
