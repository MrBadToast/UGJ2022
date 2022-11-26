using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerBehavior : MonoBehaviour
{
    static private PlayerBehavior instance;
    static public PlayerBehavior Instance { get { return instance; } }

    public delegate bool ONPlayerDamaged();
    public ONPlayerDamaged OnPlayerDamaged;

    public delegate bool ONPlayerDied();
    public ONPlayerDied OnPlayerDied;

    public delegate void ONPlayerGetFellow(PetFollowing pet);
    public ONPlayerGetFellow OnPlayerGetFellow;

    private enum ControlState
    {
        NONE,
        DIRECTION,
        TIMING
    }

    [Title("Controls")]
    [SerializeField] private KeyCode key_tryDirection;
    [SerializeField] private KeyCode key_tryConfirm;
    //[SerializeField] private KeyCode key_right;
    //[SerializeField] private KeyCode key_left;
    //[SerializeField] private KeyCode key_up;
    //[SerializeField] private KeyCode key_down;

    [Title("Properties")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fullHealth;
    [SerializeField, Range(0f, 1f)] private float acceleration;
    [SerializeField, Range(0f, 1f)] private float drag;
    [SerializeField] private float DamageCooldown = 2.0f;

    [Title("References")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private RhythmBar rhythmBar;
    [SerializeField] private Transform DirectionArrowAnchor;
    [SerializeField] private Transform RhythmbarAnchor;
    [SerializeField] private GameObject DirectionArrowObject;
    [SerializeField] private GameObject DirectionArrowBounce;
    [SerializeField] private GameObject DirectionArrowShot;
    [SerializeField] private Transform RCO_Up;
    [SerializeField] private Transform RCO_Right;
    [SerializeField] private string WallObjectTag;
    [SerializeField] private LayerMask WallLayer;

    private bool isControlActive = true;
    private float currentHealth;
    private float rhythmTimer = 0f;
    private float damageT = 0f;
    private Vector2 forwardingDirection = Vector2.right;
    [SerializeField] private ControlState controlState = ControlState.NONE;
    public bool IsControlValid { get { return isControlActive && Time.timeScale != 0f; } }

    private Rigidbody2D rbody;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentHealth = fullHealth;
        forwardingDirection = Vector2.right;
    }

    private void FixedUpdate()
    {
        rbody.velocity = forwardingDirection * moveSpeed;
    }

    private void Update()
    {
        damageT -= Time.deltaTime;

        if (IsControlValid)
        {
            rbody.velocity = Vector2.Lerp(rbody.velocity, forwardingDirection.normalized, acceleration);

            if (controlState == ControlState.NONE)
            {
                if (Input.GetKeyDown(key_tryDirection))
                {
                    controlState = ControlState.DIRECTION;
                    DirectionArrowObject.SetActive(true);
                }
            }
            else if (controlState == ControlState.DIRECTION)
            {
                SetArrowDirection();
                if (Input.GetKeyDown(key_tryDirection))
                {
                    StopCoroutine("Cor_RhythmBarTermination");
                    controlState = ControlState.TIMING;
                    DirectionArrowObject.SetActive(false);
                    DirectionArrowBounce.SetActive(true);
                    rhythmBar.gameObject.SetActive(true);
                }
            }
            else if (controlState == ControlState.TIMING)
            {
                rhythmBar.BarMoving = true;
                if (Input.GetKeyDown(key_tryConfirm))
                {
                    var beat = BeatManager.Instance;
                    if (beat.IsCurrentInputValidBeat())
                    {
                        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector2 direction = mousePos - transform.position;
                        SetPlayerDirection(direction);
                    }

                    StopCoroutine("Cor_RhythmBarTermination");
                    StartCoroutine("Cor_RhythmBarTermination");
                    DirectionArrowBounce.SetActive(false);
                    DirectionArrowShot.SetActive(true);
                    rhythmBar.BarMoving = false;
                    controlState = ControlState.NONE;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == WallObjectTag)
        {
  
            if(Physics2D.Raycast(transform.position,new Vector2(forwardingDirection.x,0f),1.5f,WallLayer))
            {
                Debug.Log("COL");
                SetPlayerDirection(new Vector2(-forwardingDirection.x, forwardingDirection.y));
            }
            if(Physics2D.Raycast(transform.position, new Vector2(0f, forwardingDirection.y), 1.5f,WallLayer))
            {
               SetPlayerDirection(new Vector2(forwardingDirection.x, -forwardingDirection.y));
            }
        }
    }

    public void DamagePlayer(float damageAmount)
    {
        if (damageT > 0) return;
        damageT = DamageCooldown;

        if (damageAmount >= currentHealth)
        {
            PlayerDied();
            currentHealth = 0f;
        }
        else
        {
            currentHealth -= damageAmount;
            StopCoroutine("Cor_DamageFliker");
            StartCoroutine("Cor_DamageFliker",DamageCooldown);
            OnPlayerDamaged.Invoke();
        }

        healthBar.SetHealthbar(currentHealth / fullHealth);
    }

    public void HealPlayer(float healAmount)
    {
        if (healAmount + currentHealth >= fullHealth)
        {
            currentHealth = fullHealth;
        }
        else
        {
            currentHealth += healAmount;
        }

        healthBar.SetHealthbar(currentHealth / fullHealth);
    }

    private IEnumerator Cor_RhythmBarTermination()
    {
        yield return new WaitForSeconds(0.5f);
        rhythmBar.gameObject.SetActive(false);
    }

    private IEnumerator Cor_DamageFliker(float time)
    {
        for(float t = time; t > 0f; t -= Time.fixedDeltaTime)
        {
            if(t % 0.2f > 0.1f)
            {
                spriteRenderer.color = Color.red;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }    
            yield return new WaitForFixedUpdate();
        }

        spriteRenderer.color = Color.white;
    }

    private void PlayerDied()
    {
        OnPlayerDied.Invoke();
        isControlActive = false;
    }

    private void SetArrowDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;

        DirectionArrowAnchor.up = direction;

    }

    private void SetPlayerDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        forwardingDirection = direction.normalized;
    }
}

