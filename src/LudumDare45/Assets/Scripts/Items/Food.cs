using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : InventoryItem
{
    public float addHunger = 0;
    public float addThirst = 0;
    public float addEnergy = 0;
    public FoodType foodType = FoodType.food;

    public enum FoodType {food,drink}

    public override void Use(int invIndex)
    {
        Debug.Log("InvSlot use item: " + name);

        if (foodType.Equals(FoodType.food)) {
            FindObjectOfType<AudioManager>().Play("eating");
        } else {
            FindObjectOfType<AudioManager>().Play("drinking");
        }

        if (addHunger > 0){
            PlayerManager.Instance.addHunger(addHunger);
        } else {
            PlayerManager.Instance.reduceHunger(addHunger);
        }

        if (addThirst > 0)
        {
            PlayerManager.Instance.addThirst(addThirst);
        } else{
            PlayerManager.Instance.reduceThirst(addThirst);
        }

        if (addEnergy > 0){
            PlayerManager.Instance.addEnergy(addEnergy);
        } else {
            PlayerManager.Instance.reduceEnergy(addEnergy);
        }

        Inventory.Instance.removeFromInv(invIndex);
    }
}
