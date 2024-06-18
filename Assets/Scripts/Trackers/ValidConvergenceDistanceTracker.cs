using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace VRSimonTask
{
    public class ValidConvergenceDistanceTracker : Tracker
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
                "validity"
            };
        }

        protected override UXFDataRow GetCurrentValues()
        {
            int isValid = eyeTracking.data_world.GazeRay.IsValid ?  1 : 0;
            
            var values = new UXFDataRow()
            {
                ("validity", isValid.ToString())
            };

            return values;
        }
    }
}

