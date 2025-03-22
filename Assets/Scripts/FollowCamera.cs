using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = 0.125f;

    void Update()
    {
        transform.position = target.position;
    }
}
