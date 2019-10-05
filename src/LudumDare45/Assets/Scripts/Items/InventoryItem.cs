using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public Sprite displayImage;
    public float sellPrice = 0;
    public float buyPrice = 0;
    public string displayName = "ObjectName";
    public string description = "404 description";

    public virtual void Use()
    {
        Debug.Log("Use Item: " + displayName);
    }
}
