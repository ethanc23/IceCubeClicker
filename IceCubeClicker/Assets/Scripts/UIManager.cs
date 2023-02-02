using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Upgrades upgrades;

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

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

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

        effMinButton.clicked += upgrades.efficientMining;
        strPicButton.clicked += upgrades.strongerPick;
        denCubButton.clicked += upgrades.denserCubes;
        morCrtButton.clicked += upgrades.critMineChance;
        strCrtButton.clicked += upgrades.critMineDamage;
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
        morCrtCost.text = upgrades.critMineDamageCost.ToString();
        strCrtCost.text = upgrades.critMineDamageCost.ToString();

    }
}
