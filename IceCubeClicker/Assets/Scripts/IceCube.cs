using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class IceCube : MonoBehaviour
{
    private Rect bounds = new Rect(8, 128, 128, 256);

    public int maxHp;
    public int hp;

    private bool shaking;

    private Controls ctrl;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite[] iceCubeSprites;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = 5;
        hp = maxHp;
        shaking = false;
    }

    void Awake()
    {
        ctrl = new Controls();
        spriteRenderer.sprite = iceCubeSprites[0];
    }

    private void OnEnable()
    {
        ctrl.Enable();
        ctrl.Default.LeftClick.performed += Click;
    }

    private void OnDisable()
    {
        ctrl.Disable();
        ctrl.Default.LeftClick.performed -= Click;
    }

    private void Click(InputAction.CallbackContext context)
    {
        Vector2 mousePos = ctrl.Default.MousePos.ReadValue<Vector2>();
        Debug.Log(mousePos);
        if (bounds.Contains(mousePos))
        {
            if(GameManager.Instance.critChance > Random.Range(0f, 100f))
            {
                hp -= (int)(GameManager.Instance.pickPower * GameManager.Instance.critDamage);
            }
            else
            {
                hp -= GameManager.Instance.pickPower;
            }
            
            //Debug.Log(hp);
            //StartCoroutine(Shake());
            //Debug.Log("coroutine started");
            float hpThreshhold = (float)maxHp / 3f;
            if (hp <= 0)
            {
                GameManager.Instance.ice += GameManager.Instance.iceMultiplier * GameManager.Instance.bonusIce;
                hp = maxHp;
                spriteRenderer.sprite = iceCubeSprites[0];
            }
            else if ((float)hp < hpThreshhold)
            {
                spriteRenderer.sprite = iceCubeSprites[3];
            }
            else if ((float)hp < hpThreshhold * 2f)
            {
                spriteRenderer.sprite = iceCubeSprites[2];
            }
            else if ((float)hp < hpThreshhold * 3f)
            {
                spriteRenderer.sprite = iceCubeSprites[1];
            }
        }
        
    }

    void Update()
    {
        while (shaking)
        {
            this.gameObject.transform.position = new Vector2(Mathf.Sin(Time.time * 1.0f) * 1.0f, this.gameObject.transform.position.y);
        }
    }

    IEnumerator Shake()
    {
        shaking = true;
        Debug.Log("shaking");
        yield return new WaitForSeconds(1);
        Debug.Log("shaken");
        shaking = false;
    }
}
