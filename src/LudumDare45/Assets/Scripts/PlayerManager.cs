using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("config")]
    public float hpReduceRate = 1f;
    public float loopTime = 4f;
    public PlayerUpgrades upgrades;
    public PlayerMovment movment;

    [Header("current stats")]
    public float hp = 100;
    public float hunger = 100;
    public float thirst = 100;
    public float energy = 100;
    public int money = 0;

    [Header("UI/slider")]
    public Slider hpSlider;
    public Slider hungerSlider;
    public Slider thirstSlider;
    public Slider energySlider;

    public static PlayerManager Instance;

    public delegate void OnShopInteraction();
    public OnShopInteraction onShopInteractionCallback;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #region add stats
    public void addHP(float cnt)
    {
        hp = hp + cnt;
        if (hp > upgrades.maxHP)
        {
            hp = upgrades.maxHP;
        }
        hpSlider.value = (hp/ upgrades.maxHP);
    }
    public void addHunger(float cnt)
    {
        hunger = hunger + cnt;
        if (hunger > upgrades.maxHunger)
        {
            hunger = upgrades.maxHunger;
        }
        hungerSlider.value = hunger/ upgrades.maxHunger;
    }
    public void addThirst(float cnt)
    {
        thirst = thirst + cnt;
        if (thirst > upgrades.maxThirst)
        {
            thirst = upgrades.maxThirst;
        }
        thirstSlider.value = thirst/ upgrades.maxThirst;
    }
    public void addEnergy(float cnt)
    {
        energy = energy + cnt;
        if (energy > upgrades.maxEnergy)
        {
            energy = upgrades.maxEnergy;
        }
        energySlider.value = energy;
    }
    public void addMoney(int m)
    {
        money += m;
    }
    #endregion

    #region func reduce stats
    public void reduceHP(float cnt)
    {
        hp = hp - cnt;
        hpSlider.value = hp/upgrades.maxHP;
        if (hp <= 0)
        {
            //TODO: GAME OVER
            Debug.Log("GAME OVER!!!");
        }
    }
    public void reduceHunger(float cnt)
    {
        hunger = hunger - cnt;
        hungerSlider.value = hunger/upgrades.maxHunger;
        if (hunger <= 0)
        {
            //TODO
        }
    }
    public void reduceThirst(float cnt)
    {
        thirst = thirst - cnt;
        thirstSlider.value = thirst/upgrades.maxThirst;
        if (thirst <= 0)
        {
            //TODO
        }
    }
    public void reduceEnergy(float cnt)
    {
        energy = energy - cnt;
        energySlider.value = energy / upgrades.maxEnergy;
        if (hp <= 0)
        {
            //TODO
        }
    }
    public bool reduceMoney(int m)
    {
        if (!checkMoney(m))
            return false;
        money -= m;
        return true;
    }
    #endregion

    #region var checks
    public bool checkEnergy(float e)
    {
        return energy >= e;
    }
    public bool checkHunger(float h)
    {
        return hunger >= h;
    }
    public bool checkThirst(float t)
    {
        return thirst >= t;
    }
    public bool checkMoney(int m)
    {
        return money >= m;
    }
    #endregion

    private void decreaseStats()
    {
        reduceHunger(upgrades.hungerLostRate);
        reduceThirst(upgrades.thirstLostRate);
        if(hunger <= 0 || thirst <= 0)
        {
            reduceHP(hpReduceRate);
            Debug.Log("hunger/thirst < 0 REDUCE HP");
        }
        Debug.Log("reduce stats loop");
    }

    private void Start()
    {
        hpSlider.value = hp / upgrades.maxHP;
        hungerSlider.value = hunger / upgrades.maxHunger;
        thirstSlider.value = thirst / upgrades.maxThirst;
        energySlider.value = energy / upgrades.maxEnergy;

        InvokeRepeating("decreaseStats", loopTime, loopTime);  //1s delay, repeat every 1s
    }

    public bool facingRight()
    {
        return movment.lookRight;
    }
}
