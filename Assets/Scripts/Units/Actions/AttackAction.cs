
using UnityEngine;

public abstract class AttackAction : IAction
{
    protected RaycastHit2D[] enemies;
    private float timeBtwAttack = 0f;
    private float cooldawnAttack = 1f;
    private Animator animator;
    private string animationName;

    public AttackAction(Animator unitAnimator, string animationTypeName)
    {
        animator = unitAnimator;
        animationName = animationTypeName;
    }

    public void SetEnemies(RaycastHit2D[] units) 
        => enemies = units;

    public void DoAction()
    {
        if (timeBtwAttack <= 0f) 
        {
            animator.SetTrigger(animationName);
            DamageMethod();
            timeBtwAttack = cooldawnAttack;
        }
        else 
            timeBtwAttack -= Time.deltaTime;
    }

    protected abstract void DamageMethod();
}