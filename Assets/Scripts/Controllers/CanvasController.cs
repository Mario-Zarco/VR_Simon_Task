using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data.Common;
using System.IO.Ports;
using UnityEngine.UI;
using Valve.VR.Extras;
using UXF;
using ViveSR.anipal.Eye;
using ViveSR;
using TMPro;
using UnityEngine.UIElements;


namespace VRSimonTask
{
    public class CanvasController : MonoBehaviour
    {
        public SteamVR_LaserPointer laserPointer;
        public GameObject controller;
        private Collider controllerCollider;

        public GameObject experiment;
        //public GameObject canvas;
        public GameObject calibrateButton;
        // public GameObject backButton;
        public GameObject nextButton;
        public GameObject continueButton;
        public GameObject instructions;
        public GameObject image;
        
        private TextMeshProUGUI instructionsText;
        
        private Texture2D textureImage;
        private RawImage rawImage;
        private Color colorImage;

        private TextMeshProUGUI nextButtonText;

        private int numInstruction = 0;
        private int totInstructions;

        // private int numBaselineInst = 0;
        // private int totBaselineInst;
        
        private bool startExperiment = false;

        public Session session;

        public CanvasState canvasState = CanvasState.BaseLine;

        private string[] baselineInstructions =
        {
            "Welcome! Press “Calibrate” whenever you are ready.",
            "In this first part of the experiment, you will respond by reaching one sphere",
            "To begin each trial, you must move your hand into the central starting sphere",
            "On each trial a cue will be shown to indicate that a sphere will appear shortly.",
            "Then move your hand to touch the target",
            "Remember to keep your hand in the central staring block until the sphere appears in each trial. Otherwise, the trial will restart.",
            "7"
        };
        
        private string[] initialInstructions =
        {
            "Please point and click on “Calibrate”.",
            "In this task, you will respond by reaching to one of two spheres.",
            "To begin each trial, you must move your hand into the central starting sphere.",
            "On each trial a cue will appear in-between the spheres. This cue indicates that a picture will appear shortly.",
            "Remember to keep your hand in the central staring block until the picture appears in each trial. Otherwise, the trial will restart.",
            "6",
            "7",
            "8"
        };
        
        public void SetInitialInstructions()
        {
            baselineInstructions[6] = "We will begin with a this first round of " +
                                      session.settings.GetInt("n_baseline_trials") + 
                                      " trials. Please respond as quickly and as accurately as possible.";
            
            // if (session.settings.GetInt("counterbalance") == 0)
            if (Convert.ToInt32(session.participantDetails["counterbalance"]) == 0)
            {
                initialInstructions[5] = "Regardless of where the picture appears, if you see a star, move your hand to touch the right target. \n " +
                                         "If you see a heart, move your hand to touch the left target. \n";
                initialInstructions[6] = "Remember that the star and the heart can appear on either side. \n" +
                                         "If you see a star, move your hand to touch the right target. If you see a heart, move your hand to touch the left target.";
            }
            else
            {
                initialInstructions[5] = "Regardless of where the picture appears, if you see a star, move your hand to touch the left target. \n " +
                                         "If you see a heart, move your hand to touch the right target. \n";
                initialInstructions[6] = "Remember that the star and the heart can appear on either side. \n" +
                                         "If you see a star, move your hand to touch the left target. If you see a heart, move your hand to touch the right target.";
            }
        
            initialInstructions[7] = "We will begin with a practice round of " +
                                     session.settings.GetInt("n_practice_trials") +
                                     " trials. Please respond as quickly and as accurately as possible.";
        }

        private void Awake()
        {
            laserPointer.PointerClick += PointerClick;
            
            controllerCollider = controller.GetComponent<Collider>();
            
            instructionsText = instructions.GetComponent<TextMeshProUGUI>();

            rawImage = image.GetComponent<RawImage>();
            
            nextButtonText = nextButton.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            controllerCollider.enabled = false;
            
            continueButton.SetActive(false);
            
            instructionsText.text = baselineInstructions[0];
            
            rawImage.color = new Color(255, 255, 255, 0);

            totInstructions = baselineInstructions.Length - 1;

            // totBaselineInst = baselineInstructions.Length - 1;
        }

        public void SetNewCanvasState()
        {
            nextButtonText.text = "Next";
            startExperiment = false;
            canvasState = CanvasState.Experiment;
            instructionsText.text = initialInstructions[0];
            numInstruction = 0;
            totInstructions = initialInstructions.Length - 1;
        }

        private void Next()
        {
            if (numInstruction < totInstructions)
            {
                numInstruction += 1;
                
                if (canvasState == CanvasState.Experiment)
                {
                    // Debug.Log("Next" + totInstructions + numInstruction);
        
                    if (numInstruction == 1 || numInstruction == 2 || numInstruction == 3)
                    {
                        rawImage.color = new Color(255, 255, 255, 255);
                        textureImage = Resources.Load("Images/instructions" + numInstruction.ToString()) as Texture2D;
                        rawImage.texture = textureImage;
                    }
                    else if (numInstruction == 5)
                    {
                        rawImage.color = new Color(255, 255, 255, 255);
                        textureImage = Resources.Load("Images/instructions5_" + session.settings.GetInt("counterbalance").ToString()) as Texture2D;
                        rawImage.texture = textureImage;
                    }
                    else
                    {
                        rawImage.color = new Color(255, 255, 255, 0);
                    }
        
                    instructionsText.text = initialInstructions[numInstruction];
                }
                else
                {
                    instructionsText.text = baselineInstructions[numInstruction];
                }
            }
        }
        
        private void Back()
        {
            if (numInstruction > 0)
            {
                numInstruction -= 1;

                if (canvasState == CanvasState.Experiment)
                {
                    // Debug.Log("Back" + totInstructions + numInstruction);
                
                    if (numInstruction == 1 || numInstruction == 2 || numInstruction == 3)
                    {
                        rawImage.color = new Color(255, 255, 255, 255);
                        textureImage = Resources.Load("Images/instructions" + numInstruction.ToString()) as Texture2D;
                        rawImage.texture = textureImage;
                    }
                    else if (numInstruction == 5)
                    {
                        rawImage.color = new Color(255, 255, 255, 255);
                        textureImage = Resources.Load("Images/instructions5_" + session.settings.GetInt("counterbalance").ToString()) as Texture2D;
                        rawImage.texture = textureImage;
                    }
                    else
                    {
                        rawImage.color = new Color(255, 255, 255, 0);
                    }
        
                    instructionsText.text = initialInstructions[numInstruction];
                }
                else
                {
                    instructionsText.text = baselineInstructions[numInstruction];
                }
            }
        }
        
        public void PointerClick(object sender, PointerEventArgs e)
        {
            if (e.target.name == "Calibrate")
            {
                // TODO: Verify this is correct
                int succesfulcalibration = SRanipal_Eye_API.LaunchEyeCalibration(IntPtr.Zero);
                if (succesfulcalibration == (int) Error.WORK)
                {
                    calibrateButton.SetActive(false);
                    Debug.Log("Successful Calibration after Motion Sickness");
                    if (nextButton.activeSelf)
                    {
                        instructionsText.text = "Calibration was successful. Please press “Next”.";
                    }
                    else
                    {
                        continueButton.SetActive(true);
                        instructionsText.text = "Press “Continue” when you are ready.";
                    }
                    
                }
            }
            
            if (e.target.name == "Next")
            {
                if (!startExperiment)
                {
                    Next();
                    if (numInstruction == totInstructions)
                    {
                        nextButtonText.text = "Start";
                        startExperiment = true;
                    }
                }
                else
                {
                    experiment.SetActive(true);
                    laserPointer.holder.SetActive(false);
                    laserPointer.pointer.SetActive(false);
                    controllerCollider.enabled = true;
                    gameObject.SetActive(false);
                }
            }
            
            if (e.target.name == "Back")
            {
                Back();
                
                if (nextButtonText.text == "Start")
                {
                    startExperiment = false;
                    nextButtonText.text = "Next";
                }
            }
        
            if (e.target.name == "Continue")
            {
                experiment.SetActive(true);
                laserPointer.holder.SetActive(false);
                laserPointer.pointer.SetActive(false);
                controllerCollider.enabled = true;
                gameObject.SetActive(false);
            }
        }
    }
    
    public enum CanvasState
    {
            BaseLine, Experiment
    }
}

