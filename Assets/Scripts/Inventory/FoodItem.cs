using GameDevTV.Inventories;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = "RPG/Inventory/Food Item")]
    public class FoodItem : ActionItem
    {
        [SerializeField] float healAmount = 25f;

        public override bool Use(GameObject user)
        {
            if (user == null) return false;

            Health health = user.GetComponent<Health>();
            if (health == null) return false;

            if (health.GetHealthPoints() >= health.GetMaxHealthPoints()) return false;

            health.Heal(healAmount);
            return true;
        }
    }
}