using System;
using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Control;
using UnityEngine;

namespace RPG.Shops
{
    public class Shop : MonoBehaviour, IRaycastable
    {
        [SerializeField] string shopName;

        public event Action onChange;

        public IEnumerable<ShopItem> GetFilteredItems() 
        {
            yield return new ShopItem(InventoryItem.GetFromID("e50a4d9a-d276-46d1-b9f7-9fbcfa3d39ad"),
                10, 10.0f, 0);
            yield return new ShopItem(InventoryItem.GetFromID("135bd571-f3cb-462b-80fd-7816762360d3"),
                10, 10.0f, 0);
            yield return new ShopItem(InventoryItem.GetFromID("0935e90f-0534-4d28-a882-2fa50115ce9a"),
                10, 1000.99f, 0); 
            yield return new ShopItem(InventoryItem.GetFromID("0935e90f-0534-4d28-a882-2fa50115ce9a"),
                10, 10.0f, 0);
        }

        public void SelectFilter(ItemCategory category) {}
        public ItemCategory GetFilter() { return ItemCategory.None; }
        public void SelectMode(bool isBuying) {}
        public bool IsBuyingMode() { return true; }
        public bool CanTransact() { return true; }
        public void ConfirmTransaction() {}
        public float TransactionTotal() { return 0; }

        public string GetShopName()
        {
            return shopName;
        }

        public void AddToTransaction(InventoryItem item, int quantity) {}

        public CursorType GetCursorType()
        {
            return CursorType.Shop;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<Shopper>().SetActiveShop(this);
            }
            return true;
        }
    }
}