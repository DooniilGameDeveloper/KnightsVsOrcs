using UnityEngine;

public abstract class Unit: MonoBehaviour, IDamageble
{
    #region Property
    private IAction action;
    protected AttackAction attackAction;
    protected MoveAction moveAction;
    protected DeadAction deadAction;
    protected GetDamageHelper damageHelper;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    private LayerMask attackLayer;
    public Vector2 direction;
    private bool isJustSpawned;
    private float range = 2f;
    private float maxHealth = 100f;
    private float currentHealth = 100f;
    private float damage = 25f;
    #endregion
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Init(float actionRange, float health, float damageValue, bool isPlayer)
    {
        range = actionRange;
        maxHealth = health;
        damage = damageValue;
        direction = isPlayer ? new(1, 0) : new(-1, 0);
        attackLayer = isPlayer ? LayerMask.GetMask("Enemies") : LayerMask.GetMask("Player");
        isJustSpawned = true;
        spriteRenderer.flipX = !isPlayer;
    }


    void FixedUpdate()
    {
        if (currentHealth <= 0 && deadAction != null)
            action = deadAction;
        else 
        {
            var enemies = Physics2D.RaycastAll(transform.position, direction, range, attackLayer);
            if (enemies.Length != 0)
            {
                attackAction.SetEnemies(enemies);
                action = attackAction ?? null;
            }
            else if (!isJustSpawned)
                action = moveAction ?? null;
        }    

        action?.DoAction();
    }

    public virtual void DestroyUnit()
    {
        Destroy(GetComponent<BoxCollider2D>());
    }


    // getters / setters
    public float GetHealth()
        => currentHealth;
    public void SetHealth(float value)
    {
        if (value < 0)
        {
            Debug.LogError("Health cannot be negative");
            return;
        }
        currentHealth = value;
    }
    public float GetDamageValue()
        => damage;
    public void GetDamage()
        => damageHelper.GetDamage();
}