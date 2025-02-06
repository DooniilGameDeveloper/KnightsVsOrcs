using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Units
{
    public abstract class MovementUnit : Unit
    {
        #region Property

        private float rangeAttack;
        private LayerMask attackLayer;
        private Vector2 movementDirection;
        private HurtEffectPlayer hurtEffectPlayer;

        #endregion
        
        public void Init(float attackRange, float health, float damageValue, bool isPlayer)
        {
            rangeAttack = attackRange;
            CurrentHealth = health;
            MaxHealth = health;
            Damage = damageValue;
            movementDirection = isPlayer 
                ? new Vector2(1, 0) 
                : new Vector2(-1, 0);
            attackLayer = isPlayer 
                ? LayerMask.GetMask("Enemies") 
                : LayerMask.GetMask("Player");
            SpriteRenderer.flipX = !isPlayer;
            hurtEffectPlayer = new HurtEffectPlayer(this);
        }
        
        // TODO: Add Attack method
        private void FixedUpdate()
        {
            foreach (var enemyCollider2D in GetEnemiesCollider2D())
            {
                var isHurtEnemy = TryHurtEnemy(enemyCollider2D, out var unitComponent);
                if (isHurtEnemy)
                {
                    PlayHurtEffect(unitComponent);
                    CheckAlive(unitComponent);
                }
            }
        }
        
        private bool TryHurtEnemy(Collider2D enemyCollider2D, out MovementUnit unitComponent)
        {
            if (Damage <= 0)
            {
                unitComponent = null;
                return false;
            }
            
            unitComponent = enemyCollider2D.GetComponent<MovementUnit>();
            GetDamage(unitComponent, Damage);
            return true;
        }
            
        protected override IEnumerable<Collider2D> GetEnemiesCollider2D()
        {
            return Physics2D.RaycastAll(transform.position, movementDirection, rangeAttack, attackLayer)
                .Select(e => e.collider);
        }
        private static void GetDamage(MovementUnit unitComponent, float damage)
        {
            unitComponent.CurrentHealth -= damage;
        }

        private static void PlayHurtEffect(MovementUnit unitComponent)
        { 
            unitComponent.hurtEffectPlayer.Play();
        }
        
        // TODO: Move PlayHurtEffect, GetDamage, CheckAlive and Die to interface
    }
}