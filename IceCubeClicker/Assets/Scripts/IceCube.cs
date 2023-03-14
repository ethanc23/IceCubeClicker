using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Linq;

public class IceCube : MonoBehaviour
{
    private bool drillActive;

    private float shakeStartTime;

    public int maxHp;
    public int hp;
    private int baseIce;

    private bool shaking;

    private Controls ctrl;

    [SerializeField] private DamagePopupManager damagePopupManager;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] iceCubeSprites;
    [SerializeField] private GameObject canvas;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Drill drill;

    private float autoMineTickrate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        baseIce = 1;
        maxHp = 5;
        healthBar.setMaxHp(maxHp);
        hp = maxHp;
        healthBar.setHp(hp);
        shaking = false;
        shakeStartTime = 0f;
    }

    private void AutoMine()
    {
        hp -= (int)(autoMineTickrate * GameManager.Instance.autoMineDamage);
        healthBar.setHp(hp);
        CheckCubeHp();
    }

    void Awake()
    {
        ctrl = new Controls();
        spriteRenderer.sprite = iceCubeSprites[0];
        InvokeRepeating(nameof(AutoMine), 1.0f, autoMineTickrate);
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

    public void SetCube()
    {
        maxHp = 5 * (int)Mathf.Exp(GameManager.Instance.height);
        hp = maxHp;
        baseIce = 3 * (int)Mathf.Exp(GameManager.Instance.height) - 2;
        healthBar.setMaxHp(maxHp);
        healthBar.setHp(hp);
    }

    public void Click(InputAction.CallbackContext context)
    {
        Vector2 mousePos = ctrl.Default.MousePos.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null)
        {
            int damage;
            Color color;
            if (GameManager.Instance.critChance > Random.Range(0f, 100f))
            {
                damage = (int)((GameManager.Instance.basePickPower + GameManager.Instance.pickPower) * GameManager.Instance.critDamage);
                color = new Color(180f/255f, 80f/255f, 10f/255f);
            }
            else
            {
                damage = GameManager.Instance.basePickPower + GameManager.Instance.pickPower;
                color = new Color(160f / 255f, 140f / 255f, 10f / 255f);
            }
            hp -= damage;
            healthBar.setHp(hp);
            CheckCubeHp();
            damagePopupManager.NewPopup(damage.ToString(), new Vector2((mousePos.x - 0.5f * Screen.width) * 0.007f, (mousePos.y - 0.5f * Screen.height) * 0.006f + 0.2f), color);
            StartCoroutine(Shake());
        }
    }

    public void CheckCubeHp()
    {
        float hpThreshhold = (float)maxHp / 3f;
        if (hp <= 0)
        {
            GameManager.Instance.ice += GameManager.Instance.iceMultiplier * baseIce + GameManager.Instance.bonusIce;
            if (GameManager.Instance.drillPartDropChance > Random.Range(0f, 1f))
            {
                drill.AddPartToInventory(drill.GeneratePart());
            }
            hp = maxHp;
            healthBar.setHp(hp);
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

    void Update()
    {
        if (shaking)
        {
            gameObject.transform.position = new Vector2(Mathf.Sin((Time.time - shakeStartTime) * 30f) * 0.2f, gameObject.transform.position.y);
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
