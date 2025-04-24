using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasFade : MonoBehaviour
{
    [SerializeField] CanvasGroup Fade;

    // Update is called once per frame
    void Update()
    {
        Invoke("FadeOut", 5f);
    }

    void FadeOut(){
        Fade.alpha -= Time.deltaTime;
    }
}
