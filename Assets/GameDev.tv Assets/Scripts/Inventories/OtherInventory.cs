using System;
using System.Linq;
using GameDevTV.UI;
using RPG.Control;
using UnityEngine;

namespace GameDevTV.Inventories
{
    [RequireComponent(typeof(Inventory))]
    public class OtherInventory : MonoBehaviour, IRaycastable
    {
        private ShowHideUI showHideUI;

        private void Awake()
        {
            showHideUI = FindObjectsByType<ShowHideUI>(FindObjectsSortMode.None).FirstOrDefault(s => s.HasOtherInventory);
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (showHideUI == null) return false;
            if (Input.GetMouseButtonDown(0))
            {
                showHideUI.ShowOtherInventory(gameObject);
            }
            return true;
        }
    }
}