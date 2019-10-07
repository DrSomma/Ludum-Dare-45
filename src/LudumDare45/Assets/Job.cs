using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : InteractAction
{
    public override bool doInteraction(float energy)
    {
        SceneSwap.Instance.FadeToLevel(1);
        return true;
    }

   
}
