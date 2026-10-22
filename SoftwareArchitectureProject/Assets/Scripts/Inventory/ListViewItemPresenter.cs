using TMPro;
using UnityEngine.UI;

namespace Inventory
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

        public override void PresentItem(Item item, int count = 1)
        {
            icon.sprite = item.itemIcon;
            nameText.text = item.ItemName;
            if (item.IsStackable) nameText.text += " x" + count; 
            if (item.Attack != 0)
                attackText.text = item.Attack.ToString();
            else
                attackText.text = "";
            if (item.Defence != 0)
                defenseText.text = item.Defence.ToString();
            else
                defenseText.text = "";
        }
    }
}