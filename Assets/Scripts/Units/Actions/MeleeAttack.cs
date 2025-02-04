using UnityEngine;

public class MeleeAttack : AttackAction
{
    public MeleeAttack(Animator unitAnimator, string animationTypeName) : base(unitAnimator, animationTypeName) {}
    
    protected override void DamageMethod() 
    {
        foreach (var enemy in enemies)
            enemy.collider.GetComponent<IDamageble>().GetDamage();
    }
}