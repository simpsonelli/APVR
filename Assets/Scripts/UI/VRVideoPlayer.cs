using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRVideoPlayer : MonoBehaviour
{
    public Transform target;
    public GameObject XRRig;
    public ActionBasedContinuousMoveProvider canMove;
    public AudioSource themeMusic;

    public void SpaceMaschine()
    {
        themeMusic.Stop();
        XRRig.transform.position = target.position;
        canMove.enabled = false;
    }

    public void Pandemnation()
    {
        themeMusic.Stop();
        XRRig.transform.position = target.position;
        canMove.enabled = false;
    }

    public void BoomDoom()
    {
        themeMusic.Stop();
        XRRig.transform.position = target.position;
        canMove.enabled = false;
    }

    public void Molly()
    {
        themeMusic.Stop();
        XRRig.transform.position = target.position;
        canMove.enabled = false;
    }
}
