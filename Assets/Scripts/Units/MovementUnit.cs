using System;
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
        private string attackAnimationName;
        private bool isAllowMoving;
        protected event Action OnGetDamaged; 

        #endregion
        
        public void Init(float attackRange, float health, float damageValue, string attackName, bool isPlayer)
        {
            rangeAttack = attackRange;
            CurrentHealth = health;
            MaxHealth = health;
            Damage = damageValue;
            isAllowMoving = false;
            movementDirection = isPlayer 
                ? new Vector2(1, 0) 
                : new Vector2(-1, 0);
            attackLayer = isPlayer 
                ? LayerMask.GetMask("Enemies") 
                : LayerMask.GetMask("Player");
            SpriteRenderer.flipX = !isPlayer;
            hurtEffectPlayer = new HurtEffectPlayer(this);
            attackAnimationName = attackName;
            
            OnGetDamaged += PlayHurtEffect;
            OnGetDamaged += () =>
            {
                if (!CheckAlive())
                    Die();
            };
        }
        
        // TODO: Add timeBtwAttack
        private void FixedUpdate()
        {
            var enemiesColliders = GetEnemiesCollider2D();
            if (enemiesColliders.Any())
            {
                foreach (var enemyCollider2D in enemiesColliders)
                {
                    PlayAnimationByNameOnce(attackAnimationName);
                    HurtEnemy(Damage, enemyCollider2D.GetComponent<MovementUnit>());
                }
            }
            else
            {
                Move();
            }
        }
            
        protected override IList<Collider2D> GetEnemiesCollider2D()
        {
            return Physics2D.RaycastAll(transform.position, movementDirection, rangeAttack, attackLayer)
                .Select(e => e.collider).ToList();
        }

        private void Move()
        {
            if (!isAllowMoving) 
                return;
            PlayAnimationByName(UnitAnimationNames.Moving);
            Rigidbody2D.MovePosition(Rigidbody2D.position + movementDirection * (4 * Time.deltaTime));
        }
        private static void HurtEnemy(float damage, MovementUnit enemyComponent)
        {
            enemyComponent.CurrentHealth -= damage;
            enemyComponent.OnGetDamaged?.Invoke();
        }

        public void AllowMoving()
            => isAllowMoving = true;
        
        private void PlayHurtEffect()
            => hurtEffectPlayer.Play();
        
        private bool CheckAlive()
            => CurrentHealth > 0;
        
        private void Die()
        {
            if (TryGetComponent(out BoxCollider2D boxCollider2D))
                Destroy(boxCollider2D);
            PlayAnimationByNameOnce(UnitAnimationNames.Death);
            SpriteRenderer.sortingLayerID = SortingLayer.NameToID("Dead");
        }
    }
}