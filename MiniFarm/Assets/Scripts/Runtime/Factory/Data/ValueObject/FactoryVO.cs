using System;
using Runtime.Identifiers;
using UnityEngine;

namespace Runtime.Factory.Data
{
    [Serializable]
    public class FactoryVO
    {
        public int FactoryID;
        public ResourcesType GainedHarvestType;
        public int GainedHarvestAmount;
        public int HarvestingTime;
        public int HarvestCapacity;
        public ResourcesType ConsumedResourcesType;
        public int ConsumedResourcesAmount;
        
        [Header("Animator")]
        public Animator TaskAnimator;
    }
}