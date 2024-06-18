using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UXFExamples;

namespace VRSimonTask
{
    public class StartingPointController : MonoBehaviour
    {
        public Session session;
        private StartingState state = StartingState.Waiting;
        private Coroutine cueCoroutine;

        public CueController cue;

        public GameObject controller;

        IEnumerator CueSequence()
        {
            state = StartingState.GetReady;
            yield return new WaitForSeconds(1.0f);
            cue.EnableCue();
            yield return new WaitForSeconds(1.0f);
            cue.DisableCue();
            // yield return new WaitForSeconds(0.1f);
            state = StartingState.Go;
            session.BeginNextTrial();
        }

        public void ResetToNormal()
        {
            state = StartingState.Waiting;
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (state)
            {
                case StartingState.Waiting:
                    cueCoroutine = StartCoroutine(CueSequence());
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            switch (state)
            {
                case StartingState.GetReady:
                    StopCoroutine(cueCoroutine);
                    cue.DisableCue();
                    state = StartingState.Waiting;
                    break;
                case StartingState.Go:
                    Vector3 p = controller.transform.position;
                    session.CurrentTrial.result["time_out"] = 0;
                    session.CurrentTrial.result["initial_time"] = Time.time;
                    session.CurrentTrial.result["init_pos_x"] = p.x;
                    session.CurrentTrial.result["init_pos_y"] = p.y;
                    session.CurrentTrial.result["init_pos_z"] = p.z;
                    break;
            }
        }
    }

    public enum StartingState
    {
        Waiting, GetReady, Go
    }
}

