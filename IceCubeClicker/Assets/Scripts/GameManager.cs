using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public int ice;
    public int depth;

    public int currentPickaxe;

    public int basePickPower;
    public IDictionary<int, int> pickaxePowers = new Dictionary<int, int>();
    public int pickPower;
    public int iceMultiplier;
    public int bonusIce;
    public float critChance;
    public float critDamage;

    public int autoMineDamage;

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
        depth = 0;
        currentPickaxe = 0;
        basePickPower = 1;
        pickPower = 0;
        bonusIce = 1;
        iceMultiplier = 1;
        critChance = 0f;
        critDamage = 2.0f;

        pickaxePowers[1] = 5;
        pickaxePowers[2] = 10;
        pickaxePowers[3] = 25;
        pickaxePowers[4] = 100;
        pickaxePowers[5] = 250;
        pickaxePowers[6] = 2000;

        autoMineDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
