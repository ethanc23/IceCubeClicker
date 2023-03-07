using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class Drill : MonoBehaviour
{
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
        public PartType type;
        public Modifier implicit1;
        public Modifier implicit2;

        public Part(string name, PartType type, Modifier implicit1, Modifier implicit2)
        {
            this.type = type;
            this.implicit1 = implicit1;
            this.implicit2 = implicit2;
            this.name = name;
        }
    }

    public Part[] drillParts = new Part[4];

    public Part[] partInventory;

    public Part GeneratePart()
    {
        ModifierType[,] modifiers =
        {
            {ModifierType.damage, ModifierType.partDropChance},
            {ModifierType.capacity, ModifierType.efficiency},
            {ModifierType.damage, ModifierType.speed},
            {ModifierType.speed, ModifierType.bonusIce}
        };
        int partType = Random.Range(0, 3);
        int implicit1Value = 1;
        int implicit2Value = 1;
        int implicit1Type = Random.Range(0, 1);
        int implicit2Type = Random.Range(0, 1);

        string name = "foo";

        return new Part(name, (PartType)partType, new(implicit1Value, modifiers[partType, implicit2Type]), new(implicit2Value, modifiers[partType, implicit2Type]));
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            drillParts[i].type = (PartType)i;
            switch(drillParts[i].type)
            {
                case PartType.drillBit:
                    drillParts[i].name = "Rusty DrillBit";
                    drillParts[i].implicit1 = new(1, ModifierType.damage);
                    drillParts[i].implicit2 = new(1, ModifierType.partDropChance);
                    break;
                case PartType.battery:
                    drillParts[i].name = "Rusty Battery";
                    drillParts[i].implicit1 = new(1, ModifierType.capacity);
                    drillParts[i].implicit2 = new(1, ModifierType.efficiency);
                    break;
                case PartType.motor:
                    drillParts[i].name = "Rusty Motor";
                    drillParts[i].implicit1 = new(1, ModifierType.damage);
                    drillParts[i].implicit2 = new(1, ModifierType.speed);
                    break;
                case PartType.gearbox:
                    drillParts[i].name = "Rusty Gearbox";
                    drillParts[i].implicit1 = new(1, ModifierType.speed);
                    drillParts[i].implicit2 = new(1, ModifierType.bonusIce);
                    break;
                default: { throw new System.Exception("Drill part type is null"); }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
