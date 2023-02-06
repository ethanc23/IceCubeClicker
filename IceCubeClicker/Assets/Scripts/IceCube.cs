using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class IceCube : MonoBehaviour
{
    private float shakeStartTime;

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
        shakeStartTime = 0f;
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

    public void Click(InputAction.CallbackContext context)
    {
        
        Ray ray = Camera.main.ScreenPointToRay(ctrl.Default.MousePos.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null)
        {
            if (GameManager.Instance.critChance > Random.Range(0f, 100f))
            {
                hp -= (int)((GameManager.Instance.basePickPower + GameManager.Instance.pickPower) * GameManager.Instance.critDamage);
            }
            else
            {
                hp -= GameManager.Instance.basePickPower + GameManager.Instance.pickPower;
            }

            StartCoroutine(Shake());

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
        if (shaking)
        {
            this.gameObject.transform.position = new Vector2(Mathf.Sin((Time.time - shakeStartTime) * 30f) * 0.2f, this.gameObject.transform.position.y);
        }
    }

    IEnumerator Shake()
    {
        Vector2 initialPos = transform.position;

        shakeStartTime = Time.time;

        if (shaking == false)
        {
            shaking = true;
        }

        yield return new WaitForSeconds(0.25f);

        shaking = false;
        transform.position = initialPos;
    }
}
