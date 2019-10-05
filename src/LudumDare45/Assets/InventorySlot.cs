using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button btnElemnt;

    InventoryItem item;

    public void addItem(InventoryItem newItem)
    {
        item = newItem;
        icon.sprite = item.displayImage;
        icon.enabled = true;
        btnElemnt.interactable = true;
    }

    public void clearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        btnElemnt.interactable = false;
    }

    public void useItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
