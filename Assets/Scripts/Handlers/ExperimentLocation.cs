using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UXF;

namespace VRSimonTask
{
    public class ExperimentLocation : MonoBehaviour
    {
        public GameObject experiment;

        private bool adjusted = false;

        private Vector3 position;

        private float new_experiment_y;

        public GameObject leftStimulus;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) && !adjusted)
            {
                position = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.Head);
                Debug.Log("THE LOCATION OF THE EXPERIMENT WAS ADJUSTED");
                Debug.Log("Head Position: " + position.ToString());
                Debug.Log("Experiment Position: " + experiment.transform.position.ToString());
                Debug.Log("Left Stimulus position: " + leftStimulus.transform.position.ToString("F4"));
                new_experiment_y = position[1] - 1.6f;
                experiment.transform.Translate(0, new_experiment_y, 0);
                adjusted = true;
                Debug.Log("Updated Experiment Position: " + experiment.transform.position.ToString());
                Debug.Log("Updated Left Stimulus position: " + leftStimulus.transform.position.ToString("F4"));
            }
        }
    }
}

