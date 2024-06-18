using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace VRSimonTask
{
    public class GazeFocusTracker : Tracker
    {

        private GazeFocusDetector detector;

        private void Awake()
        {
            detector = GetComponent<GazeFocusDetector>();
        }

        protected override void SetupDescriptorAndHeader()
        {
            measurementDescriptor = "gazetracker";
            
            customHeader = new string[]
            {
                "focus"
            };
        }

        protected override UXFDataRow GetCurrentValues()
        {
            int f = detector.focus ? 1 : 0;

            var values = new UXFDataRow()
            {
                ("focus", f.ToString())
            };
                
            return values;
        }
    }
}

