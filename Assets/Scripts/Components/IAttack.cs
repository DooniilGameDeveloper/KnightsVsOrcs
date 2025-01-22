using UnityEngine;

public interface IAttack
{
    public void Attack(Collider2D unit, float damage);
}