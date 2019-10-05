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
            //Update UI
            setUIText();
            return;
        }
        Debug.Log("do interaction");
        PlayerManager.Instance.reduceEnergy(energyCost);
        interactAction.doInteraction(energyCost);
        //Update UI
        setUIText();
    }

    public void playerFacingMe()
    {
        Debug.Log("FacingMe");
        setUIText();
        ui.show();
    }

    public void playerLostInterest()
    {
        ui.hide();
    }

    public void setUIText()
    {
        if (PlayerManager.Instance.checkEnergy(energyCost))
        {
            ui.setText(objName, (int)type, energyCost);
        }
        else
        {
            ui.setText(objName, (int)type, energyCost, interactUI.COLOR_RED);
        }
    }
}
