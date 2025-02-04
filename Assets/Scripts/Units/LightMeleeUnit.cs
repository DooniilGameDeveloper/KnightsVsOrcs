using UnityEngine;

namespace Units
{
    public class LightMeleeUnit : Unit
    {
        public override void Init(float actionRange, float health, float damageValue, bool isPlayer)
        {
            base.Init(actionRange, health, damageValue, isPlayer);
            attackAction = new MeleeAttack(animator, AttackActionName.LightAttack);
            moveAction = new MoveAction(animator, GetComponent<Rigidbody2D>(), direction);
            deadAction = new DeadAction(animator, spriteRenderer);
            damageHelper = new GetDamageHelper(spriteRenderer, this);
        }
    }
}
