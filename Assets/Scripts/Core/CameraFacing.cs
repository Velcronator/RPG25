using UnityEngine;
using TMPro;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        private Camera mainCamera;
        private void Awake()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }
        private void LateUpdate()
        {
            transform.forward = mainCamera.transform.forward;
        }
    }

}
