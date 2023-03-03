using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    public class DrillBit
    {
        public int damage;
        public int dropChance;

        public DrillBit(int damage, int dropChance)
        {
            this.damage = damage;
            this.dropChance = dropChance;
        }
    }
    
    public class Battery
    {
        public int capacity;
        public int efficiency;

        public Battery(int capacity, int efficiency)
        {
            this.capacity = capacity;
            this.efficiency = efficiency;
        }
    }

    public class Motor
    {
        int damage;
    }

    public class Gearbox
    {

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
