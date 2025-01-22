using UnityEngine;

public class MovementComponent : MonoBehaviour, IComponent
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private StateMachineUnits stateMachine;
    void FixedUpdate()
    {
        if (stateMachine.currentState == UnitState.Moving) 
            Move();
    }

    public void InitComponent()
    {
        stateMachine = GetComponent<StateMachineUnits>();
        rb = GetComponent<Rigidbody2D>();
        direction = gameObject.CompareTag("EnemyUnits") ? new(-1, 0) : new(1, 0);
        if (!gameObject.CompareTag("EnemyUnits"))
            UnitsSystem.pushUnits += Push;
    }
    public void Push() 
    {
        stateMachine.RevertJustSpawned();
        stateMachine.SetMoving();
    }
    protected void Move() 
        => rb.MovePosition(rb.position + direction * 4 * Time.deltaTime);
    public void DisposeComponent()
    {
        UnitsSystem.pushUnits -= Push;
    }
}
