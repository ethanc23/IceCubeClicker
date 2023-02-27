using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Upgrades : MonoBehaviour
{
    public IceCube iceCube;
    [SerializeField] private HealthBar healthBar;

    public int strongerPickLvl;
    public int efficientMiningLvl;
    public int critMineChanceLvl;
    public int critMineDamageLvl;

    public int strongerPickCost;
    public int efficientMiningCost;
    public int critMineChanceCost;
    public int critMineDamageCost;

    public int autoMinerLvl;

    public int autoMinerCost;

    // Start is called before the first frame update
    void Start()
    {
        strongerPickLvl = 1;
        efficientMiningLvl = 1;
        critMineChanceLvl = 1;
        critMineDamageLvl = 1;

        strongerPickCost = 10;
        efficientMiningCost = 10;
        critMineChanceCost = 100;
        critMineDamageCost = 100;

        autoMinerLvl = 1;
        autoMinerCost = 450;
    }

    public void strongerPick()
    {
        if (GameManager.Instance.ice >= strongerPickCost)
        {
            GameManager.Instance.ice -= strongerPickCost;
            GameManager.Instance.pickPower++;
            strongerPickLvl++;
            strongerPickCost = (int)Mathf.Floor(15 * Mathf.Pow(strongerPickLvl, 1.2f));

        }
    }

    public void efficientMining()
    {
        if (GameManager.Instance.ice >= efficientMiningCost)
        {
            GameManager.Instance.ice -= efficientMiningCost;
            GameManager.Instance.bonusIce++;
            efficientMiningLvl++;
            efficientMiningCost = 10 * efficientMiningLvl;
        }
    }

    public void critMineChance()
    {
        if (GameManager.Instance.ice >= critMineChanceCost)
        {
            GameManager.Instance.ice -= critMineChanceCost;
            GameManager.Instance.critChance += 0.5f;
            critMineChanceLvl++;
            //Improve
            critMineChanceCost = 100 + (int)Mathf.Floor(200 * (critMineChanceLvl-1));
        }
    }

    public void critMineDamage()
    {
        if (GameManager.Instance.ice >= critMineDamageCost)
        {
            GameManager.Instance.ice -= critMineDamageCost;
            GameManager.Instance.critDamage += 0.1f;
            critMineDamageLvl++;
            critMineDamageCost = 100 + (int)Mathf.Floor(50 * Mathf.Pow(critMineDamageLvl, 1.5f));
        }
    }

    public void autoMiner()
    {
        if (GameManager.Instance.ice >= autoMinerCost)
        {
            GameManager.Instance.ice -= autoMinerCost;
            GameManager.Instance.autoMineDamage += 2;
            autoMinerLvl++;
            autoMinerCost = 450 + 75 * autoMinerLvl;
        }
    }
}
