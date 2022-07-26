using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RedTeam19;

public class SceneChangeAfterTime : MonoBehaviour
{
   
     public float delay = 3;
    public string NewLevel = "Home";
    VRScreenEffects VRFX;
    [SerializeField] GameObject screenFade;

    private void Awake()
    {
        VRFX = screenFade.GetComponent<VRScreenEffects>();
    }


    void Start()
    {
        StartCoroutine(LoadLevelAfterDelay(delay));
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
       
        yield return new WaitForSeconds(delay);
        VRFX.FadeOut();
        SceneManager.LoadScene(NewLevel);
       
    }
}
