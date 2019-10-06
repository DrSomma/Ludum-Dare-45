using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Image sellIcon;
    public bool isShopSlot = false;
    public Button btnElemnt;
    public int index;

    InventoryItem item;

    private bool isShopOpen;

    public void addItem(InventoryItem newItem, int i)
    {
        item = newItem;
        icon.sprite = item.displayImage;
        icon.enabled = true;
        btnElemnt.interactable = true;
        index = i;
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
            if (!isShopOpen && !isShopSlot)
            {
                item.Use(index);
            }
            else
            {
                if (isShopSlot)
                {
                    item.Buy(index);
                }
                else
                {
                    item.Sell(index);
                }
                
            }
            
        }
    }

    public void setShopOpen(bool b)
    {
        sellIcon.enabled = b;
        isShopOpen = b;
    }
}
