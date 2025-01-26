using UnityEngine;

public abstract class Unit: MonoBehaviour, IDamageble
{
    private IAction action;
    private AttackAction attackAction;
    private MoveAction moveAction;
    private DeadAction deadAction;
    private DamageHelper damageHelper;
    [SerializeField] private HurtEffect hurtEffect;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private LayerMask attackLayer;
    private Vector2 direction;
    [SerializeField] private float range = 2f;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth = 100f;
    [SerializeField] private float damage = 25f;
    void Awake()
    {
        attackLayer = CompareTag("EnemyUnits") ? LayerMask.GetMask("Player") : LayerMask.GetMask("Enemies");
        direction = CompareTag("EnemyUnits") ? new(-1, 0) : new(1, 0);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitActions();
        InitHelpers();
    }

    protected abstract void InitActions();
    protected abstract void InitHelpers();

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
            else
                action = moveAction ?? null;
        }    

        action?.DoAction();
    }

    public void DestroyUnit()
    {
        Destroy(GetComponent<BoxCollider2D>());
    }
    
    // TODO: Move own script
    // Init Action and helpers
    public void SetMeleeAttackAction()
        => attackAction = new MeleeAttack(animator);
    public void SetMoveAction()
        => moveAction = new MoveAction(animator, GetComponent<Rigidbody2D>(), direction);
    public void SetDeadAction()
        => deadAction = new DeadAction(animator, GetComponent<SpriteRenderer>());
    public void SetDamageHelper()
        => damageHelper = new DamageHelper(spriteRenderer, hurtEffect, this);

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