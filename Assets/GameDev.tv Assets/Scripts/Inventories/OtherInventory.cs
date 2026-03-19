using GameDevTV.UI;
using RPG.Control;
using UnityEngine;

namespace GameDevTV.Inventories
{
    [RequireComponent(typeof(Inventory))]
    public class OtherInventory : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowHideUI targetUI = null;
                foreach (var ui in FindObjectsByType<ShowHideUI>(FindObjectsSortMode.None))
                {
                    if (ui.IsOtherInventoryEnabled)
                    {
                        targetUI = ui;
                        break;
                    }
                }

                if (targetUI == null)
                {
                    Debug.LogError($"{nameof(OtherInventory)} could not find a configured {nameof(ShowHideUI)}.", this);
                    return true;
                }

                targetUI.ShowOtherInventory(gameObject);
            }
            return true;
        }
    }
}