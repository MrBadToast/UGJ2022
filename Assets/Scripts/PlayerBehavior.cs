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

    public delegate bool ONPlayerGainedPet(PetFollowing pet);
    public ONPlayerGainedPet OnPlayerGainedPet;

    public delegate void ONPlayerDied();
    public ONPlayerDied OnPlayerDied;

    public delegate void ONPlayerGetFellow();
    public ONPlayerGetFellow OnPlayerGetFellow;

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
    [SerializeField,Range(0f,1f)] private float acceleration;
    [SerializeField,Range(0f,1f)] private float drag;
    [Title("RhythmMechanicProperties")]
    [SerializeField] private float interval = 0.5f;
    [SerializeField] private float tollerance = 0.1f;

    [Title("References")]
    [SerializeField] private HealthBar healthBar;

    private bool isControlActive = true;
    private bool isRhythmTimerOn = false;
    private float currentHealth;
    private float rhythmTimer = 0f;
    private Vector2 forwardingDirection = Vector2.right;
    public bool IsControlActive { get { return isControlActive; } }

    private Rigidbody2D rbody;

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
    }

    private void Start()
    {   
        currentHealth = fullHealth;
    }

    private void FixedUpdate()
    {
        {
            //if (Input.GetKey(key_right))
            //{
            //    transform.right = Vector2.right;
            //    rbody.velocity = Vector2.Lerp(rbody.velocity,new Vector2( moveSpeed,rbody.velocity.y),acceleration);
            //}
            //else if (Input.GetKey(key_left))
            //{
            //    transform.right = Vector2.left;
            //    rbody.velocity = Vector2.Lerp(rbody.velocity, new Vector2(-moveSpeed, rbody.velocity.y), acceleration);
            //}

            //if (Input.GetKey(key_up))
            //{
            //    rbody.velocity = Vector2.Lerp(rbody.velocity, new Vector2(rbody.velocity.x, moveSpeed), acceleration);
            //}
            //else if (Input.GetKey(key_down))
            //{
            //    rbody.velocity = Vector2.Lerp(rbody.velocity, new Vector2(rbody.velocity.x, -moveSpeed), acceleration);
            //}

            //rbody.velocity = Vector2.Lerp(rbody.velocity, Vector2.zero, drag);
        }       
    }

    private void Update()
    {
        if (isRhythmTimerOn)
            rhythmTimer += Time.deltaTime;

        if (Input.GetKeyDown(key_tryDirection))
        {

        }
    }

    public void StartRhythmTimer()
    { isRhythmTimerOn =true; }

    public void DamagePlayer(float damageAmount) 
    {
        if(damageAmount >= currentHealth)
        {
            PlayerDied();
            currentHealth = 0f;
        }
        else
        {
            currentHealth -= damageAmount;
        }

        healthBar.SetHealthbar(currentHealth / fullHealth);
    }

    public void HealPlayer(float healAmount)
    {
        if(healAmount + currentHealth >= fullHealth)
        {
            currentHealth = fullHealth;
        }
        else
        {
            currentHealth += healAmount;
        }

        healthBar.SetHealthbar(currentHealth/fullHealth);
    }

    private void PlayerDied()
    {
        OnPlayerDied.Invoke();
        isControlActive = false;
    }



}

