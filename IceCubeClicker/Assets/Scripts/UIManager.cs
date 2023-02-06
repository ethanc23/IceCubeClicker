using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Upgrades upgrades;

    [SerializeField] private Sprite[] pickaxeSprites;

    private VisualElement pickaxeWindow;

    private Label iceCount;

    private Label effMinNum;
    private Label strPicNum;
    private Label denCubNum;
    private Label morCrtNum;
    private Label strCrtNum;

    private Label effMinCost;
    private Label strPicCost;
    private Label denCubCost;
    private Label morCrtCost;
    private Label strCrtCost;

    public Button effMinButton;
    public Button strPicButton;
    public Button denCubButton;
    public Button morCrtButton;
    public Button strCrtButton;

    private Button pickaxeButton;
    private VisualElement pickaxeImage;

    private Button stonePickBuy;
    private Button copperPickBuy;
    private Button bronzePickBuy;
    private Button ironPickBuy;
    private Button steelPickBuy;
    private Button titaniumPickBuy;

    private Button stonePickSelect;
    private Button copperPickSelect;
    private Button bronzePickSelect;
    private Button ironPickSelect;
    private Button steelPickSelect;
    private Button titaniumPickSelect;

    private bool pickaxeWindowActive;

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

        effMinNum = root.Q<Label>("effMinNum");
        strPicNum = root.Q<Label>("strPicNum");
        denCubNum = root.Q<Label>("denCubNum");
        morCrtNum = root.Q<Label>("morCrtNum");
        strCrtNum = root.Q<Label>("strCrtNum");

        effMinCost = root.Q<Label>("effMinCost");
        strPicCost = root.Q<Label>("strPicCost");
        denCubCost = root.Q<Label>("denCubCost");
        morCrtCost = root.Q<Label>("morCrtCost");
        strCrtCost = root.Q<Label>("strCrtCost");

        effMinButton = root.Q<Button>("effMinButton");
        strPicButton = root.Q<Button>("strPicButton");
        denCubButton = root.Q<Button>("denCubButton");
        morCrtButton = root.Q<Button>("morCrtButton");
        strCrtButton = root.Q<Button>("strCrtButton");

        pickaxeButton = root.Q<Button>("pickaxeButton");
        pickaxeImage = pickaxeButton.ElementAt(0);

        stonePickBuy = root.Q<Button>("stonePickBuy");
        copperPickBuy = root.Q<Button>("copperPickBuy");
        bronzePickBuy = root.Q<Button>("bronzePickBuy");
        ironPickBuy = root.Q<Button>("ironPickBuy");
        steelPickBuy = root.Q<Button>("steelPickBuy");
        titaniumPickBuy = root.Q<Button>("titaniumPickBuy");

        stonePickSelect = root.Q<Button>("stonePickSelect");
        copperPickSelect = root.Q<Button>("copperPickSelect");
        bronzePickSelect = root.Q<Button>("bronzePickSelect");
        ironPickSelect = root.Q<Button>("ironPickSelect");
        steelPickSelect = root.Q<Button>("steelPickSelect");
        titaniumPickSelect = root.Q<Button>("titaniumPickSelect");

        effMinButton.clicked += upgrades.efficientMining;
        strPicButton.clicked += upgrades.strongerPick;
        denCubButton.clicked += upgrades.denserCubes;
        morCrtButton.clicked += upgrades.critMineChance;
        strCrtButton.clicked += upgrades.critMineDamage;

        pickaxeButton.clicked += PickaxeWindow;

        stonePickBuy.clicked += StonePickBuy;
        copperPickBuy.clicked += CopperPickBuy;
        bronzePickBuy.clicked += BronzePickBuy;
        ironPickBuy.clicked += IronPickBuy;
        steelPickBuy.clicked += SteelPickBuy;
        titaniumPickBuy.clicked += TitaniumPickBuy;

        stonePickSelect.clicked += StonePickSelect;
        copperPickSelect.clicked += CopperPickSelect;
        bronzePickSelect.clicked += BronzePickSelect;
        ironPickSelect.clicked += IronPickSelect;
        steelPickSelect.clicked += SteelPickSelect;
        titaniumPickSelect.clicked += TitaniumPickSelect;

        pickaxeWindow.visible = false;
        pickaxeWindowActive = false;

        stonePickCost = 0;
        copperPickCost = 0;
        bronzePickCost = 0;
        ironPickCost = 0;
        steelPickCost = 0;
        titaniumPickCost = 0;

        stonePickOwned = false;
        copperPickOwned = false;
        bronzePickOwned = false;
        ironPickOwned = false;
        steelPickOwned = false;
        titaniumPickOwned = false;

    }
    private void PickaxeWindow()
    {
        if (pickaxeWindowActive == false)
        {
            pickaxeWindow.visible = true;
            pickaxeWindowActive = true;
        } 
        else if (pickaxeWindowActive == true)
        {
            pickaxeWindow.visible = false;
            pickaxeWindowActive = false;
        }
    }

    private void StonePickBuy()
    {
        if (GameManager.Instance.ice > stonePickCost)
        {
            stonePickOwned = true;
            GameManager.Instance.currentPickaxe = 1;
        }
    }

    private void CopperPickBuy()
    {
        if (GameManager.Instance.ice > copperPickCost)
        {
            copperPickOwned = true;
            GameManager.Instance.currentPickaxe = 2;
        }
    }

    private void BronzePickBuy()
    {
        if (GameManager.Instance.ice > bronzePickCost)
        {
            bronzePickOwned = true;
            GameManager.Instance.currentPickaxe = 3;
        }
    }

    private void IronPickBuy()
    {
        if (GameManager.Instance.ice > ironPickCost)
        {
            ironPickOwned = true;
            GameManager.Instance.currentPickaxe = 4;
        }
    }

    private void SteelPickBuy()
    {
        if (GameManager.Instance.ice > steelPickCost)
        {
            steelPickOwned = true;
            GameManager.Instance.currentPickaxe = 5;
        }
    }

    private void TitaniumPickBuy()
    {
        if (GameManager.Instance.ice > titaniumPickCost)
        {
            titaniumPickOwned = true;
            GameManager.Instance.currentPickaxe = 6;
        }
    }
    private void StonePickSelect()
    {
        if (stonePickOwned == true)
        {
            GameManager.Instance.currentPickaxe = 1;
        }
    }

    private void CopperPickSelect()
    {
        if (copperPickOwned == true)
        {
            GameManager.Instance.currentPickaxe = 2;
        }
    }

    private void BronzePickSelect()
    {
        if (bronzePickOwned == true)
        {
            GameManager.Instance.currentPickaxe = 3;
        }
    }

    private void IronPickSelect()
    {
        if (ironPickOwned == true)
        {
            GameManager.Instance.currentPickaxe = 4;
        }
    }

    private void SteelPickSelect()
    {
        if (steelPickOwned == true)
        {
            GameManager.Instance.currentPickaxe = 5;
        }
    }

    private void TitaniumPickSelect()
    {
        if (titaniumPickOwned == true)
        {
            GameManager.Instance.currentPickaxe = 6;
        }
    }

    // Update is called once per frame
    void OnGUI()
    {
        iceCount.text = GameManager.Instance.ice.ToString();

        effMinNum.text = upgrades.efficientMiningLvl.ToString();
        strPicNum.text = upgrades.strongerPickLvl.ToString();
        denCubNum.text = upgrades.denserCubesLvl.ToString();
        morCrtNum.text = upgrades.critMineChanceLvl.ToString();
        strCrtNum.text = upgrades.critMineDamageLvl.ToString();

        effMinCost.text = upgrades.efficientMiningCost.ToString();
        strPicCost.text = upgrades.strongerPickCost.ToString();
        denCubCost.text = upgrades.denserCubesCost.ToString();
        morCrtCost.text = upgrades.critMineChanceCost.ToString();
        strCrtCost.text = upgrades.critMineDamageCost.ToString();

        pickaxeImage.style.backgroundImage = new StyleBackground(pickaxeSprites[GameManager.Instance.currentPickaxe]);

        stonePickBuy.text = stonePickCost.ToString();
        copperPickBuy.text = copperPickCost.ToString();
        bronzePickBuy.text = bronzePickCost.ToString();
        ironPickBuy.text = ironPickCost.ToString();
        steelPickBuy.text = steelPickCost.ToString();
        titaniumPickBuy.text = titaniumPickCost.ToString();
           
    }
}
