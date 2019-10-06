using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedFiller : InteractAction
{
    public enum Needs {sleep, hygiene, water};
    public Needs fillNeed;

    public override bool doInteraction(float energy)
    {
        StartCoroutine("freezePlayer");
        return true;
    }

    IEnumerator freezePlayer()
    {
        Debug.Log("Sleeping.....zZzZ");

        PlayerManager.Instance.freezePlayer(true);
        yield return new WaitForSeconds(5f);
        PlayerManager.Instance.freezePlayer(false);
        
        if (fillNeed.Equals(Needs.sleep))
        {
            PlayerManager.Instance.fillUpEnergy();
        }
        if (fillNeed.Equals(Needs.hygiene))
        {
            PlayerManager.Instance.fillUpHygiene();
        }
        if (fillNeed.Equals(Needs.water))
        {
            PlayerManager.Instance.fillUpThirst();
        }

        Debug.Log("Wake Up");
    }
}
