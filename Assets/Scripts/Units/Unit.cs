using UnityEngine;

public class Unit: MonoBehaviour, IDamageble, IAttack
{
    #region Properties
    protected Animator animator;
    private SpriteRenderer spriteRenderer;
    public HealthComponent healthComponent { get; private set; }
    private MovementComponent movementComponent;
    private AttackComponent attackComponent;
    #endregion

    #region InitMethods
    void Awake() => UnitAwake();
    protected virtual void UnitAwake()
    {
        if (TryGetComponent(out Animator an)) 
            animator = an;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (TryGetComponent(out HealthComponent hc)) 
        {
            healthComponent = hc;
            healthComponent.InitComponent();
            healthComponent.OnDead += GetDead;
        }
        if (TryGetComponent(out AttackComponent ac)) 
        {
            attackComponent = ac;
            attackComponent.InitComponent();
        }
        if (TryGetComponent(out MovementComponent mc)) 
        {
            movementComponent = mc;
            movementComponent.InitComponent();
        }
    }
    #endregion
    #region IDamagable
    public virtual void GetDead()
    {
        animator.SetTrigger("Death");
        spriteRenderer.sortingLayerID = SortingLayer.NameToID("Dead");
    }
    public void GetDamage(float damage) 
        => healthComponent.Hurt(damage);
    #endregion
    #region IAttack
    public void Attack(Collider2D unit, float damage) 
    {
        if (healthComponent != null && !healthComponent.IsDead())
        {
            animator.SetTrigger("Attack");
            unit.GetComponent<IDamageble>().GetDamage(damage);
        }
    }
    #endregion
    #region Common methods
    public void DestroyUnit()
    {
        movementComponent.DisposeComponent();
        healthComponent.OnDead -= GetDead;
        Destroy(GetComponent<BoxCollider2D>());
    }
    #endregion
}