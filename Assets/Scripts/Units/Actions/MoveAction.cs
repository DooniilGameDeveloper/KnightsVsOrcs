using UnityEngine;

public class MoveAction : IAction
{
    private Vector2 dir;
    private Rigidbody2D rb;
    private Animator animator;
    
    public MoveAction(Animator unitAnimator, Rigidbody2D rigidbody, Vector2 direction)
    {
        dir = direction;
        rb = rigidbody;
        animator = unitAnimator;
    }
    public void DoAction()
    {
        animator.SetBool("isMove", true);
        rb.MovePosition(rb.position + dir * 4 * Time.deltaTime);
    }
}