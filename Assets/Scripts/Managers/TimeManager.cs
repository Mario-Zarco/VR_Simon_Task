using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace VRSimonTask
{
    public class TimeManager : MonoBehaviour
    {
        public Session session;
        private Coroutine countdown;

        public AudioClip audioIncorrect;

        public GameObject controller;

        public void BeginCountdown()
        {
            countdown = StartCoroutine(Countdown());
        }

        public void StopCountdown()
        {
            StopCoroutine(countdown);
        }

        IEnumerator Countdown()
        {
            yield return new WaitForSeconds(2.5f);
            AudioSource.PlayClipAtPoint(audioIncorrect, new Vector3(0, 0, 0), 0.1f);
            Vector3 p = controller.transform.position;
            session.CurrentTrial.result["accuracy"] = 0;
            session.CurrentTrial.result["time_out"] = 1;
            session.CurrentTrial.result["final_time"] = Time.time;
            session.CurrentTrial.result["fin_pos_x"] = p.x;
            session.CurrentTrial.result["fin_pos_y"] = p.y;
            session.CurrentTrial.result["fin_pos_z"] = p.z;
            session.EndCurrentTrial();
        }
    }
}


