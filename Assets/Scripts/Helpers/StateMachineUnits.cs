using UnityEngine;

public class StateMachineUnits : MonoBehaviour
{
    public StateMachineUnits()
        => currentState = UnitState.Spawned;
        
    public UnitState currentState { get; private set; }
    private bool justSpawned = true;
    public void RevertJustSpawned() => justSpawned = false;
    public void SetMoving()
    {
        if (!justSpawned)
            currentState = UnitState.Moving;
    } 
    public void SetAttacking() => currentState= UnitState.Attacking;
}

public enum UnitState 
{
    Spawned,
    Moving,
    Attacking
}
