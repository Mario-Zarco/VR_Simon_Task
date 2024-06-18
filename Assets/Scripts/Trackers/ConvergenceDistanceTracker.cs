using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace VRSimonTask
{
    public class ConvergenceDistanceTracker : Tracker
    {
        private EyeTracking eyeTracking;
        private void Awake()
        {
            eyeTracking = GetComponentInParent<EyeTracking>();
        }

        protected override void SetupDescriptorAndHeader()
        {
            measurementDescriptor = "convergence";
            
            customHeader = new string[]
            {
                "distance"
            };
        }

        protected override UXFDataRow GetCurrentValues()
        {
            float convergenceDistance = eyeTracking.data_world.ConvergenceDistance;
            
            var values = new UXFDataRow()
            {
                ("distance", convergenceDistance)
            };

            return values;
        }
    }
}


