using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : InteractAction
{
    public List<InventoryItem> inStock;

    private Inventory inventory;
    private PlayerManager playerManager;

    public Transform itemsParent;
    public Transform shopUIObject;
    public bool isOpen;

    InventorySlot[] slots;
    private bool userCanCangeSatus = true;

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        isOpen = false;
        changeUIVisible();
    }

    void changeUIVisible()
    {
        foreach (Transform child in shopUIObject)
        {
           child.gameObject.SetActive(isOpen);
        }
    }


    public override bool doInteraction(float energy)
    {
        openShopUI();
        return true;
    }

    public void openShopUI()
    {
        Debug.Log("Openshop");
        //playerManager.onShopInteractionCallback.Invoke();
        isOpen = true;
        changeUIVisible();
        UpdateUI();
    }

    void UpdateUI()
    {
        Debug.Log("Update Shop UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inStock.Count)
            {
                slots[i].addItem(inStock[i], i);
            }
            else
            {
                slots[i].clearSlot();
            }
        }
    }

    private void Update()
    {
        if (!isOpen)
            return;
        
       //TODO: Shop schließen
    }
}
