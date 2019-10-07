using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedFiller : InteractAction
{
    public enum Needs {sleep, hygiene, water};
    public Needs fillNeed;

    public DayNightCycle dayNightCycle;

    private void Start()
    {
        dayNightCycle = DayNightCycle.Instance;
    }

    public override bool doInteraction(float energy)
    {
        StartCoroutine("freezePlayer");
        return true;
    }

    IEnumerator freezePlayer()
    {
        Debug.Log("Sleeping.....zZzZ");

        if (fillNeed.Equals(Needs.sleep))
        {
            DayNightCycle.Instance.fastForward();
            
            int endTimeInt = ((int)dayNightCycle.curDayTime + 2) % dayNightCycle.dayTimeCnt;
            DayNightCycle.Instance.fastForward();
            PlayerManager.Instance.freezePlayer(true);
            Debug.Log("Speedup");

            yield return new WaitUntil(() => (int)dayNightCycle.curDayTime == endTimeInt);

            DayNightCycle.Instance.normalTime();
            PlayerManager.Instance.freezePlayer(false);
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
