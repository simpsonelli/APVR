using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] AudioSource openMenu;
   public void JustStart()
    {
        openMenu.Play();
        SceneManager.LoadScene("Gallery");
    }

   
}
