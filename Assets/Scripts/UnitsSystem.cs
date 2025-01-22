using System;
using UnityEngine;

public class UnitsSystem : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject meleeUnit;
    static public event Action pushUnits;

    public void Push()
    {
        pushUnits?.Invoke();
    }

    public void AddMeleeUnit()
    {
        AddUnit(meleeUnit);
    }

    private void AddUnit(GameObject unit) 
    {
        Instantiate(unit, spawnPoint);
    }
}
