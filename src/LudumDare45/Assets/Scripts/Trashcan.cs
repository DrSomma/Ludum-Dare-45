using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : InteractAction
{
    public GameObject loot;
    public float speed = 5f;
    public int maxSpawns = 2;
 
    private int countSpawned = 0;

    public override void doInteraction(float energy)
    {
        if (canSpawn())
        {
            //TODO: Animation / Sound
            return;
        }
            

        //TODO: Loottable
        GameObject ob = Instantiate(loot, transform);
        Rigidbody2D rb = ob.GetComponent<Rigidbody2D>();
        countSpawned += 1;
        float posX = (50 + Random.Range(-10, 10)) * ((countSpawned % 2 == 0) ? -1 : 1);
        rb.AddForce(new Vector2(posX, 400));
    }

    public bool canSpawn()
    {
        return countSpawned >= maxSpawns;
    }

}
