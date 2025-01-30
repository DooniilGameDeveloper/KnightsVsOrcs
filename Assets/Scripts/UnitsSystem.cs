using System;
using UnityEngine;

public class UnitsSystem : MonoBehaviour
{
    public Transform spawnPoint;
    private Creator unitCreator;
    static public event Action pushUnits;

    void Awake()
    {
        unitCreator = new OrcCreator();
    }

    public void Push()
    {
        pushUnits?.Invoke();
    }

    public void AddMeleeUnit()
    {
        unitCreator.FactoryMethod();
    }
}
