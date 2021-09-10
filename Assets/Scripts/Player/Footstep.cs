using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    [SerializeField] AudioSource footstepAudio;         // 坷叼坷 家胶(胶乔目)
    [SerializeField] AudioClip walkClip;                // 叭绰 家府.
    [SerializeField] AudioClip runClip;                 // 第绰 家府.

    public void OnPlay(bool isWalk, bool isRun)
    {
        // 惯家府 (Footstep)
        bool isPlayingAudio = footstepAudio.isPlaying;
        if (isWalk)
        {
            footstepAudio.clip = walkClip;
            if (isPlayingAudio == false)
                footstepAudio.Play();
        }
        else if (isRun)
        {
            footstepAudio.clip = runClip;
            if (isPlayingAudio == false)
                footstepAudio.Play();
        }
        else
        {
            footstepAudio.Stop();
        }
    }
}
