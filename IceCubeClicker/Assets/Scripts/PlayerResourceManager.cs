using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResourceManager : MonoBehaviour
{
    // GameObjects
    [SerializeField]
    private GameObject IceCount;

    // Text
    TextMeshProUGUI IceCountText;

    // Start is called before the first frame update
    void Start()
    {
        IceCountText = IceCount.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        IceCountText.text = GameManager.Instance.ice.ToString();
    }
}
