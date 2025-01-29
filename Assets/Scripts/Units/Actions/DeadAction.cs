using UnityEngine;

public class DeadAction : IAction
{
    private Animator animator;
    private SpriteRenderer sp;
        
    public DeadAction(Animator unitAnimator, SpriteRenderer spriteRenderer)
    {
        animator = unitAnimator;
        sp = spriteRenderer;
    }

    public void DoAction()
    {
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Body")) return;
        animator.SetTrigger("Death");
        sp.sortingLayerID = SortingLayer.NameToID("Dead");
    }
}