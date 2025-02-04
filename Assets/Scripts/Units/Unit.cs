using UnityEngine;

namespace Units
{
    public abstract class Unit: MonoBehaviour
    {
        #region Property
        protected Animator Animator;
        protected SpriteRenderer SpriteRenderer;
        protected float Damage;
        // private bool isJustSpawned;
        //
        // public float Health
        // {
        //     get => currentHealth;
        //     set
        //     {
        //         if (value < 0)
        //             throw new InvalidDataException("[Unit->Health] Value can't be negative");
        //         currentHealth -= value;
        //         if (currentHealth <= 0)
        //             DestroyUnit();
        //     }
        // }
        
        #endregion

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected abstract void HurtEnemy(RaycastHit2D enemy);

        // public void DestroyUnit()
        // {
        //     Destroy(GetComponent<BoxCollider2D>());
        //     animator.SetTrigger("Death");
        //     spriteRenderer.sortingLayerID = SortingLayer.NameToID("Dead");
        // }
    }
}