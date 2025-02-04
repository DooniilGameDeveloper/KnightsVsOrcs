using System;
using Units.Factories;
using UnityEngine;

public class UnitsSystem : MonoBehaviour
{
    public Transform spawnPoint;
    private UnitFactory unitUnitFactory;
    static public event Action pushUnits;

    void Awake()
    {
        unitUnitFactory = new OrcUnitFactory();
    }

    public void Push()
    {
        pushUnits?.Invoke();
    }

    public void AddMeleeUnit()
    {
        unitUnitFactory.CreateLightMeleeUnit();
    }
}
