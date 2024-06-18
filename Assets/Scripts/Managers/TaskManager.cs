using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;
using UXF;
using ViveSR.anipal.Eye;
using TMPro;

namespace VRSimonTask
{
    public class TaskManager : MonoBehaviour
    {
        public GameObject experiment;
        public GameObject canvas;
        public GameObject calibrateButton;
        public GameObject backButton;
        public GameObject nextButton;
        public GameObject continueButton;
        public GameObject instructions;
        public GameObject image;
        
        private TextMeshProUGUI instructionsText;

        public GameObject controller;
        private Collider controllerCollider;
        public SteamVR_LaserPointer controllerLaserPointer;

        public Session session;
        public MotionSicknessHandler motionSicknessHandler;

        private static int numExtraBlocks;

        private CanvasController canvasController;

        private void Awake()
        {
            instructionsText = instructions.GetComponent<TextMeshProUGUI>();

            controllerCollider = controller.GetComponent<Collider>();

            canvasController = canvas.GetComponent<CanvasController>();
        }
        
        public void SetInstructionsEndPracticeBlock()
        {
            if (session.settings.GetInt("counterbalance") == 0)
            {
                instructionsText.text = "The practice round has finished. Press “Continue” to begin the first block. \n\n" + 
                                         " Remember, if you see a star, move your hand to touch the right target. \n " +
                                         "If you see heart, move your hand to touch the left target. \n";
            }
            else
            {
                instructionsText.text = "The practice round has finished. Press “Continue” to begin the first block. \n\n" +
                                         "Remember, if you see a star, move your hand to touch the left target. \n " + 
                                         "If you see heart, move your hand to touch the right target. \n";
            }
        }

        IEnumerator EndOfExperiment()
        {
            Debug.Log("Finalizing Session");
            yield return new WaitForSeconds(10.0f);
            session.End();
        }

        public void LastTrialPerBlock(Trial trial)
        {
            /*
             *  Motion Sickness Event
             */
            if (motionSicknessHandler.inMotionSickness())
            {
                instructionsText.text = "Please press “Calibrate.”";
                controllerCollider.enabled = false;
                controllerLaserPointer.holder.SetActive(true);
                controllerLaserPointer.pointer.SetActive(true);
                experiment.SetActive(false);
                canvas.SetActive(true);
                calibrateButton.SetActive(true);
                backButton.SetActive(false);
                nextButton.SetActive(false);
                continueButton.SetActive(false);
                instructions.SetActive(true);
                image.SetActive(false);
                if (motionSicknessHandler.isExtraBlock())
                {
                    numExtraBlocks = numExtraBlocks + 1;
                }
                motionSicknessHandler.ResetToNormal();
            }
            /*
             * Last trial of the last block
             */
            else if (trial.session.CurrentTrial == session.LastTrial)
            {
                instructionsText.text = "This is the end of the experiment.";
                controllerCollider.enabled = false;
                controllerLaserPointer.holder.SetActive(false);
                controllerLaserPointer.pointer.SetActive(false);
                experiment.SetActive(false);
                canvas.SetActive(true);
                calibrateButton.SetActive(false);
                backButton.SetActive(false);
                nextButton.SetActive(false);
                continueButton.SetActive(false);
                instructions.SetActive(true);
                image.SetActive(false);
                StartCoroutine(EndOfExperiment());
            }
            /*
             *  Last trial of experimental blocks
             */
            else if (trial.numberInBlock == trial.settings.GetInt("n_experimental_trials") && (trial.session.currentBlockNum != 1 || trial.session.currentBlockNum != 2))
            {
                /*
                 * The participant has to take a break if they have finished half of the total number of blocks
                 * This considers the number of extra blocks due to Motion Sickness
                 * 
                 */
                
                Debug.Log("CURRENT BLOCK NUMBER " + session.currentBlockNum + " End of Experimental Block");
                
                if ((session.currentBlockNum - 2 )*2 == (session.blocks.Count - 2 + numExtraBlocks))
                {
                    instructionsText.text = 
                        "Please take a break and then press “Calibrate” whenever you are ready."; // Mandatory Break
                    controllerCollider.enabled = false;
                    controllerLaserPointer.holder.SetActive(true);
                    controllerLaserPointer.pointer.SetActive(true);
                    experiment.SetActive(false);
                    canvas.SetActive(true);
                    calibrateButton.SetActive(true);
                    backButton.SetActive(false);
                    nextButton.SetActive(false);
                    continueButton.SetActive(false);
                    instructions.SetActive(true);
                    image.SetActive(false);
                }
                else
                {
                    instructionsText.text =
                        "If you took a break and removed the headset, please press “Calibrate.” \n" +
                        "Otherwise, please press “Continue”."; // Optional Break
                    controllerCollider.enabled = false;
                    controllerLaserPointer.holder.SetActive(true);
                    controllerLaserPointer.pointer.SetActive(true);
                    experiment.SetActive(false);
                    canvas.SetActive(true);
                    calibrateButton.SetActive(true);
                    backButton.SetActive(false);
                    nextButton.SetActive(false);
                    continueButton.SetActive(true);
                    instructions.SetActive(true);
                    image.SetActive(false);
                }
            }
            /*
             * Last trial of practice block
             */
            else if (trial.numberInBlock == trial.settings.GetInt("n_practice_trials") && trial.session.currentBlockNum == 2)
            {
                Debug.Log("CURRENT BLOCK NUMBER " + session.currentBlockNum + " End of Practice Trials");
                
                SetInstructionsEndPracticeBlock();
                controllerCollider.enabled = false;
                controllerLaserPointer.holder.SetActive(true);
                controllerLaserPointer.pointer.SetActive(true);
                experiment.SetActive(false);
                canvas.SetActive(true);
                calibrateButton.SetActive(true);
                backButton.SetActive(false);
                nextButton.SetActive(false);
                continueButton.SetActive(true);
                instructions.SetActive(true);
                image.SetActive(false);
            }
            else if (trial.numberInBlock == trial.settings.GetInt("n_baseline_trials") && trial.session.currentBlockNum == 1)
            {
                Debug.Log("CURRENT BLOCK NUMBER " + session.currentBlockNum + " End of Baseline Trials");
                
                controllerCollider.enabled = false;
                controllerLaserPointer.holder.SetActive(true);
                controllerLaserPointer.pointer.SetActive(true);
                experiment.SetActive(false);
                canvas.SetActive(true);
                calibrateButton.SetActive(true);
                backButton.SetActive(true);
                nextButton.SetActive(true);
                continueButton.SetActive(false);
                instructions.SetActive(true);
                image.SetActive(true);
                canvasController.SetNewCanvasState();
            }
        }
    }
}

