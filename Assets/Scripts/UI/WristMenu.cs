using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class WristMenu : MonoBehaviour
{
    public GameObject wristUI;
    public bool activeWristUI;
    public AudioSource openMenu;
   

    public XRRig moveProvider;

    private void Start()
    {
        DisplayWristUI();
    }

    public void OnButton1Clicked()
    {
        StartCoroutine(DelaySceneLoad());
        IEnumerator DelaySceneLoad()
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("Gallery_07072022");
        }
    }

    public void OnButton2Clicked()
    {
        StartCoroutine(DelaySceneLoad());
        IEnumerator DelaySceneLoad()
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("Cinema_07072022");
        }
    }


    public void OnButton3Clicked()
    {
        StartCoroutine(DelaySceneLoad());
        IEnumerator DelaySceneLoad()
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("360Video_07072022");
        }
    }

    public void OnButton4Clicked()
    {
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

        ////Deactivating UI Controller by default
        //wristUI.GetComponent<XRRayInteractor>().enabled = false;
        //wristUI.GetComponent<XRInteractorLineVisual>().enabled = false;

    }
}
