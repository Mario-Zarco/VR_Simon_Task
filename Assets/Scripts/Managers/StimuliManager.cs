using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace VRSimonTask
{
    public class StimuliManager : MonoBehaviour
    {
        public Session session;

        public StimulusController leftStimulus;
        public StimulusController rightStimulus;
        
        public void SetupStimuli(Trial trial)
        {
            if (trial.number > session.settings.GetInt("n_baseline_trials"))
            {
                StimulusPosition correctStimulusPosition = (StimulusPosition) trial.settings.GetObject("correct_stimulus_position");
            
                leftStimulus.Setup(correctStimulusPosition == StimulusPosition.Left);
                rightStimulus.Setup(correctStimulusPosition == StimulusPosition.Right);
            
                leftStimulus.EnableText();
                rightStimulus.EnableText();
            }
        }

        public void ResetToNormal(Trial trial)
        {
            if (trial.number > session.settings.GetInt("n_baseline_trials"))
            {
                leftStimulus.ResetToNormal();
                rightStimulus.ResetToNormal();
            }
        }
        
    }

    public enum StimulusPosition
    {
        Left, Right
    }
}

