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
    [SerializeField] public Sprite[] partSprites;

    public bool drillOwned;

    public int drillPower;

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
        public float value;
        public ModifierType type;

        public Modifier(float value, ModifierType type)
        {
            this.value = value;
            this.type = type;
        }
    }

    private struct Range
    {
        public float min;
        public float max;

        public Range(float max, float min)
        {
            this.min = min;
            this.max = max;
        }
    }

    private readonly Range[] implicitRanges = { new(0f,1f), new(0f,1f), new(0f,1f), new(0f,1f), new(0f,1f), new(0f,1f) };
    private readonly string[] implicit1Names = { "damaging", "battery efficient", "battery capacity", "speedy", "lucky", "icy" };
    private readonly string[] implicit2Names = { "damage", "battery efficiency", "battery", "speed", "luck", "extra ice" };

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
        ModifierType implicit1Type = modifiers[partType, Random.Range(0, 2)];
        ModifierType implicit2Type = modifiers[partType, Random.Range(0, 2)];
        float implicit1Value = Random.Range(implicitRanges[(int)implicit1Type].min, implicitRanges[(int)implicit1Type].max);
        float implicit2Value = Random.Range(implicitRanges[(int)implicit2Type].min, implicitRanges[(int)implicit2Type].max);
        string name = implicit1Names[(int)implicit1Type] + " " + ((PartType)partType).ToString() + " of " + implicit2Names[(int)implicit2Type];
        return new Part(name, partSprites[partType], (PartType)partType, new(implicit1Value, implicit1Type), new(implicit2Value, implicit2Type));
    }

    public void AddPartToInventory(Part part)
    {
        partInventory.Add(part);
        uiManager.PartInventorySprite(part.sprite, partInventory.Count - 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        drillOwned = false;
        drillParts[0] = new("Rusty DrillBit", partSprites[0], PartType.drillBit, new(1, ModifierType.damage), new(0.005f, ModifierType.partDropChance));
        drillParts[1] = new("Rusty Battery", partSprites[1], PartType.battery, new(1, ModifierType.capacity), new(1, ModifierType.efficiency));
        drillParts[2] = new("Rusty Motor", partSprites[2], PartType.motor, new(1, ModifierType.damage), new(1, ModifierType.speed));
        drillParts[3] = new("Rusty Gearbox", partSprites[3], PartType.gearbox, new(1, ModifierType.speed), new(1, ModifierType.bonusIce));
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
                case ModifierType.damage: { GameManager.Instance.drillDamage += (int)part.implicit1.value; } break;
                case ModifierType.efficiency: { GameManager.Instance.drillBatteryEfficiency += part.implicit1.value;  } break;
                case ModifierType.capacity: { GameManager.Instance.drillBatteryCapacity += (int)part.implicit1.value; } break;
                case ModifierType.bonusIce: { GameManager.Instance.bonusIce += (int)part.implicit1.value; } break;
                case ModifierType.speed: { GameManager.Instance.drillSpeed += part.implicit1.value; } break;
                case ModifierType.partDropChance: { GameManager.Instance.drillPartDropChance += part.implicit1.value; } break;
                default: { throw new Exception(part.name + " has an invalid type for implicit1"); }
            }
            switch (part.implicit2.type)
            {
                case ModifierType.damage: { GameManager.Instance.drillDamage += (int)part.implicit2.value; } break;
                case ModifierType.efficiency: { GameManager.Instance.drillBatteryEfficiency += part.implicit2.value; } break;
                case ModifierType.capacity: { GameManager.Instance.drillBatteryCapacity += (int)part.implicit2.value; } break;
                case ModifierType.bonusIce: { GameManager.Instance.bonusIce += (int)part.implicit2.value; } break;
                case ModifierType.speed: { GameManager.Instance.drillSpeed += part.implicit2.value; } break;
                case ModifierType.partDropChance: { GameManager.Instance.drillPartDropChance += part.implicit2.value; } break;
                default: { throw new Exception(part.name + " has an invalid type for implicit2"); }
            }
        }
    }
}
