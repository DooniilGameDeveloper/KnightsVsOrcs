
using UnityEngine;

public abstract class AttackAction : IAction
{
    private float timeBtwAttack = 0f;
    private float cooldawnAttack = 1f;
    private Animator animator;
    private string animationName;

    public AttackAction(Animator unitAnimator, string animationTypeName)
    {
        animator = unitAnimator;
        animationName = animationTypeName;
    }

    public void DoAction()
    {
        if (timeBtwAttack <= 0f) 
        {
            animator.SetTrigger(animationName);
            DamageMethod();
            timeBtwAttack = cooldawnAttack;
        }
        else 
            timeBtwAttack -= Time.deltaTime;
    }

    // Принимает название анимации и секунд задержки
    protected abstract void DamageMethod();
    // Запустить анимацию 
    // Подождать N секунд перед ударом
    // Перед ударом проверить, жив юнит или нет
    // Если не жив -> завершить коррутину
}