namespace Units.Factories
{
    public abstract class UnitFactory 
    {
        public abstract LightMeleeUnit CreateLightMeleeUnit();
        public abstract HeavyMeleeUnit CreateHeavyMeleeUnit();
        public abstract RangeUnit CreateRangeUnit();
    }
}