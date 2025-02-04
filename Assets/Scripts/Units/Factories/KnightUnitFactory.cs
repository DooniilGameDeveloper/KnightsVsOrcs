using UnityEngine;

namespace Units.Factories
{
    public class KnightUnitFactory : UnitFactory
    {
        public override LightMeleeUnit CreateLightMeleeUnit()
        {
            var prefab = Resources.Load("Prefabs/MeleeKnight");
            var go = GameObject.Instantiate(prefab) as GameObject;
            var unitComponent = go.AddComponent<LightMeleeUnit>();
            unitComponent.Init(2.5f, 100f, 25f, true);
            return unitComponent;
        }

        public override HeavyMeleeUnit CreateHeavyMeleeUnit()
        {
            throw new System.NotImplementedException();
        }

        public override RangeUnit CreateRangeUnit()
        {
            throw new System.NotImplementedException();
        }
    }
}