using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace VRSimonTask
{
    public class StimulusController : MonoBehaviour
    {
        private TextMesh stimulusText;

        public Session session;

        public Collider stimulusCollider;

        public bool? isCorrect = null;

        private void Awake()
        {
            stimulusText = transform.GetChild(0).gameObject.GetComponent<TextMesh>();
            stimulusCollider = GetComponent<Collider>();
            ResetToNormal();
        }

        public void ResetToNormal()
        {
            stimulusText.text = String.Empty;
            enabled = false;
            stimulusCollider.enabled = false;
        }

        public void Setup(bool thisIsCorrect)
        {
            isCorrect = thisIsCorrect;
            enabled = true;
            stimulusCollider.enabled = true;
        }

        public void EnableText()
        {
            if ((bool) isCorrect)
            {
                StimulusPosition correctStimulusPosition = (StimulusPosition)session.CurrentTrial.settings.GetObject("correct_stimulus_position");
                string type = session.CurrentTrial.settings.GetString("type");
                // if (session.settings.GetInt("counterbalance") == 0)
                if (Convert.ToInt32(session.participantDetails["counterbalance"]) == 0)
                {
                    session.CurrentTrial.result["counterbalance"] = 0;
                    if (type == "congruent")
                    {
                        if (correctStimulusPosition == StimulusPosition.Left)
                        {
                            stimulusText.text = "\u2665".ToString(); // Left = 1; Black heart = 1
                            session.CurrentTrial.result["stimulus"] = 1;
                            session.CurrentTrial.result["stimulus_location"] = 1;
                        }
                        if (correctStimulusPosition == StimulusPosition.Right)
                        {
                            stimulusText.text = "\u2605".ToString(); // Right = 2; Black star = 2
                            session.CurrentTrial.result["stimulus"] = 2;
                            session.CurrentTrial.result["stimulus_location"] = 2;
                        }
                        session.CurrentTrial.result["stimulus_presentation"] = Time.time;
                        session.CurrentTrial.result["congruency"] = 1;
                    }

                    if (type == "incongruent")
                    {
                        if (correctStimulusPosition == StimulusPosition.Left)
                        {
                            stimulusText.text = "\u2605".ToString(); // Left = 1; Black star = 2 
                            session.CurrentTrial.result["stimulus"] = 2;
                            session.CurrentTrial.result["stimulus_location"] = 1;
                        }
                        if (correctStimulusPosition == StimulusPosition.Right)
                        {
                            stimulusText.text = "\u2665".ToString(); // Right = 2; Black heart = 1
                            session.CurrentTrial.result["stimulus"] = 1;
                            session.CurrentTrial.result["stimulus_location"] = 2;
                        }
                        session.CurrentTrial.result["stimulus_presentation"] = Time.time;
                        session.CurrentTrial.result["congruency"] = 0;
                    }
                }
                else
                {
                    session.CurrentTrial.result["counterbalance"] = 1;
                    if (type == "congruent")
                    {
                        if (correctStimulusPosition == StimulusPosition.Left)
                        {
                            stimulusText.text = "\u2605".ToString(); // Left = 1; Black star = 2
                            session.CurrentTrial.result["stimulus"] = 2;
                            session.CurrentTrial.result["stimulus_location"] = 1;
                        }
                        if (correctStimulusPosition == StimulusPosition.Right)
                        {
                            stimulusText.text = "\u2665".ToString(); // Right = 2; Black heart = 1
                            session.CurrentTrial.result["stimulus"] = 1;
                            session.CurrentTrial.result["stimulus_location"] = 2;
                        }
                        session.CurrentTrial.result["stimulus_presentation"] = Time.time;
                        session.CurrentTrial.result["congruency"] = 1;
                    }

                    if (type == "incongruent")
                    {
                        if (correctStimulusPosition == StimulusPosition.Left)
                        {
                            stimulusText.text = "\u2665".ToString(); // Left = 1; Black heart = 1
                            session.CurrentTrial.result["stimulus"] = 1;
                            session.CurrentTrial.result["stimulus_location"] = 1;
                        }
                        if (correctStimulusPosition == StimulusPosition.Right)
                        {
                            stimulusText.text = "\u2605".ToString(); // Right = 2; Black star = 2
                            session.CurrentTrial.result["stimulus"] = 2;
                            session.CurrentTrial.result["stimulus_location"] = 2;
                        }
                        session.CurrentTrial.result["stimulus_presentation"] = Time.time;
                        session.CurrentTrial.result["congruency"] = 0;
                    }
                }
                
            }
        }
    }
}

