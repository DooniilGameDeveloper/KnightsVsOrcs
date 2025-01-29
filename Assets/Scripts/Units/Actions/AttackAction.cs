using UnityEngine;

public abstract class AttackAction : IAction
{
    protected RaycastHit2D[] enemies;
    private float timeBtwAttack = 0f;
    private float cooldawnAttack = 1f;
    private Animator animator;

    public AttackAction(Animator unitAnimator)
    {
        animator = unitAnimator;
    }

    public void SetEnemies(RaycastHit2D[] units) 
        => enemies = units;

    public void DoAction()
    {
        if (timeBtwAttack <= 0f) 
        {
            animator.SetTrigger("Attack");
            DamageMethod();
            timeBtwAttack = cooldawnAttack;
        }
        else 
            timeBtwAttack -= Time.deltaTime;
    }

    protected abstract void DamageMethod();
}