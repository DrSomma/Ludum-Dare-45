using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedFiller : InteractAction
{
    public enum Needs {sleep, hygiene, water};
    public Needs fillNeed;

    public Transform sleepingBubble;

    private DayNightCycle dayNightCycle;

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

            sleepingBubble.position = new Vector3(PlayerManager.Instance.transform.position.x - 0.2f, PlayerManager.Instance.transform.position.y + 1.3f,0);
            sleepingBubble.gameObject.SetActive(true);

            yield return new WaitUntil(() => (int)dayNightCycle.curDayTime == endTimeInt);

            sleepingBubble.gameObject.SetActive(false);

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
