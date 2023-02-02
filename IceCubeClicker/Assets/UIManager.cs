using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Upgrades upgrades;

    private Label effMinNum;
    private Label effMinCost;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        effMinNum = root.Q<Label>("num");
        effMinCost = root.Q<Label>("cost");
    }

    // Update is called once per frame
    void OnGUI()
    {
        effMinNum.text = upgrades.efficientMiningLvl.ToString();
        effMinCost.text = upgrades.efficientMiningCost.ToString();
    }
}
