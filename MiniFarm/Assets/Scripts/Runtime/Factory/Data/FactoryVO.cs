using System;
using Runtime.Identifiers;

namespace Runtime.Factory.Data
{
    [Serializable]
    public class FactoryVO
    {
        public ResourcesType GainedHarvestType;
        public int GainedHarvestAmount;
        public int HarvestingTime;
        public int HarvestCapacity;
        public ResourcesType ConsumedResourcesType;
        public int ConsumedResourcesAmount;
    }
}