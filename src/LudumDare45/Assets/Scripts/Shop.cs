using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : InteractAction
{
    public static bool shopOpen = false;

    public List<InventoryItem> inStock;

    private Inventory inventory;
    private PlayerManager playerManager;

    public InventoryUI invUI;

    private Transform itemsParent;
    private Transform shopUIObject;
    public bool thisShopIsOpen;

    InventorySlot[] slots;
    private bool userCanCangeSatus = true;

    public Transform playerPos;
    public float maxDis = 3f;

    void Start()
    {
        thisShopIsOpen = false;
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
        Debug.Log("........." + Shop.shopOpen);
        if (Shop.shopOpen)
        {
            closeShopUI();
        }
        else
        {
            openShopUI();
        }
        return true;
    }

    private void closeShopUI()
    {
        Debug.Log("closeShopUI");

        thisShopIsOpen = false;
        Shop.shopOpen = false;

        if (playerManager.onShopInteractionCallback != null)
            playerManager.onShopInteractionCallback.Invoke();
    }

    public void openShopUI()
    {
        Shop.shopOpen = true;
        thisShopIsOpen = true;

        Debug.Log("Openshop");
        if(playerManager.onShopInteractionCallback != null)
            playerManager.onShopInteractionCallback.Invoke();

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
        if (!thisShopIsOpen)
            return;
        
        if(Vector3.Distance(transform.position,playerPos.position) > maxDis)
        {
            Debug.Log("xxxxxxSHOP player DIS" + Vector3.Distance(transform.position, playerPos.position));
            closeShopUI();
        }
    }
}
