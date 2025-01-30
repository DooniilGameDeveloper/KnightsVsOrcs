using UnityEngine;

public class OrcCreator : Creator
{
    public override Melee FactoryMethod()
    {
        var prefab = Resources.Load("Prefabs/MeleeOrc");
        var go = GameObject.Instantiate(prefab) as GameObject;
        var unitComponent = go.AddComponent<Melee>();
        unitComponent.Init(2f, 150f, 35f, false);
        return unitComponent;
    }
}