using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : InteractAction
{
    public InventoryItem item;

    private bool inFly = true;
    private Rigidbody2D rb;

    private Vector2 throwVector = new Vector2(0,70);

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override bool doInteraction(float energy)
    {

        if (Inventory.Instance.addToInv(item))
        {
            Debug.Log("Pickup Item: " + item.displayName);
            Destroy(this.gameObject);
            return true;
        }
        else
        {
            //TODO: Error Sound
            if (inFly)
                return false;
            inFly = true;
            rb.AddForce(throwVector); 
        }
        return false;
    }

    void Update()
    {
        if (inFly)
        {
            if(Vector3.Distance(Vector3.zero, rb.velocity) < 0.2f)
            {
                inFly = false;
            }
        }
    }
}
