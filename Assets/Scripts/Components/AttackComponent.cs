using System;
using UnityEngine;

public class AttackComponent : MonoBehaviour, IComponent
{
    [SerializeField] private MeleeUnitData meleeUnitData;
    [SerializeField] [Range(0.5f, 5f)] private float range = 2f;
    private float timeBtwAttack = 0f;
    private LayerMask attackLayer;
    private Action<Collider2D, float> attackSpecification;
    private StateMachineUnits stateMachine;
    private Vector2 raycastDirection;

    public void InitComponent()
    {
        attackLayer = gameObject.CompareTag("EnemyUnits") ? LayerMask.GetMask("Player") : LayerMask.GetMask("Enemies");
        raycastDirection = gameObject.CompareTag("EnemyUnits") ? new(-1, 0) : new(1, 0);
        attackSpecification = GetComponent<IAttack>().Attack;
        stateMachine = GetComponent<StateMachineUnits>();
    }
    void FixedUpdate()
    {
        if (timeBtwAttack <= 0f) 
        {
            var enemies = Physics2D.RaycastAll(transform.position, raycastDirection, range, attackLayer);
            if (enemies.Length == 0) 
                stateMachine.SetMoving();
            else
                stateMachine.SetAttacking();

            foreach (var enemy in enemies)
            {
                attackSpecification.Invoke(enemy.collider, meleeUnitData.damage);
                timeBtwAttack = meleeUnitData.cooldawnAttack;
            }
        }
        else 
            timeBtwAttack -= Time.deltaTime;
    }
    public void DisposeComponent() { }
}
