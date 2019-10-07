using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.LWRP;

public class LampController : MonoBehaviour
{
    public Light2D light;
    public bool flicker;
    public bool isOn;

    private DayNightCycle dayNightCycle;
    private Animator animator;

    void Start()
    {
        light.intensity = 0;
        dayNightCycle = DayNightCycle.Instance;
        dayNightCycle.onDayTimeChangeCallback += updateLight;

        animator = GetComponent<Animator>();
        animator.SetBool("canFlicker", flicker); 
    }

    void updateLight()
    {
        Debug.Log("updateLight");
        if (dayNightCycle.curDayTime == DayNightCycle.DayTime.sunset)
        {
            if (isOn)
                return;
            isOn = true;
            animator.SetBool("isOn", isOn);
        }
        else if (dayNightCycle.curDayTime == DayNightCycle.DayTime.sunrise)
        {
            if (!isOn)
                return;
            isOn = false;
            animator.SetBool("isOn", isOn);
        }
    }

    private void Update()
    {
        if (!flicker)
            return;
        if (!isOn)
            return;
        if (Random.Range(1, 100) == 1)
        {
            animator.SetTrigger("doFlicker");
        }
    }
}
