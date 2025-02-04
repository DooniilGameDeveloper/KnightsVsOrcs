using UnityEngine;

namespace Units.Factories
{
    public class OrcUnitFactory : UnitFactory
    {
        public override LightMeleeUnit CreateLightMeleeUnit()
        {
            var prefab = Resources.Load("Prefabs/MeleeOrc");
            var go = GameObject.Instantiate(prefab) as GameObject;
            var unitComponent = go.AddComponent<LightMeleeUnit>();
            unitComponent.Init(2f, 150f, 35f, false);
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