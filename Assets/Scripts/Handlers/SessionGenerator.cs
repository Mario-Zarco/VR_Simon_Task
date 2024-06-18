using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace VRSimonTask
{
    /// <summary>
    /// Generate the session
    /// </summary>
    public class SessionGenerator : MonoBehaviour
    {
        /// <summary>
        /// Generate the correct number of blocks and trials based on the settings profile (json file)
        /// </summary>
        /// <param name="session">UXF session</param>
        public void GenerateExperiment(Session session)
        {
            /*
             * BASELINE BLOCK
             */
            int numberOfBaselineTrials = session.settings.GetInt("n_baseline_trials");

            Block baselineCongruentBlock = session.CreateBlock(numberOfBaselineTrials);

            for (int i = 1; i <= numberOfBaselineTrials/2; i++)
            {
                baselineCongruentBlock.GetRelativeTrial(i).settings.SetValue("type", "congruent");
                baselineCongruentBlock.GetRelativeTrial(i).settings.SetValue("correct_target_position", TargetPosition.Left);
                // baselineCongruentBlock.GetRelativeTrial(i).settings.SetValue("correct_stimulus_position", TargetPosition.Left);
            }
            
            for (int i = numberOfBaselineTrials/2 + 1; i <= numberOfBaselineTrials; i++)
            {
                baselineCongruentBlock.GetRelativeTrial(i).settings.SetValue("type", "congruent");
                baselineCongruentBlock.GetRelativeTrial(i).settings.SetValue("correct_target_position", TargetPosition.Right);
                // baselineCongruentBlock.GetRelativeTrial(i).settings.SetValue("correct_stimulus_position", TargetPosition.Right);
            }
            
            baselineCongruentBlock.trials.Shuffle();
            
            /*
             * PRACTICE BLOCK
             */
            int numberOfPracticeTrials = session.settings.GetInt("n_practice_trials");

            Block practiceBlock = session.CreateBlock(numberOfPracticeTrials);
            
            for (int i = 1; i <= numberOfPracticeTrials/4; i++)
            {
                /*
                 * CONGRUENT 
                 */
                practiceBlock.GetRelativeTrial(i).settings.SetValue("type", "congruent");
                practiceBlock.GetRelativeTrial(i).settings.SetValue("correct_target_position", TargetPosition.Left);
                practiceBlock.GetRelativeTrial(i).settings.SetValue("correct_stimulus_position", StimulusPosition.Left);
            }

            for (int i = numberOfPracticeTrials/4 + 1; i <= numberOfPracticeTrials/2; i++)
            {
                /*
                 * CONGRUENT
                 */
                practiceBlock.GetRelativeTrial(i).settings.SetValue("type", "congruent");
                practiceBlock.GetRelativeTrial(i).settings.SetValue("correct_target_position", TargetPosition.Right);
                practiceBlock.GetRelativeTrial(i).settings.SetValue("correct_stimulus_position", StimulusPosition.Right);
            }

            for (int i = numberOfPracticeTrials/2 + 1; i <= 3*numberOfPracticeTrials/4; i++)
            {
                /*
                 * INCONGRUENT
                 */
                practiceBlock.GetRelativeTrial(i).settings.SetValue("type", "incongruent");
                practiceBlock.GetRelativeTrial(i).settings.SetValue("correct_target_position", TargetPosition.Left);
                practiceBlock.GetRelativeTrial(i).settings.SetValue("correct_stimulus_position", StimulusPosition.Right);
            }

            for (int i = 3*numberOfPracticeTrials/4 + 1; i <= numberOfPracticeTrials; i++)
            {
                /*
                 * INCONGRUENT
                 */
                practiceBlock.GetRelativeTrial(i).settings.SetValue("type", "incongruent");
                practiceBlock.GetRelativeTrial(i).settings.SetValue("correct_target_position", TargetPosition.Right);
                practiceBlock.GetRelativeTrial(i).settings.SetValue("correct_stimulus_position", StimulusPosition.Left);
            }

            practiceBlock.trials.Shuffle();
            
            /*
             * Experimental Block
             */
            int numberOfTrials = session.settings.GetInt("n_experimental_trials");

            int numberOfBlocks = session.settings.GetInt("n_experimental_blocks");
            
            Block[] experimentalBlocks = new Block[numberOfBlocks];

            for (int i = 0; i < numberOfBlocks; i++)
            {
                experimentalBlocks[i] = new Block((uint) numberOfTrials, session);

                for (int j = 1; j <= numberOfTrials / 4; j++)
                {
                    experimentalBlocks[i].GetRelativeTrial(j).settings.SetValue("type", "congruent");
                    experimentalBlocks[i].GetRelativeTrial(j).settings.SetValue("correct_target_position", TargetPosition.Left);
                    experimentalBlocks[i].GetRelativeTrial(j).settings.SetValue("correct_stimulus_position", StimulusPosition.Left);
                }
                
                for (int j = numberOfTrials/4 + 1; j <= numberOfTrials/2; j++)
                {
                    experimentalBlocks[i].GetRelativeTrial(j).settings.SetValue("type", "congruent");
                    experimentalBlocks[i].GetRelativeTrial(j).settings.SetValue("correct_target_position", TargetPosition.Right);
                    experimentalBlocks[i].GetRelativeTrial(j).settings.SetValue("correct_stimulus_position", StimulusPosition.Right);
                }
                
                for (int j = numberOfTrials/2 + 1; j <= 3*numberOfTrials/4; j++)
                {
                    experimentalBlocks[i].GetRelativeTrial(j).settings.SetValue("type", "incongruent");
                    experimentalBlocks[i].GetRelativeTrial(j).settings.SetValue("correct_target_position", TargetPosition.Left);
                    experimentalBlocks[i].GetRelativeTrial(j).settings.SetValue("correct_stimulus_position", StimulusPosition.Right);
                }
                
                for (int j = 3*numberOfTrials/4 + 1; j <= numberOfTrials; j++)
                {
                    experimentalBlocks[i].GetRelativeTrial(j).settings.SetValue("type", "incongruent");
                    experimentalBlocks[i].GetRelativeTrial(j).settings.SetValue("correct_target_position", TargetPosition.Right);
                    experimentalBlocks[i].GetRelativeTrial(j).settings.SetValue("correct_stimulus_position", StimulusPosition.Left);
                }
                
                experimentalBlocks[i].trials.Shuffle();
            }
        }
    }

}

