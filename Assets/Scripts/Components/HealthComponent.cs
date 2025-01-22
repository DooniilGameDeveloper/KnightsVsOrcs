using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent: MonoBehaviour, IComponent
{
    private float currentValue { get; set; }
    [SerializeField] private UnitData unit;
    private Material originalMaterial;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private HurtEffect hurtEffect;
    private Coroutine damageCoroutine;
    [SerializeField] private Image healthbar; 
    // Events
    public event Action OnDead;
    public event Action OnHurt;

    void Update()
    {
        if (healthbar != null)
            healthbar.fillAmount = currentValue / unit.health;
    }
    public void InitComponent()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        currentValue = unit.health;
        OnHurt += ShowHurtEffect;
    }

    public void Hurt(float damage)
    {
        var healthDamaged = currentValue - damage;
        if (currentValue > healthDamaged)
        {
            currentValue = healthDamaged;
            OnHurt?.Invoke();
        }   

        if (IsDead())
        {
            DisposeComponent();
            OnDead?.Invoke();
        }
    }

    public bool IsDead() => currentValue <= 0;

    private void ShowHurtEffect()
    {
        if (damageCoroutine != null)
            StopCoroutine(damageCoroutine);
        damageCoroutine = StartCoroutine(ChangeMaterial());

        IEnumerator ChangeMaterial()
        {
            spriteRenderer.material = hurtEffect.damageMaterial;
            spriteRenderer.material.SetFloat("_FlashIntensity", hurtEffect.flashIntensity); ;
            yield return new WaitForSeconds(hurtEffect.flashDuration);
            spriteRenderer.material.SetFloat("_FlashIntensity", 0f);
            damageCoroutine = null;
        }
    }
    public void DisposeComponent() => OnHurt -= ShowHurtEffect;
}