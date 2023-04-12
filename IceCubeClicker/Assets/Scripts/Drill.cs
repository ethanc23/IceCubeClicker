using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;
using static UnityEditor.Experimental.GraphView.Port;

public class Drill : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Sprite[] partSprites;
    public enum PartType
    {
        drillBit,
        battery,
        motor,
        gearbox
    }

    public enum ModifierType
    {
        damage,
        efficiency,
        capacity,
        speed,
        partDropChance,
        bonusIce
    }

    public struct Modifier
    {
        public int value;
        public ModifierType type;

        public Modifier(int value, ModifierType type)
        {
            this.value = value;
            this.type = type;
        }
    }

    public class Part
    {
        public string name;
        public Sprite sprite;
        public PartType type;
        public Modifier implicit1;
        public Modifier implicit2;

        public Part(string name, Sprite sprite, PartType type, Modifier implicit1, Modifier implicit2)
        {
            this.name = name;
            this.type = type;
            this.sprite = sprite;
            this.implicit1 = implicit1;
            this.implicit2 = implicit2;
        }
    }

    public Part[] drillParts = new Part[4];

    public List<Part> partInventory = new();

    public Part GeneratePart()
    {
        ModifierType[,] modifiers =
        {
            {ModifierType.damage, ModifierType.partDropChance},
            {ModifierType.capacity, ModifierType.efficiency},
            {ModifierType.damage, ModifierType.speed},
            {ModifierType.speed, ModifierType.bonusIce}
        };
        int partType = Random.Range(0, 4);
        int implicit1Value = 1;
        int implicit2Value = 1;
        int implicit1Type = Random.Range(0, 1);
        int implicit2Type = Random.Range(0, 1);
        string name = "foo";
        return new Part(name, partSprites[partType], (PartType)partType, new(implicit1Value, modifiers[partType, implicit1Type]), new(implicit2Value, modifiers[partType, implicit2Type]));
    }

    public void AddPartToInventory(Part part)
    {
        partInventory.Add(part);
        uiManager.partInventorySprite(part.sprite, partInventory.Count - 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        drillParts[0] = new("Rusty DrillBit", partSprites[0], PartType.drillBit, new(1, ModifierType.damage), new(1, ModifierType.partDropChance));
        drillParts[1] = new("Rusty Battery", partSprites[1], PartType.battery, new(1, ModifierType.capacity), new(1, ModifierType.efficiency));
        drillParts[2] = new("Rusty Motor", partSprites[2], PartType.motor, new(1, ModifierType.damage), new(1, ModifierType.speed));
        drillParts[3] = new("Rusty Gearbox", partSprites[3], PartType.gearbox, new(1, ModifierType.speed), new(1, ModifierType.bonusIce));
        SetPartEffects();
    }

    public void SetPartEffects()
    {
        GameManager.Instance.drillDamage = 0;
        GameManager.Instance.drillBatteryEfficiency = 0;
        GameManager.Instance.drillBatteryCapacity = 0;
        GameManager.Instance.drillBonusIce = 0;
        GameManager.Instance.drillSpeed = 0;
        GameManager.Instance.partDropChance = 0;
        
        foreach (Part part in drillParts)
        {
            switch (part.implicit1.type)
            {
                case ModifierType.damage: { GameManager.Instance.drillDamage += part.implicit1.value; } break;
                case ModifierType.efficiency: { GameManager.Instance.drillBatteryEfficiency += part.implicit1.value;  } break;
                case ModifierType.capacity: { GameManager.Instance.drillBatteryCapacity += part.implicit1.value; } break;
                case ModifierType.bonusIce: { GameManager.Instance.bonusIce += part.implicit1.value; } break;
                case ModifierType.speed: { GameManager.Instance.drillSpeed += part.implicit1.value; } break;
                case ModifierType.partDropChance: { GameManager.Instance.partDropChance += part.implicit1.value; } break;
                default: { throw new Exception(part.name + " has an invalid type for implicit1"); }
            }
            switch (part.implicit2.type)
            {
                case ModifierType.damage: { GameManager.Instance.drillDamage += part.implicit2.value; } break;
                case ModifierType.efficiency: { GameManager.Instance.drillBatteryEfficiency += part.implicit2.value; } break;
                case ModifierType.capacity: { GameManager.Instance.drillBatteryCapacity += part.implicit2.value; } break;
                case ModifierType.bonusIce: { GameManager.Instance.bonusIce += part.implicit2.value; } break;
                case ModifierType.speed: { GameManager.Instance.drillSpeed += part.implicit2.value; } break;
                case ModifierType.partDropChance: { GameManager.Instance.partDropChance += part.implicit2.value; } break;
                default: { throw new Exception(part.name + " has an invalid type for implicit2"); }
            }
        }
    }
}
