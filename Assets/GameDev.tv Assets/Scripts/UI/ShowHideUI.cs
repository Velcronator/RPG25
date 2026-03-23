using GameDevTV.UI.Inventories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevTV.UI
{
    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] KeyCode toggleKey = KeyCode.Escape;
        [SerializeField] GameObject uiContainer = null;
        [SerializeField] GameObject otherInventoryContainer = null;
        [SerializeField] InventoryUI otherInventoryUI = null;

        public bool HasOtherInventory => otherInventoryContainer != null;

        // Start is called before the first frame update
        void Start()
        {
            if (uiContainer == null)
            {
                Debug.LogError($"{nameof(ShowHideUI)} missing {nameof(uiContainer)} reference.", this);
                return;
            }

            uiContainer.SetActive(false);
            if (otherInventoryContainer != null) otherInventoryContainer.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                Toggle();
            }
        }

        public void Toggle()
        {
            if (uiContainer == null)
            {
                Debug.LogError($"{nameof(ShowHideUI)} missing {nameof(uiContainer)} reference.", this);
                return;
            }

            if (otherInventoryContainer != null) otherInventoryContainer.SetActive(false);
            uiContainer.SetActive(!uiContainer.activeSelf);
        }

        public void ShowOtherInventory(GameObject go)
        {
            if (!HasOtherInventory)
            {
                Debug.LogWarning($"{nameof(ShowHideUI)} called for other inventory but it's not configured.", this);
                return;
            }

            if (uiContainer == null)
            {
                Debug.LogError($"{nameof(ShowHideUI)} missing {nameof(uiContainer)} reference.", this);
                return;
            }

            uiContainer.SetActive(true);
            if (otherInventoryContainer != null)
            {
                otherInventoryContainer.SetActive(true);
            }

            if (!otherInventoryUI.Setup(go))
            {
                Debug.LogWarning($"{nameof(ShowHideUI)} could not set up other inventory for {go.name}.", this);
            }
        }
    }
}