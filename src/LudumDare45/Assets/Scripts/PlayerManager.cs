using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("config")]
    public float hpReduceRate = 1f;
    public float loopTime = 4f;
    private PlayerUpgrades upgrades;
    public PlayerMovment movment;

    [Header("current stats")]
    public float hp = 100;
    public float hunger = 100;
    public float thirst = 100;
    public float energy = 100;
    public float hygiene = 100;
    public int money = 0;

    public static PlayerManager Instance;

    public delegate void OnShopInteraction();
    public OnShopInteraction onShopInteractionCallback;

    public delegate void OnStatsChange();
    public OnStatsChange onStatsChangeCallback;

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
        onStatsChangeCallback.Invoke();
    }
    public void addHunger(float cnt)
    {
        hunger = hunger + cnt;
        if (hunger > upgrades.maxHunger)
        {
            hunger = upgrades.maxHunger;
        }
        onStatsChangeCallback.Invoke();
    }
    public void fillUpHunger()
    {
        hunger = upgrades.maxHunger;
        onStatsChangeCallback.Invoke();
    }
    public void addThirst(float cnt)
    {
        thirst = thirst + cnt;
        if (thirst > upgrades.maxThirst)
        {
            thirst = upgrades.maxThirst;
        }
        onStatsChangeCallback.Invoke();
    }
    public void fillUpThirst()
    {
        thirst = upgrades.maxThirst;
        onStatsChangeCallback.Invoke();
    }
    public void addEnergy(float cnt)
    {
        energy = energy + cnt;
        if (energy > upgrades.maxEnergy)
        {
            energy = upgrades.maxEnergy;
        }   
        onStatsChangeCallback.Invoke();
    }
    public void fillUpEnergy()
    {
        energy = upgrades.maxEnergy;
        onStatsChangeCallback.Invoke();
    }
    public void addHygiene(float cnt)
    {
        hygiene = hygiene + cnt;
        if (hygiene > upgrades.maxHygiene)
        {
            hygiene = upgrades.maxHygiene;
        }
        onStatsChangeCallback.Invoke();
    }
    public void fillUpHygiene()
    {
        hygiene = upgrades.maxHygiene;
        onStatsChangeCallback.Invoke();
    }
    public void addMoney(int m)
    {
        money += m;
        onStatsChangeCallback.Invoke();
    }
    #endregion

    #region func reduce stats
    public void reduceHP(float cnt)
    {
        hp = hp - cnt;
        onStatsChangeCallback.Invoke();
        if (hp <= 0)
        {
            //TODO: GAME OVER
            Debug.Log("GAME OVER!!!");
        }
    }
    public void reduceHunger(float cnt)
    {
        hunger = hunger - cnt;
        onStatsChangeCallback.Invoke();
        if (hunger <= 0)
        {
            //TODO
        }
    }
    public void reduceThirst(float cnt)
    {
        thirst = thirst - cnt;
        onStatsChangeCallback.Invoke();
        if (thirst <= 0)
        {
            //TODO
        }
    }
    public void reduceEnergy(float cnt)
    {
        energy = energy - cnt;
        onStatsChangeCallback.Invoke();
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
        upgrades = PlayerUpgrades.instance;
        InvokeRepeating("decreaseStats", loopTime, loopTime);  //1s delay, repeat every 1s
    }

    public bool facingRight()
    {
        return movment.lookRight;
    }

    public void freezePlayer(bool b)
    {
        movment.StopMovment(b);
        movment.StopIntract(b);
    }
}
