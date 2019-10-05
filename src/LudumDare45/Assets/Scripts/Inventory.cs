using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<InventoryItem> invItems;
    public int size;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        invItems = new List<InventoryItem>();
    }

    public bool addToInv(InventoryItem item)
    {
        if(getSize() < size)
        {
            invItems.Add(item);
            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
            return true;
        }
        else
        {
            Debug.Log("no room for Item!");
            return false;
        }
    }

    public void removeFromInv(int index)
    {
        invItems.Remove(invItems[index]);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public int getSize()
    {
        return invItems.Count;
    }
}
