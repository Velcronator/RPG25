using GameDevTV.Core.UI.Tooltips;
using UnityEngine;

namespace RPG.UI.Quests
{
    /// <summary>
    /// Spawns a tooltip for a quest when the mouse hovers over the UI element.
    /// </summary>
    public class QuestTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
        }
    }
}
