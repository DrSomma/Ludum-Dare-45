using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public Sprite displayImage;
    public int sellPrice = 0;
    public int buyPrice = 0;
    public string displayName = "ObjectName";
    public string description = "404 description";

    public virtual void Use(int invIndex)
    {
        Debug.Log("Use Item: " + displayName);
    }

    public void Sell(int invIndex)
    {
        PlayerManager.Instance.addMoney(sellPrice);
        Inventory.Instance.removeFromInv(invIndex);
    }

    public void Buy(int invIndex)
    {
        if (PlayerManager.Instance.reduceMoney(sellPrice))
        {
            Inventory.Instance.addToInv(this);
        }
    }
}
