using UnityEngine;

namespace Units
{
    public abstract class MovementUnit : Unit, IDamageble
    {
        private float rangeAttack;
        private float currentHealth;
        private LayerMask attackLayer;
        private Vector2 movementDirection;
        
        public void Init(float attackRange, float health, float damageValue, bool isPlayer)
        {
            rangeAttack = attackRange;
            currentHealth = health;
            Damage = damageValue;
            movementDirection = isPlayer 
                ? new Vector2(1, 0) 
                : new Vector2(-1, 0);
            attackLayer = isPlayer 
                ? LayerMask.GetMask("Enemies") 
                : LayerMask.GetMask("Player");
            SpriteRenderer.flipX = !isPlayer;
        }
        
        private void FixedUpdate()
        {
            var enemies = Physics2D.RaycastAll(transform.position, movementDirection, rangeAttack, attackLayer);
            if (enemies.Length != 0)
            {
                foreach(var enemy in enemies)
                    HurtEnemy(enemy);
            }
        }

        protected override void HurtEnemy(RaycastHit2D enemy)
        {
            enemy.transform.GetComponent<IDamageble>().GetDamage();
        }

        public void GetDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}