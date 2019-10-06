using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingSpot : InteractAction
{
    public override bool doInteraction(float energy)
    {
        StartCoroutine("freezePlayer");
        return true;
    }

    public IEnumerable freezePlayer()
    {
        PlayerManager.Instance.freezePlayer(true);
        yield return new WaitForSeconds(5f);
        PlayerManager.Instance.fillUpEnergy();
        PlayerManager.Instance.freezePlayer(false);
    }
}
