using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public static PlayerUpgrades instance = null;

    public float maxHP = 100;
    public float maxHunger = 100;
    public float maxThirst = 100;
    public float maxEnergy = 100;

    public float hungerLostRate = 1f;
    public float thirstLostRate = 1f;
    public float energyLostRate = 1f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
