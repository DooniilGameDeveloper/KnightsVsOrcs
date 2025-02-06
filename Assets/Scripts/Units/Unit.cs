using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public abstract class Unit: MonoBehaviour
    {
        #region Property and get / set methods
        
        private Animator animator;
        protected SpriteRenderer SpriteRenderer;
        protected float Damage;
        protected float CurrentHealth;
        protected float MaxHealth;
        // private bool isJustSpawned;
        
        public SpriteRenderer GetSpriteRenderer()
            => SpriteRenderer;
        
        #endregion

        private void Awake()
        {
            animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected abstract IEnumerable<Collider2D> GetEnemiesCollider2D();

        private void Die()
        {
            if (TryGetComponent(out BoxCollider2D boxCollider2D))
                Destroy(boxCollider2D);
            animator.SetTrigger("Death");
            SpriteRenderer.sortingLayerID = SortingLayer.NameToID("Dead");
        }
        protected static void CheckAlive(Unit unit)
        {
            if (unit.CurrentHealth <= 0)
                unit.Die();
        }
    }
}