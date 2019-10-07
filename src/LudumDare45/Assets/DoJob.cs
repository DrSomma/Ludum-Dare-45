using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoJob : MonoBehaviour
{
    public int doCount = 20;
    public int toDo = 20;
    public TextMeshProUGUI priceTxt;

    public void pressBtn()
    {
        toDo = toDo - 1;
        priceTxt.text = "Work todo: " + toDo;
        if(toDo <= 0)
        {
            PlayerManager.Instance.addMoney(100);
            SceneSwap.Instance.FadeToLevel(0);
        }
    }

}
