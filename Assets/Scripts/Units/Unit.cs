using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public abstract class Unit: MonoBehaviour
    {
        #region Property and get / set methods
        
        private Animator animator;
        protected SpriteRenderer SpriteRenderer;
        protected Rigidbody2D Rigidbody2D;
        
        protected float Damage;
        protected float CurrentHealth;
        protected float MaxHealth;
        
        public SpriteRenderer GetSpriteRenderer()
            => SpriteRenderer;
        
        #endregion

        private void Awake()
        {
            animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        protected abstract IList<Collider2D> GetEnemiesCollider2D();
        protected void PlayAnimationByNameOnce(string animationName)
            => animator.SetTrigger(animationName);
        protected void PlayAnimationByName(string animationName)
            => animator.SetBool(animationName, !animator.GetBool(animationName));
    }
}