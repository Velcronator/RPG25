using RPG.Attributes;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;

        Mover mover;
        Health health;

        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
        }

        private void Start()
        {
            currentWaypointIndex = GetClosestWaypoint();
        }

        private void Update()
        {
            if (health != null && health.IsDead()) return;

            PatrolBehaviour();
        }

        private void PatrolBehaviour()
        {
            if (patrolPath == null) return;

            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(GetCurrentWaypoint(), patrolSpeedFraction);
            }

            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private int GetClosestWaypoint()
        {
            if (patrolPath == null) return 0;

            int closestIndex = 0;
            float closestDistance = Mathf.Infinity;

            for (int i = 0; i < patrolPath.transform.childCount; i++)
            {
                float distance = Vector3.Distance(transform.position, patrolPath.GetWaypoint(i));
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }
    }
}
