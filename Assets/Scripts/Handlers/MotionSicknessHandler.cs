using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UXFExtensionMethods;

namespace VRSimonTask
{
    public class MotionSicknessHandler : MonoBehaviour
    {
        public Session session;

        private static bool motionSickness = false;

        public TaskManager taskManager;

        public static bool extraBlock = false;

        public void ResetToNormal()
        {
            motionSickness = false;
            extraBlock = false;
        }

        public bool inMotionSickness()
        {
            return motionSickness;
        }

        public bool isExtraBlock()
        {
            return extraBlock;
        }

        private void Update()
        {
            /*
             * Press SPACE BAR if a block needs to be stopped 
             */
            if (Input.GetKeyDown(KeyCode.Space) && session.currentBlockNum != 1)
            {
                Debug.Log("Motion Sickness Event has been called");
                motionSickness = true;
                float minPercentageOfTrials = 0.90f;
                int numberOfTrialsInBlock = session.CurrentBlock.trials.Count;
                int previousTrialNum = session.CurrentTrial.numberInBlock;
                float percentageOfTrials = Convert.ToSingle(previousTrialNum) / numberOfTrialsInBlock;
                if (percentageOfTrials < minPercentageOfTrials)
                {
                    extraBlock = true;
                    if (session.currentBlockNum == 1)
                    {
                        Block newPracticeBlock = 
                            session.CreateBlock(session.settings.GetInt("n_practice_trials"));
                        CreateTrialsForBlock(newPracticeBlock);
                    }
                    else
                    {
                        Block newExperimentalBlock =
                            session.CreateBlock(session.settings.GetInt("n_experimental_trials"));
                        CreateTrialsForBlock(newExperimentalBlock);
                    }
                }
                for (int i = 0; i < numberOfTrialsInBlock - previousTrialNum; i++)
                {
                    session.NextTrial.BeginExtension();
                    session.CurrentTrial.EndExtension();
                }
                taskManager.LastTrialPerBlock(session.CurrentTrial);
            }
        }

        private void CreateTrialsForBlock(Block newBlock)
        {
            int numberOfTrials = newBlock.trials.Count;
            
            for (int i = 1; i <= numberOfTrials/4; i++)
            {
                newBlock.GetRelativeTrial(i).settings.SetValue("type", "congruent");
                newBlock.GetRelativeTrial(i).settings.SetValue("correct_target_position", TargetPosition.Left);
                newBlock.GetRelativeTrial(i).settings.SetValue("correct_stimulus_position", StimulusPosition.Left);
            }

            for (int i = numberOfTrials/4 + 1; i <= numberOfTrials/2; i++)
            {
                newBlock.GetRelativeTrial(i).settings.SetValue("type", "congruent");
                newBlock.GetRelativeTrial(i).settings.SetValue("correct_target_position", TargetPosition.Right);
                newBlock.GetRelativeTrial(i).settings.SetValue("correct_stimulus_position", StimulusPosition.Right);
            }

            for (int i = numberOfTrials/2 + 1; i <= 3*numberOfTrials/4; i++)
            {
                newBlock.GetRelativeTrial(i).settings.SetValue("type", "incongruent");
                newBlock.GetRelativeTrial(i).settings.SetValue("correct_target_position", TargetPosition.Left);
                newBlock.GetRelativeTrial(i).settings.SetValue("correct_stimulus_position", StimulusPosition.Right);
            }

            for (int i = 3*numberOfTrials/4 + 1; i <= numberOfTrials; i++)
            {
                newBlock.GetRelativeTrial(i).settings.SetValue("type", "incongruent");
                newBlock.GetRelativeTrial(i).settings.SetValue("correct_target_position", TargetPosition.Right);
                newBlock.GetRelativeTrial(i).settings.SetValue("correct_stimulus_position", StimulusPosition.Left);
            }

            newBlock.trials.Shuffle();
            
        }
    }
}

