using UnityEngine;

public class KnightCreator : Creator
{
    public override Melee FactoryMethod()
    {
        var prefab = Resources.Load("Prefabs/MeleeKnight");
        var go = GameObject.Instantiate(prefab) as GameObject;
        var unitComponent = go.AddComponent<Melee>();
        unitComponent.Init(2.5f, 100f, 25f, true);
        return unitComponent;
    }
}