using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public Sprite displayImage;
    public int sellPrice = 0;
    public int buyPrice = 0;
    public string displayName = "ObjectName";
    public string description = "404 description";
    public GameObject prefab;

    public virtual void Use(int invIndex)
    {
        Debug.Log("Use Item: " + displayName);
        if (displayName.Equals("Lotto"))
        {
            SceneSwap.Instance.FadeToLevel(2);
        }
    }

    public void Sell(int invIndex)
    {
        if(sellPrice == 0)
        {
            Debug.Log("no sell price");
            //TODO: MSG u cant sell this item
            return;
        }

        PlayerManager.Instance.addMoney(sellPrice);
        Inventory.Instance.removeFromInv(invIndex);
    }

    public void Buy(int invIndex)
    {
        if (PlayerManager.Instance.reduceMoney(buyPrice))
        {
            Inventory.Instance.addToInv(this);
            Debug.Log("Buy: " + name);
        }
        else
        {
            Debug.Log("Buy: No Money");
        }
    }
}
