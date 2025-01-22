using UnityEngine;

[CreateAssetMenu(fileName = "HurtEffect", menuName = "Effects/HurtEffectDataType")]
public class HurtEffect : ScriptableObject
{
    public Material damageMaterial;
    public float flashIntensity;
    public float flashDuration; 
}
