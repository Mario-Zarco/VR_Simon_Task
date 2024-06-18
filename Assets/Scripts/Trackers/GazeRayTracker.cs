using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace VRSimonTask
{
    public class GazeRayTracker : Tracker
    {
        private EyeTracking eyeTracking;
        private void Awake()
        {
            eyeTracking = GetComponentInParent<EyeTracking>();
        }

        protected override void SetupDescriptorAndHeader()
        {
            measurementDescriptor = "gazeray";
            
            customHeader = new string[]
            {
                "origin_x",
                "origin_y",
                "origin_z",
                "direction_x",
                "direction_y",
                "direction_z"
            };
        }

        protected override UXFDataRow GetCurrentValues()
        {
            Vector3 rayOrigin = eyeTracking.data_world.GazeRay.Origin;
            Vector3 rayDirection = eyeTracking.data_world.GazeRay.Direction;

            var values = new UXFDataRow()
            {
                ("origin_x", rayOrigin.x),
                ("origin_y", rayOrigin.y),
                ("origin_z", rayOrigin.z),
                ("direction_x", rayDirection.x),
                ("direction_y", rayDirection.y),
                ("direction_z", rayDirection.z),
            };

            return values;
        }
    }
}

