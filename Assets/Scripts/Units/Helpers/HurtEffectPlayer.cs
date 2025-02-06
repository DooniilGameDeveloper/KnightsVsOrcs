using System.Collections;
using Units;
using UnityEngine;

public class HurtEffectPlayer
{
    private Coroutine damageCoroutine;
    private readonly SpriteRenderer sp;
    private readonly HurtEffect hE;
    private readonly Unit m;
    public HurtEffectPlayer(Unit unit)
    {
        hE = Resources.Load<HurtEffect>("HurtEffect");
        sp = unit.GetSpriteRenderer();
        m = unit;
    }

    public void Play()
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
}