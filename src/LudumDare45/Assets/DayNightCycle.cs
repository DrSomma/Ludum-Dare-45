using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Animator animator;
    public float normalizedTime = 0;
    public float timeDay = 60;
    public float timeNight = 60;
    public float timeSunset = 30;
    public float timeSunrise = 30;
    public float gameTimer = 0;
    public bool pauseTime;

    public enum DayTime { sunrise, noon, sunset, midnight }
    public DayTime curDayTime;

    public delegate void OnDayTimeChange();
    public OnDayTimeChange onDayTimeChangeCallback;


    private float timerAnimation;
    private float onDayTime = 0;
    // Use this for initialization

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        onDayTime = timeDay + timeNight + timeSunset + timeSunrise;
        setGameTimer(gameTimer);
    }
    private float test = 0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(test);
            animator.SetFloat("gameTime", test);
            test = test + 0.1f;
        }

        if (pauseTime)
            return;

        timerAnimation = timerAnimation + Time.deltaTime;
        gameTimer = (gameTimer + Time.deltaTime) % onDayTime;

        switch (curDayTime)
        {
            case DayTime.sunrise:
                setAnimation(timeSunrise);
                checkDayTimeChange(timeSunrise,DayTime.noon);
                break;
            case DayTime.noon:
                break;
            case DayTime.sunset:
                break;
            case DayTime.midnight:
                setAnimation(timeSunset);
                break;
            default:
                Debug.Log("Fail!");
                break;
        }
    }

    public void setGameTimer(float timer)
    {
        if (timer > onDayTime)
            timer = 0;
        gameTimer = timer;

        //Check witch time of the day
        float sunR = 0 + timeSunrise;
        float noo = sunR + timeDay;
        float sunS = noo + timeSunset;
        float mid = sunS + timeNight;

        Debug.Log("Set Light to: " + gameTimer);

        if (gameTimer <= sunR)
        {
            curDayTime = DayTime.sunrise;
            timerAnimation = sunR - gameTimer;
            setAnimation(timeSunrise);
        }else if (gameTimer <= noo){
            curDayTime = DayTime.noon;
            timerAnimation = noo - gameTimer;
            normalizedTime = 1;
            animator.SetFloat("gameTime", 0.5f);
        }else if(gameTimer <= sunS)
        {
            curDayTime = DayTime.sunset;
            timerAnimation = sunS - gameTimer;
            setAnimation(timeSunset);
        }else if (gameTimer <= mid)
        {
            curDayTime = DayTime.midnight;
            timerAnimation = mid - gameTimer;
            normalizedTime = 0;
            animator.SetFloat("gameTime", 0f);
        }
    }


    private void checkDayTimeChange(float maxTime, DayTime nextTime)
    {
        if (timerAnimation >= maxTime)
        {
            timerAnimation = 0;
            curDayTime = nextTime;
            if (onDayTimeChangeCallback != null)
                onDayTimeChangeCallback.Invoke();
        }

    }

    private void setAnimation(float maxTime)
    {
        normalizedTime = Mathf.Clamp01(timerAnimation / maxTime);
        animator.SetFloat("gameTime", normalizedTime);
    }

}
