using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Image sellIcon;
    public bool isShopSlot = false;
    public Button btnElemnt;
    public int index;
    public Image priceTagImage;
    public TextMeshProUGUI priceTxt;

    InventoryItem item;

    public void addItem(InventoryItem newItem, int i)
    {
        item = newItem;
        icon.sprite = item.displayImage;
        icon.enabled = true;
        btnElemnt.interactable = true;
        index = i;
        if (isShopSlot)
        {
            priceTxt.text = item.buyPrice + "$";
        }
        else
        {
            priceTxt.text = item.sellPrice + "$";
        }
        
        if (Shop.shopOpen)
        {
            priceTagImage.gameObject.SetActive(true);
        }
        
    }

    public void clearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        btnElemnt.interactable = false;
        priceTagImage.gameObject.SetActive(false);
    }

    public void useItem()
    {
        if (item != null)
        {
            if (!Shop.shopOpen && !isShopSlot)
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
        priceTagImage.gameObject.SetActive(b);
    }
}
