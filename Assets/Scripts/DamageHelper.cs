using System.Collections;
using UnityEngine;

public class DamageHelper
{
    private Coroutine damageCoroutine;
    private SpriteRenderer sp;
    private HurtEffect hE;
    private Unit m;
    public DamageHelper(SpriteRenderer spriteRenderer, HurtEffect hurtEffect, Unit mono)
    {
        sp = spriteRenderer;
        hE = hurtEffect;
        m = mono;
    }

    private void ShowHurtEffect()
    {
        if (damageCoroutine != null)
            m.StopCoroutine(damageCoroutine);
        damageCoroutine = m.StartCoroutine(ChangeMaterial());

        IEnumerator ChangeMaterial()
        {
            sp.material = hE.damageMaterial;
            sp.material.SetFloat("_FlashIntensity", hE.flashIntensity); ;
            yield return new WaitForSeconds(hE.flashDuration);
            sp.material.SetFloat("_FlashIntensity", 0f);
            damageCoroutine = null;
        }
    }

    public void GetDamage() 
    {
        var healthDamaged = m.GetHealth() - m.GetDamageValue();
        if (m.GetHealth() > healthDamaged)
        {
            m.SetHealth(healthDamaged);
            ShowHurtEffect();
        }   
    }
}