using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using System.IO;

namespace VRSimonTask
{
    public class Counterbalance : MonoBehaviour
    {
        public Session session;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        private void ReadTextFile()
        {
            // string path = Path.Combine(@"D:\Data", session.experimentName, session.ppid, 
            //     string.Format("S{0:000}", session.number), "participantdetails", "participant_details.csv");
            // Debug.Log(path);
            // StreamReader participantDetails = new StreamReader(path);
            // while (!participantDetails.EndOfStream)
            // {
            //     string line = participantDetails.ReadLine();
            //     Debug.Log(line);
            // }
            // foreach (var kvp in session.participantDetails)
            // {
            //     Debug.Log("Key = {0}, Value = {1}" + kvp.Key + kvp.Value);
            // }
            Debug.Log(session.participantDetails["counterbalance"]);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

