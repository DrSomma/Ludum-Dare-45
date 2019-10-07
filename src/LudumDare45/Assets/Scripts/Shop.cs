using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : InteractAction
{
    public List<InventoryItem> inStock;

    private Inventory inventory;
    private PlayerManager playerManager;

    public InventoryUI invUI;

    private Transform itemsParent;
    private Transform shopUIObject;
    public bool isOpen;

    InventorySlot[] slots;
    private bool userCanCangeSatus = true;

    void Start()
    {
        isOpen = false;
        playerManager = PlayerManager.Instance;
    }

    void changeUIVisible()
    {
        //First open
        if(slots == null)
        {
            this.itemsParent = invUI.itemsParentShop;
            shopUIObject = invUI.shopUIObjectShop;
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        }
        //shopUIObject.gameObject.SetActive(isOpen);
    }


    public override bool doInteraction(float energy)
    {
        openShopUI();
        return true;
    }

    public void openShopUI()
    {
        Debug.Log("Openshop");
        if(playerManager.onShopInteractionCallback != null)
            playerManager.onShopInteractionCallback.Invoke();

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
