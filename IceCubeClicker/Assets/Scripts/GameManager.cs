using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public int ice;

    public int currentPickaxe;

    public int pickPower;
    public int iceMultiplier;
    public int bonusIce;
    public float critChance;
    public float critDamage;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } 
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ice = 0;
        currentPickaxe = 3;
        pickPower = 1;
        bonusIce = 1;
        iceMultiplier = 1;
        critChance = 0f;
        critDamage = 100f;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
