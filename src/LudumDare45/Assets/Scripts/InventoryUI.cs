using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private PlayerManager playerManager; 

    public Transform itemsParent;
    public Transform invParent;
    public bool isOpen;
    private Vector3 posRight;
    private Vector3 posLeft;

    InventorySlot[] slots;
    private bool userCanCangeSatus = true;

    [Header("Shop")]
    public Transform itemsParentShop;
    public Transform shopUIObjectShop;

    [Header("Player")]
    public Transform follow;
    public float offsetX;
    public float offsetY;

    private bool playerAtStore;


    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.Instance;
        playerManager.onShopInteractionCallback += UpdateUiForShop;

        inventory = Inventory.Instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        //Hide all unused slots
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].gameObject.SetActive(i < inventory.maxSize);
        }
        calcScaling();
        UpdateUI();

        posRight = transform.localPosition;
        posLeft = new Vector3(-posRight.x, posRight.y, 0);

        isOpen = false;
        changeUIVisible();
    }

    void calcScaling()
    {
        if(inventory.maxSize > 12)
        {
            itemsParent.transform.localScale = new Vector3(0.7f, 0.7f);
        }
        else
        {
            if (inventory.maxSize <= 4)
            {
                itemsParent.transform.localScale = new Vector3(1.5f, 1.5f);
            }
            else
            {
                itemsParent.transform.localScale = Vector3.one;
            }
        }
    }

    // Update is called once per frame
    private Vector3 tar;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            isOpen = !isOpen;
            changeUIVisible();
        }

        if (isOpen)
        {
            if (playerManager.facingRight())
            {
                tar = new Vector3(follow.position.x - offsetX, follow.position.y + offsetY, 0);
            }
            else
            {
                tar = new Vector3(follow.position.x + offsetX, follow.position.y + offsetY, 0);
            }
            transform.position = Vector3.Lerp(transform.position, tar, Time.deltaTime * 4f);
        }
    }

    void changeUIVisible()
    {
        if (playerManager.facingRight())
        {
            tar = new Vector3(follow.position.x - offsetX, follow.position.y + offsetY, 0);
            transform.localPosition = tar;
        }
        else
        {
            tar = new Vector3(follow.position.x + offsetX, follow.position.y + offsetY, 0);
            transform.localPosition = tar;
        }

        //Hide/Show UI
        invParent.gameObject.SetActive(isOpen);
        shopUIObjectShop.gameObject.SetActive(playerAtStore && isOpen);
    }

    public void UpdateUiForShop()
    {
        Debug.Log("INV Shop UI");
        if (Shop.shopOpen)
        {
            playerAtStore = true;
            isOpen = true;
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.getSize())
                {
                    slots[i].setShopOpen(true);
                }
                else
                {
                    slots[i].setShopOpen(false);
                }
            }
            changeUIVisible();
        }
        else
        {
            playerAtStore = false;
            isOpen = false;
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].setShopOpen(false);
            }
            changeUIVisible();
        }
    }

    void UpdateUI() {
        Debug.Log("Update UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.getSize())
            {
                slots[i].addItem(inventory.invItems[i],i);
            }
            else
            {
                slots[i].clearSlot();
            }
        }
        StartCoroutine("AutoShowUI");
    }

    IEnumerator AutoShowUI()
    {
        bool curStatus = isOpen;
        if (curStatus)
            yield break;

        userCanCangeSatus = false;
        isOpen = true;
        changeUIVisible();
        yield return new WaitForSeconds(2f);
        isOpen = false;
        changeUIVisible();
        userCanCangeSatus = true;
    }

}
