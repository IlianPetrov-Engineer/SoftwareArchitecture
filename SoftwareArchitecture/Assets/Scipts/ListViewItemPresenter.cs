using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SA_Inventory
{
    /// <summary>
    /// This class presents an item in a list in the GUI.
    /// </summary>
    public class ListViewItemPresenter : ItemPresenter
    {
        public Image icon;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI attackText;
        public TextMeshProUGUI defenseText;

        public override void PresentItem(Item item)
        {
            icon.sprite = item.itemIcon;
            nameText.text = item.ItemName;
            if (item.Health != 0)
                attackText.text = item.Health.ToString();
            else
                attackText.text = "";
            if (item.Defense != 0)
                defenseText.text = item.Defense.ToString();
            else
                defenseText.text = "";
        }
    }
}
