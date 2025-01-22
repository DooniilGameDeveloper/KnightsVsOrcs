using System;
using UnityEngine;

public class AttackComponent : MonoBehaviour, IComponent
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private MeleeUnitData meleeUnitData;
    private float timeBtwAttack = 0f;
    private LayerMask attackLayer;
    private Action<Collider2D, float> attackSpecification;
    private StateMachineUnits stateMachine;

    public void InitComponent()
    {
        attackLayer = gameObject.CompareTag("EnemyUnits") ? LayerMask.GetMask("Player") : LayerMask.GetMask("Enemies");
        attackSpecification = GetComponent<IAttack>().Attack;
        stateMachine = GetComponent<StateMachineUnits>();
    }
    void FixedUpdate()
    {
        if (timeBtwAttack <= 0f) 
        {
            if (attackPoint == null) 
                return;

            var enemies = Physics2D.OverlapCircleAll(attackPoint.position, meleeUnitData.attackRange, attackLayer);
            if (enemies.Length == 0) 
                stateMachine.SetMoving();
            else
                stateMachine.SetAttacking();

            foreach (var enemy in enemies)
            {
                attackSpecification.Invoke(enemy, meleeUnitData.damage);
                timeBtwAttack = meleeUnitData.cooldawnAttack;
            }
        }
        else 
            timeBtwAttack -= Time.deltaTime;
    }
    public void DisposeComponent() { }
}
