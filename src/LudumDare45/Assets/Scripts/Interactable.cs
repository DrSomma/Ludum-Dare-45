using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum ObjectType { use, pickUp };

    public string objName = "ObjectName";
    public ObjectType type;
    public InteractAction interactAction;
    public float energyCost = 0f;
    public interactUI ui;

    private void Start()
    {
        if (ui == null)
            Debug.LogError("UI NOT SET!!!!!");
        if (interactAction == null)
            Debug.LogError("No interactAction!!!");
    }

    public void playerInteractWithMe()
    {
        if (!PlayerManager.Instance.checkEnergy(energyCost))
        {
            //TODO: Sound!!
            return;
        }
        Debug.Log("do interaction");
        interactAction.doInteraction(energyCost);
    }

    public void playerFacingMe()
    {
        Debug.Log("FacingMe");
        ui.setText(objName, (int) type);
    }

    public void playerLostInterest()
    {
        ui.hide();
    }
}
