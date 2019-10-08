using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle Instance;

    public Animator animator;
    public float normalizedTime = 0;
    public float dayTime = 60;
    public float gameTimer = 0;
    public bool pauseTime;
    public string realTimeDisplay;

    public enum DayTime { sunrise, noon, sunset, midnight }
    public int dayTimeCnt = Enum.GetValues(typeof(DayTime)).Length;
    public DayTime curDayTime;

    public delegate void OnDayTimeChange();
    public OnDayTimeChange onDayTimeChangeCallback;

    private float speed = 1;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        //setGameTimer(gameTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseTime)
            return;

        gameTimer = (gameTimer + Time.deltaTime* speed) % dayTime;
        normalizedTime = Mathf.Clamp01(gameTimer / dayTime);

        animator.SetFloat("gameTime", normalizedTime);
        calcRealTimeDisplay();
        calcTimeDisplay();

    }

    public void fastForward()
    {
        speed = 10;
    }
    public void normalTime()
    {
        speed = 1;
    }

    void calcRealTimeDisplay()
    {
        float realTime = (1440 * normalizedTime);
        float hours = Mathf.Floor(realTime / 60);
        float minutes = Mathf.RoundToInt(realTime % 60);

        if (hours < 10)
        {
            realTimeDisplay = "0" + hours.ToString() + ":";
        }
        else
        {
            realTimeDisplay = hours.ToString() + ":";
        }
        if (minutes < 10)
        {
            realTimeDisplay += "0" + Mathf.RoundToInt(minutes).ToString();
        }
        else
        {
            realTimeDisplay += Mathf.RoundToInt(minutes).ToString();
        }

    }

    void calcTimeDisplay()
    {
        DayTime newTime = curDayTime;
        if(normalizedTime < 0.16f)
        {
            newTime = DayTime.sunrise;
        }else if (normalizedTime < 0.5f)
        {
            newTime = DayTime.noon;
        }else if (normalizedTime < 0.66f)
        {
            newTime = DayTime.sunset;
        }else{
            newTime = DayTime.midnight;
        }

        if(!newTime.Equals(curDayTime))
        {
            curDayTime = newTime; 
            if (onDayTimeChangeCallback != null)
            {
                onDayTimeChangeCallback.Invoke();
            }
        }
    }

    

}
