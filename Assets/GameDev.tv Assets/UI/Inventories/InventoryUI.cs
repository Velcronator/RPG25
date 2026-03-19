using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;
using TMPro;

namespace GameDevTV.UI.Inventories
{
    /// <summary>
    /// To be placed on the root of the inventory UI. Handles spawning all the
    /// inventory slot prefabs.
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        // CONFIG DATA
        [SerializeField] InventorySlotUI InventoryItemPrefab = null;
        [SerializeField] bool isPlayerInventory = true;
        [SerializeField] TextMeshProUGUI Title;

        // CACHE
        Inventory selectedInventory;

        // LIFECYCLE METHODS

        // LIFECYCLE METHODS

        private void Awake()
        {
            if (isPlayerInventory)
            {
                selectedInventory = Inventory.GetPlayerInventory();
                selectedInventory.inventoryUpdated += Redraw;
            }
        }

        private void Start()
        {
            if (isPlayerInventory)
            {
                Redraw();
            }
        }

        public bool Setup(GameObject user)
        {
            if (user.TryGetComponent(out selectedInventory))
            {
                selectedInventory.inventoryUpdated += Redraw;
                Title.text = selectedInventory.name; //perhaps add a field to Inventory for a displayname
                Redraw();
                return true;
            }
            return false;
        }

        // PRIVATE

        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < selectedInventory.GetSize(); i++)
            {
                var itemUI = Instantiate(InventoryItemPrefab, transform);
                itemUI.Setup(selectedInventory, i);
            }
        }
    }
}