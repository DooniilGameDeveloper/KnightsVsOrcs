using UnityEngine;

public class MeleeAttack : AttackAction
{
    public MeleeAttack(Animator unitAnimator) : base(unitAnimator) {}
    
    protected override void DamageMethod() 
    {
        foreach (var enemy in enemies)
            enemy.collider.GetComponent<IDamageble>().GetDamage();
    }
}