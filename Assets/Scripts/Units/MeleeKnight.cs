public class MeleeKnight : Unit
{
    protected override void InitActions()
    {
        SetMeleeAttackAction();  
        SetDeadAction();
        SetMoveAction();
    }

    protected override void InitHelpers()
    {
        SetDamageHelper();
    }
}
