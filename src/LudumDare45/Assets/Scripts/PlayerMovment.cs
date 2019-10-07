using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public Animator animator;
    public float speed = 2f;
    public bool lookRight = false;
    public bool canMove = true;
    public bool canInteract = true;

    private float movement;

    private const float rayCastDis = 0.5f;

    private Interactable lastRayCastTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        doMovment();
        useItemKey();
    }

    void useItemKey()
    {
        if (Input.GetKeyUp(KeyCode.F) && lastRayCastTarget!=null && canInteract)
        {
            lastRayCastTarget.playerInteractWithMe();
        }
    }
    void doMovment()
    {
        if (!canMove)
            return;

        float new_movement = Input.GetAxisRaw("Horizontal") * speed;

        checkViewDir(new_movement);

        //Check if player can move 
        Vector2 facing = (lookRight ? Vector2.right : Vector2.left);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, facing, rayCastDis);
        Debug.DrawRay(transform.position, facing * rayCastDis, Color.green);

        // If it hits something...
        if (hit.collider != null)
        {
            //worldborder reached
            if (hit.collider.gameObject.tag == "border")
            {
                new_movement = 0;
            }
            if (hit.collider.gameObject.tag == "interactable")
            {
                Interactable newTarget = hit.collider.gameObject.GetComponent<Interactable>();
                if (lastRayCastTarget != null)
                {
                    if (!newTarget.Equals(lastRayCastTarget))
                    {
                        lastRayCastTarget.playerLostInterest();
                        newTarget.playerFacingMe();
                    }
                }
                else
                {
                    lastRayCastTarget = newTarget;
                    newTarget.playerFacingMe();
                }
            }
        }
        else
        {
            if (lastRayCastTarget != null)
                lastRayCastTarget.playerLostInterest();
            lastRayCastTarget = null;
        }

        movement = new_movement;
        transform.Translate(movement * Time.fixedDeltaTime, 0, 0);

        animator.SetBool("isMoving", movement != 0);
        animator.SetBool("isFacingRight", lookRight);

    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        //transform.Translate(movement * Time.fixedDeltaTime, 0, 0);
    }

    public void StopMovment(bool b)
    {
        canMove = !b;
    }
    public void StopIntract(bool b)
    {
        canInteract = !b;
    }

    void checkViewDir(float mov)
    {
        if(mov < 0 && lookRight) //Now facing left
        {
            lookRight = false;
            //TODO: Change Sprit!!! 
        }
        else if(mov > 0 && !lookRight) //Now right left
        {
            lookRight = true;
            //TODO: Change Sprit!!! 
        }
    }
}
