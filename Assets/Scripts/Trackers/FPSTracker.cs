using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace VRSimonTask
{
    public class FPSTracker : Tracker
    {
        protected override void SetupDescriptorAndHeader()
        {
            measurementDescriptor = "fps";
            
            customHeader = new string[]
            {
                "fps"
            };
        }

        protected override UXFDataRow GetCurrentValues()
        {
            float fps = Time.unscaledDeltaTime;
            
            var values = new UXFDataRow()
            {
                ("fps", fps)
            };

            return values;
        }
    }
}

