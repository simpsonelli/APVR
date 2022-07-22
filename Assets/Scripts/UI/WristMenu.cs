using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using RedTeam19;

public class WristMenu : MonoBehaviour
{
    public GameObject wristUI;
    public bool activeWristUI;
    public AudioSource openMenu;
    public XRRig moveProvider;
     VRScreenEffects VRFX;
    [SerializeField] GameObject screenFade;

    private void Awake()
    {
        VRFX = screenFade.GetComponent<VRScreenEffects>();
    }
    private void Start()
    {
        DisplayWristUI();
    }

    public void OnButton1Clicked()
    {
        StartCoroutine(DelaySceneLoad());
        IEnumerator DelaySceneLoad()
        {
            VRFX.FadeOut();
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Gallery");
        }
    }

    public void OnButton2Clicked()
    {
        StartCoroutine(DelaySceneLoad());
        IEnumerator DelaySceneLoad()
        {
            VRFX.FadeOut();
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Cinema");
        }
    }

    public void OnButton3Clicked()
    {
        StartCoroutine(DelaySceneLoad());
        IEnumerator DelaySceneLoad()
        {
            VRFX.FadeOut();
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("360Video");
        }
    }

    public void OnButton4Clicked()
    {
        VRFX.FadeOut();
        Application.Quit();
        Debug.Log("Quit app");
    }


    public void MenuPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
            DisplayWristUI();
    }

    public void DisplayWristUI()
    {
        if(activeWristUI)
        {
            wristUI.SetActive(false);
            activeWristUI = false;
        }
        else if (!activeWristUI)
        {
            wristUI.SetActive(true);
            activeWristUI = true;
            openMenu.Play();
        }
    }
}
