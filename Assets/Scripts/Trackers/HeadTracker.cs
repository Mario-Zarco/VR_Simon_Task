using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace VRSimonTask
{
    public class HeadTracker : Tracker
    {
        protected override void SetupDescriptorAndHeader()
        {
            measurementDescriptor = "movement";
            
            customHeader = new string[]
            {
                "pos_x",
                "pos_y",
                "pos_z",
            };
        }

        protected override UXFDataRow GetCurrentValues()
        {
            Vector3 p = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.Head);
            
            var values = new UXFDataRow()
            {
                ("pos_x", p.x),
                ("pos_y", p.y),
                ("pos_z", p.z)
            };

            return values;
        }
    }
}

