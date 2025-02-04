using Units;
using UnityEngine;

public class DeathStateMachineBehavior : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, 1f);
    }
}
