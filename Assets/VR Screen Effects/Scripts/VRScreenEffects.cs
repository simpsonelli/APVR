using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace RedTeam19
{
    [RequireComponent(typeof(Canvas))]

    public class VRScreenEffects : MonoBehaviour
    {

        private Image img;

        [Header("Screen Flash Options")]

        [Tooltip("How long the Screen Flash will last on screen")]
        [SerializeField] float flashSpeed = .5f;

        [Tooltip("The Color that will be flashed on player's screen")]
        [SerializeField] Color flashColor = Color.red;

        [Tooltip("Event called when flash effect is completed")]
        [SerializeField] UnityEvent OnFlashComplete;



        [Header("Screen Fade Options")]

        [Tooltip("How fast the screen will fade")]
        [SerializeField] float fadeSpeed = 1.5f;

        [Tooltip("Color that will fade into")]
        [SerializeField] private Color fadeColor = Color.black;

        [Tooltip("Event called when fade out effect is completed")]
        [SerializeField] UnityEvent OnFadeOutComplete;

        [Tooltip("Event called when fade in effect is completed")]
        [SerializeField] UnityEvent OnFadeInComplete;


        //Ensures the proper components are added to the object
        private void OnValidate()
        {
            SetupVRScreenEffects();
        }

        private void Awake()
        {
            img = GetComponentInChildren<Image>();
            img.color = Color.clear;
        }

        public void SetupVRScreenEffects()
        {
            img = GetComponentInChildren<Image>();

            if (img == null)
            {
                GameObject VRImage = new GameObject();
                VRImage.gameObject.name = "VR_Screen_Image";
                VRImage.transform.parent = this.transform;
                VRImage.AddComponent<Image>();
                Camera camera = GetComponentInParent<Camera>(); 

                if(camera  != null)
                {
                    this.transform.position = new Vector3(0, 0, camera.nearClipPlane + .01f);
                }
                else
                {
                    this.transform.position = new Vector3(0, 0, 0.31f);
                }
                
                this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); 
            }

            Camera cam = GetComponentInParent<Camera>(); 

            if(cam != null && this.transform.localPosition.z <= cam.nearClipPlane) 
            {
                Debug.LogError("Move the VR Screen Effects Canvas further away from the Camera than the near clipping plan, otherwise you won't see the effects"); 
            }
        }

        #region Screen Flash

        /// <summary>
        /// Flashes player's screen with a predetermined color and speed
        /// </summary>
        public void ScreenFlash()
        {
            StartCoroutine(DoScreenFlash());
        }

        IEnumerator DoScreenFlash()
        {
            float t = 0, r = 1 / flashSpeed;

            Color s = flashColor;
            Color e = flashColor;
            e.a = 0;
            while (t < 1)
            {
                t += Time.deltaTime * r;
                img.color = Color.Lerp(s, e, t);
                yield return null;
            }

            OnFlashComplete?.Invoke();
        }
        #endregion

        #region Screen Fade

        //Clear version of the choosen fade color
        Color ClearColor
        {
            get
            {
                Color clear = fadeColor;
                clear.a = 0;
                return clear;
            }
        }
        /// <summary>
        /// Fades screen from a dark color to 100% transparent
        /// </summary>
        public void FadeIn()
        {
            StartCoroutine(DoFadeIn(ClearColor, fadeSpeed));
        }

        /// <summary>
        /// Fades screen from a 100% transparent to a dark color
        /// </summary>
        public void FadeOut()
        {
            StartCoroutine(DoFadeOut(fadeColor, fadeSpeed));
        }

        IEnumerator DoFadeOut(Color targetColor, float duration)
        {
            float t = 0, r = 1 / duration;
            Color cur = fadeColor;
            cur.a = 0;  

            while (t < 1)
            {

                t += Time.unscaledDeltaTime * r;
                img.color = Color.Lerp(cur, targetColor, t);
                yield return null;
            }

            img.color = targetColor;
            OnFadeOutComplete?.Invoke();
        }

        IEnumerator DoFadeIn(Color targetColor, float duration)
        {
            float t = 0, r = 1 / duration;
            Color cur = img.color;
            while (t < 1)
            {

                t += Time.unscaledDeltaTime * r;
                img.color = Color.Lerp(cur, targetColor, t);
                yield return null;
            }

            img.color = targetColor;
            OnFadeInComplete?.Invoke();
        }
        #endregion
    }
}
