using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField] private IceCube iceCube;
    [SerializeField] private UIManager uiManager;

    private Controls ctrl;

    public int ice;
    public int height;
    public int maxHeight;

    public int currentPickaxe;

    public int basePickPower;
    public IDictionary<int, int> pickaxePowers = new Dictionary<int, int>();
    public int pickPower;
    public int iceMultiplier;
    public int bonusIce;
    public float critChance;
    public float critDamage;
    public float partDropChance;

    public int autoMineDamage;

    public int drillDamage = 0;
    public float drillBatteryEfficiency = 0;
    public int drillBatteryCapacity = 0;
    public float drillSpeed = 0;
    public float drillPartDropChance = 0;
    public int drillBonusIce = 0;

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
        ctrl = new Controls();

        ice = 100000;
        height = 0;
        maxHeight = 0;
        currentPickaxe = 0;
        basePickPower = 1;
        pickPower = 0;
        bonusIce = 0;
        iceMultiplier = 1;
        critChance = 0f;
        critDamage = 2.0f;
        partDropChance = 1f;

        pickaxePowers[1] = 5;
        pickaxePowers[2] = 10;
        pickaxePowers[3] = 25;
        pickaxePowers[4] = 100;
        pickaxePowers[5] = 250;
        pickaxePowers[6] = 2000;

        autoMineDamage = 0;

        ctrl.Default.LeftClick.performed += LeftClick;
        ctrl.Default.RightClick.performed += RightClick;
        ctrl.Enable();
    }

    private void LeftClick(InputAction.CallbackContext context)
    {
        iceCube.Click(ctrl.Default.MousePos.ReadValue<Vector2>());
    }
    private void RightClick(InputAction.CallbackContext context)
    {
        uiManager.DrillPopup(new Vector2(Screen.width, Screen.height) - ctrl.Default.MousePos.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
