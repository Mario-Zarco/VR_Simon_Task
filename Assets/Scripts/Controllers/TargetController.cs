using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

// TODO: Comments
namespace VRSimonTask
{
    public class TargetController : MonoBehaviour
    {
        public Collider targetCollider;
        public MeshRenderer targetMeshRenderer;

        public bool? isCorrect = null;

        public AudioClip audioCorrect;
        public AudioClip audioIncorrect;

        public Session session;

        public GameObject controller;

        private void Awake()
        {
            targetCollider = GetComponent<Collider>();
            targetCollider.enabled = false;

            targetMeshRenderer = GetComponent<MeshRenderer>();
            targetMeshRenderer.enabled = false;
        }

        private void OnTriggerExit(Collider other)
        {
            isCorrect = null;
            targetCollider.enabled = false;
        }

        public void Setup(bool thisIcCorrect)
        {
            isCorrect = thisIcCorrect;
            targetCollider.enabled = true;
        }

        public void SetupBaseline(bool thisIsCorrect)
        {
            isCorrect = thisIsCorrect;
            if (thisIsCorrect)
            {
                targetCollider.enabled = true;
                targetMeshRenderer.enabled = true;
            }
        }

        public void ResetToNormal()
        {
            targetCollider.enabled = false;
        }

        public void ResetToNormalBaseline()
        {
            targetCollider.enabled = false;
            targetMeshRenderer.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Controller"))
            {
                if (isCorrect != null)
                {
                    Vector3 p = controller.transform.position;

                    session.CurrentTrial.result["final_time"] = Time.time;
                    session.CurrentTrial.result["fin_pos_x"] = p.x;
                    session.CurrentTrial.result["fin_pos_y"] = p.y;
                    session.CurrentTrial.result["fin_pos_z"] = p.z;

                    if (session.CurrentTrial.number > session.settings.GetInt("n_baseline_trials"))
                    {
                        if (isCorrect == true)
                        {
                            AudioSource.PlayClipAtPoint(audioCorrect, new Vector3(0, 0, 0), 0.1f);
                            session.CurrentTrial.result["accuracy"] = 1;
                        }
                        else
                        {
                            AudioSource.PlayClipAtPoint(audioIncorrect, new Vector3(0, 0, 0), 0.1f);
                            session.CurrentTrial.result["accuracy"] = 0;
                        }
                    }

                    session.CurrentTrial.End();
                }
            }
        }
    }
}

