using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using Unity.Mathematics;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Upgrades upgrades;
    [SerializeField] private IceCube iceCube;
    [SerializeField] private Sprite[] pickaxeSprites;

    private VisualElement pickaxeWindow;

    private Label iceCount;
    private Label height;

    private VisualElement drillWindow;
    private VisualElement upgradesWindow;
    private Button drillButton;
    private Button drillClose;
    private Button upgradesButton;
    private Button liftButton;

    private Button up;
    private Button down;

    private int liftCost;

    private Label effMinNum;
    private Label strPicNum;
    private Label morCrtNum;
    private Label strCrtNum;

    private Label autMinNum;

    private Label effMinCost;
    private Label strPicCost;
    private Label morCrtCost;
    private Label strCrtCost;

    private Label autMinCost;

    private Button effMinButton;
    private Button strPicButton;
    private Button morCrtButton;
    private Button strCrtButton;

    private Button autMinButton; 

    private Button pickaxeButton;
    private VisualElement pickaxeImage;

    private readonly IDictionary<int, Button> pickSelect = new Dictionary<int, Button>();

    private Button stonePickBuy;
    private Button copperPickBuy;
    private Button bronzePickBuy;
    private Button ironPickBuy;
    private Button steelPickBuy;
    private Button titaniumPickBuy;

    private Button drillBitButton;
    private Button drillBatteryButton;
    private Button drillMotorButton;
    private Button drillGearboxButton;
    private Button drillBaseButton;

    private int stonePickCost;
    private int copperPickCost;
    private int bronzePickCost;
    private int ironPickCost;
    private int steelPickCost;
    private int titaniumPickCost;

    private bool stonePickOwned;
    private bool copperPickOwned;
    private bool bronzePickOwned;
    private bool ironPickOwned;
    private bool steelPickOwned;
    private bool titaniumPickOwned;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        pickaxeWindow = root.Q<VisualElement>("pickaxeWindow");
        
        iceCount = root.Q<Label>("iceCount");
        height = root.Q<Label>("heightLabel");

        drillWindow = root.Q<VisualElement>("drillWindow");
        upgradesWindow = root.Q<VisualElement>("upgradesWindow");
        drillButton = root.Q<Button>("drillButton");
        drillClose = root.Q<Button>("drillExit");
        upgradesButton = root.Q<Button>("upgradesButton");
        liftButton = root.Q<Button>("liftButton");

        up = root.Q<Button>("upButton");
        down = root.Q<Button>("downButton");

        effMinNum = root.Q<Label>("effMinNum");
        strPicNum = root.Q<Label>("strPicNum");
        morCrtNum = root.Q<Label>("morCrtNum");
        strCrtNum = root.Q<Label>("strCrtNum");

        autMinNum = root.Q<Label>("autMinNum");

        effMinCost = root.Q<Label>("effMinCost");
        strPicCost = root.Q<Label>("strPicCost");
        morCrtCost = root.Q<Label>("morCrtCost");
        strCrtCost = root.Q<Label>("strCrtCost");

        autMinCost = root.Q<Label>("autMinCost");

        effMinButton = root.Q<Button>("effMinButton");
        strPicButton = root.Q<Button>("strPicButton");
        morCrtButton = root.Q<Button>("morCrtButton");
        strCrtButton = root.Q<Button>("strCrtButton");

        autMinButton = root.Q<Button>("autMinButton");

        pickaxeButton = root.Q<Button>("pickaxeButton");
        pickaxeImage = pickaxeButton.ElementAt(0);

        stonePickBuy = root.Q<Button>("stonePickBuy");
        copperPickBuy = root.Q<Button>("copperPickBuy");
        bronzePickBuy = root.Q<Button>("bronzePickBuy");
        ironPickBuy = root.Q<Button>("ironPickBuy");
        steelPickBuy = root.Q<Button>("steelPickBuy");
        titaniumPickBuy = root.Q<Button>("titaniumPickBuy");

        pickSelect[1] = root.Q<Button>("stonePickSelect");
        pickSelect[2] = root.Q<Button>("copperPickSelect");
        pickSelect[3] = root.Q<Button>("bronzePickSelect");
        pickSelect[4] = root.Q<Button>("ironPickSelect");
        pickSelect[5] = root.Q<Button>("steelPickSelect");
        pickSelect[6] = root.Q<Button>("titaniumPickSelect");

        drillButton.clicked += DrillWindow;
        drillClose.clicked += DrillWindow;
        upgradesButton.clicked += UpgradesWindow;
        liftButton.clicked += LiftButton;

        up.clicked += Up;
        down.clicked += Down;

        effMinButton.clicked += upgrades.efficientMining;
        strPicButton.clicked += upgrades.strongerPick;
        morCrtButton.clicked += upgrades.critMineChance;
        strCrtButton.clicked += upgrades.critMineDamage;

        autMinButton.clicked += upgrades.autoMiner;

        pickaxeButton.clicked += PickaxeWindow;

        stonePickBuy.clicked += StonePickBuy;
        copperPickBuy.clicked += CopperPickBuy;
        bronzePickBuy.clicked += BronzePickBuy;
        ironPickBuy.clicked += IronPickBuy;
        steelPickBuy.clicked += SteelPickBuy;
        titaniumPickBuy.clicked += TitaniumPickBuy;

        pickSelect[1].clicked += StonePickSelect;
        pickSelect[2].clicked += CopperPickSelect;
        pickSelect[3].clicked += BronzePickSelect;
        pickSelect[4].clicked += IronPickSelect;
        pickSelect[5].clicked += SteelPickSelect;
        pickSelect[6].clicked += TitaniumPickSelect;

        upgradesWindow.visible = false;
        pickaxeWindow.visible = false;
        drillWindow.visible = false;
        up.visible = false;
        down.visible = false;

        liftCost = 25;

        stonePickCost = 50;
        copperPickCost = 200;
        bronzePickCost = 1000;
        ironPickCost = 5000;
        steelPickCost = 10000;
        titaniumPickCost = 1000000;

        stonePickOwned = false;
        copperPickOwned = false;
        bronzePickOwned = false;
        ironPickOwned = false;
        steelPickOwned = false;
        titaniumPickOwned = false;

        stonePickBuy.text = stonePickCost.ToString();
        copperPickBuy.text = copperPickCost.ToString();
        bronzePickBuy.text = bronzePickCost.ToString();
        ironPickBuy.text = ironPickCost.ToString();
        steelPickBuy.text = steelPickCost.ToString();
        titaniumPickBuy.text = titaniumPickCost.ToString();
    }

    private void DrillWindow()
    {
        if (drillWindow.visible == false)
        {
            drillWindow.visible = true;
            return;
        }
        drillWindow.visible = false;
    }

    private void UpgradesWindow()
    {
        if (upgradesWindow.visible == false)
        {
            upgradesWindow.visible = true;
            return;
        }
        upgradesWindow.visible = false;
    }

    private void LiftButton()
    {
        if (GameManager.Instance.ice >= liftCost)
        {
            GameManager.Instance.ice -= liftCost;
            GameManager.Instance.maxHeight++;
            //Improve
            liftCost += liftCost;
            up.visible = true;
        }
    }

    private void Up()
    {
        if (GameManager.Instance.maxHeight > GameManager.Instance.height)
        {
            GameManager.Instance.height++;
            iceCube.maxHp = 5 * (int)Mathf.Exp(GameManager.Instance.height);
            iceCube.hp = iceCube.maxHp;
            down.visible = true;
            if (GameManager.Instance.maxHeight == GameManager.Instance.height)
            {
                up.visible = false;
            }
        }
    }

    private void Down()
    {
        if (GameManager.Instance.height > 0)
        {
            GameManager.Instance.height--;
            iceCube.maxHp = 5 * (int)Mathf.Exp(GameManager.Instance.height);
            iceCube.hp = iceCube.maxHp;
            up.visible = true;
            if (GameManager.Instance.height == 0)
            {
                down.visible = false;
            }
        }
    }

    private void pickBorder(int pick)
    {
        foreach (KeyValuePair<int, Button> pair in pickSelect)
        {
            if (pair.Key == pick)
            {
                pickSelect[pair.Key].style.borderLeftWidth = 3;
                pickSelect[pair.Key].style.borderRightWidth = 3;
                pickSelect[pair.Key].style.borderBottomWidth = 3;
                pickSelect[pair.Key].style.borderTopWidth = 3;
            }
            else
            {
                pickSelect[pair.Key].style.borderLeftWidth = 0;
                pickSelect[pair.Key].style.borderRightWidth = 0;
                pickSelect[pair.Key].style.borderBottomWidth = 0;
                pickSelect[pair.Key].style.borderTopWidth = 0;
            }
        }
    }

    private void PickaxeWindow()
    {
        if (pickaxeWindow.visible == false)
        {
            pickaxeWindow.visible = true;
            return;
        } 
        pickaxeWindow.visible = false;
    }

    private void StonePickBuy()
    {
        if (GameManager.Instance.ice >= stonePickCost)
        {
            GameManager.Instance.ice -= stonePickCost;
            stonePickOwned = true;
            StonePickSelect();
            pickSelect[1].style.backgroundColor = new Color(0.15f, 0.15f, 0.15f);
            stonePickBuy.text = "Owned";
        }
    }

    private void CopperPickBuy()
    {
        if (GameManager.Instance.ice >= copperPickCost)
        {
            GameManager.Instance.ice -= copperPickCost;
            copperPickOwned = true;
            CopperPickSelect();
            pickSelect[2].style.backgroundColor = new Color(0.15f, 0.15f, 0.15f);
            copperPickBuy.text = "Owned";
        }
    }

    private void BronzePickBuy()
    {
        if (GameManager.Instance.ice >= bronzePickCost)
        {
            GameManager.Instance.ice -= bronzePickCost;
            bronzePickOwned = true;
            BronzePickSelect();
            pickSelect[3].style.backgroundColor = new Color(0.15f, 0.15f, 0.15f);
            bronzePickBuy.text = "Owned";
        }
    }

    private void IronPickBuy()
    {
        if (GameManager.Instance.ice >= ironPickCost)
        {
            GameManager.Instance.ice -= ironPickCost;
            ironPickOwned = true;
            IronPickSelect();
            pickSelect[4].style.backgroundColor = new Color(0.15f, 0.15f, 0.15f);
            ironPickBuy.text = "Owned";
        }
    }

    private void SteelPickBuy()
    {
        if (GameManager.Instance.ice >= steelPickCost)
        {
            GameManager.Instance.ice -= steelPickCost;
            steelPickOwned = true;
            SteelPickSelect();
            pickSelect[5].style.backgroundColor = new Color(0.15f, 0.15f, 0.15f);
            steelPickBuy.text = "Owned";
        }
    }

    private void TitaniumPickBuy()
    {
        if (GameManager.Instance.ice >= titaniumPickCost)
        {
            GameManager.Instance.ice -= titaniumPickCost;
            titaniumPickOwned = true;
            TitaniumPickSelect();
            pickSelect[6].style.backgroundColor = new Color(0.15f, 0.15f, 0.15f);
            titaniumPickBuy.text = "Owned";
        }
    }
    private void StonePickSelect()
    {
        if (stonePickOwned == true)
        {
            GameManager.Instance.currentPickaxe = 1;
            GameManager.Instance.basePickPower = GameManager.Instance.pickaxePowers[1];
            pickBorder(1);
        }
    }

    private void CopperPickSelect()
    {
        if (copperPickOwned == true)
        {
            GameManager.Instance.currentPickaxe = 2;
            GameManager.Instance.basePickPower = GameManager.Instance.pickaxePowers[2];
            pickBorder(2);
        }
    }

    private void BronzePickSelect()
    {
        if (bronzePickOwned == true)
        {
            GameManager.Instance.currentPickaxe = 3;
            GameManager.Instance.basePickPower = GameManager.Instance.pickaxePowers[3];
            pickBorder(3);
        }
    }

    private void IronPickSelect()
    {
        if (ironPickOwned == true)
        {
            GameManager.Instance.currentPickaxe = 4;
            GameManager.Instance.basePickPower = GameManager.Instance.pickaxePowers[4];
            pickBorder(4);
        }
    }

    private void SteelPickSelect()
    {
        if (steelPickOwned == true)
        {
            GameManager.Instance.currentPickaxe = 5;
            GameManager.Instance.basePickPower = GameManager.Instance.pickaxePowers[5];
            pickBorder(5);
        }
    }

    private void TitaniumPickSelect()
    {
        if (titaniumPickOwned == true)
        {
            GameManager.Instance.currentPickaxe = 6;
            GameManager.Instance.basePickPower = GameManager.Instance.pickaxePowers[6];
            pickBorder(6);
        }
    }

    // Update is called once per frame
    void OnGUI()
    {
        iceCount.text = GameManager.Instance.ice.ToString();
        height.text = "Height: "+ GameManager.Instance.height.ToString();
        liftButton.text = "Lift: " + liftCost + " ice";

        effMinNum.text = upgrades.efficientMiningLvl.ToString();
        strPicNum.text = upgrades.strongerPickLvl.ToString();
        morCrtNum.text = upgrades.critMineChanceLvl.ToString();
        strCrtNum.text = upgrades.critMineDamageLvl.ToString();

        autMinNum.text = upgrades.autoMinerLvl.ToString();

        effMinCost.text = upgrades.efficientMiningCost.ToString();
        strPicCost.text = upgrades.strongerPickCost.ToString();
        morCrtCost.text = upgrades.critMineChanceCost.ToString();
        strCrtCost.text = upgrades.critMineDamageCost.ToString();

        autMinCost.text = upgrades.autoMinerCost.ToString();

        pickaxeImage.style.backgroundImage = new StyleBackground(pickaxeSprites[GameManager.Instance.currentPickaxe]);           
    }
}
