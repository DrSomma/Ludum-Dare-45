using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : InteractAction
{
    private bool inFly = true;
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void doInteraction(float energy)
    {
        //PlayerManager.Instance.addToInv();
        Debug.Log("Add to INV");
        Destroy(this.gameObject);
    }

    public void FixedUpdate()
    {
        if (!inFly)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
        Debug.DrawRay(transform.position, Vector2.down * 0.5f, Color.green);
        if (hit.collider != null)
        {
            Debug.Log("huhu");
            if (hit.collider.gameObject.tag == "border")
            {
                rb.bodyType = RigidbodyType2D.Static;
                Collider2D[] coll = new Collider2D[2];
                int i = rb.GetAttachedColliders(coll);
                Debug.Log(i);
                foreach(var item in coll)
                {
                    item.enabled = false;
                }
                
            }
        }
    }
}
