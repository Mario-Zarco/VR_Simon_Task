using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

// TODO: Comments
namespace VRSimonTask
{
    public class TargetsManager : MonoBehaviour
    {
        public Session session;

        public TargetController leftTarget;
        public TargetController rightTarget;

        public void SetupTargets(Trial trial)
        {
            if (trial.number > session.settings.GetInt("n_baseline_trials"))
            {
                TargetPosition correctTargetPosition = (TargetPosition) trial.settings.GetObject("correct_target_position");

                leftTarget.Setup(correctTargetPosition == TargetPosition.Left);
                rightTarget.Setup(correctTargetPosition == TargetPosition.Right);
            }
        }

        public void ResetToNormal(Trial trial)
        {
            if (trial.number > session.settings.GetInt("n_baseline_trials"))
            {
                leftTarget.ResetToNormal();
                rightTarget.ResetToNormal();
            }
        }

        public void SetupTargetsBaseline(Trial trial)
        {
            if (trial.number <= session.settings.GetInt("n_baseline_trials"))
            {
                TargetPosition correctTargetPosition = (TargetPosition) trial.settings.GetObject("correct_target_position");

                leftTarget.SetupBaseline(correctTargetPosition == TargetPosition.Left);
                rightTarget.SetupBaseline(correctTargetPosition == TargetPosition.Right);
            }
        }

        public void ResetToNormalBaseline(Trial trial)
        {
            if (trial.number < session.settings.GetInt("n_baseline_trials"))
            {
                leftTarget.ResetToNormalBaseline();
                rightTarget.ResetToNormalBaseline();   
            }
            else if (trial.number == session.settings.GetInt("n_baseline_trials"))
            {
                leftTarget.targetMeshRenderer.enabled = true;
                rightTarget.targetMeshRenderer.enabled = true;
            }
        }

    }

    public enum TargetPosition
    {
        Left, Right
    }
}
