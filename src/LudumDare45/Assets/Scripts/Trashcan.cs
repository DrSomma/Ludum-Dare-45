using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Loot
{  
    public InventoryItem item;
    public int dropRateInPercent;
}

public class Trashcan : InteractAction
{
    public float speed = 5f;
    public int maxSpawns = 2;
    public Loot[] loot;
    public Animator animator;

    public Sprite usedSprit;
    public SpriteRenderer render;
    private Sprite normalSprite;



    public List<InventoryItem> drops;

    private void Start()
    {
        drops = getDrops();
        normalSprite = render.sprite;
    }


    public override bool doInteraction(float energy)
    {
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        animator.SetTrigger("searchTrash");
        render.sprite = usedSprit;

        if (!canSpawn())
        {
            //TODO: Animation / Sound
            return false;
        }

        //TODO: Loottable
        GameObject ob = Instantiate(drops[0].prefab);
        drops.RemoveAt(0);
        Rigidbody2D rb = ob.GetComponent<Rigidbody2D>();
        ob.transform.position = transform.position;
        float posX = (30 + Random.Range(-10, 10)) * ((drops.Count % 2 == 0) ? -1 : 1);
        rb.AddForce(new Vector2(posX, 350));

        return true;
    }

    public bool canSpawn()
    {
        return drops.Count > 0;
    }

    private List<InventoryItem> getDrops()
    {
        List<InventoryItem> dropList = new List<InventoryItem>();
        for (int x = 0; x < maxSpawns; x++)
        {
            for (int i = 0; i < loot.Length; i++)
            {
                if (Random.Range(0, 100) <= loot[i].dropRateInPercent)
                {
                    dropList.Add(loot[i].item);
                    break;
                }
            }
        }
        

        return dropList;
    }

}
