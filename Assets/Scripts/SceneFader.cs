using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour {

    [SerializeField]
    private Image panelImage;

    [SerializeField]
    private AnimationCurve m_curve;

    public void FadeTo(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut(string sceneName)
    {
        Debug.Log(sceneName);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = m_curve.Evaluate(t);
            panelImage.color = new Color(0f, 0f, 0f, a);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = m_curve.Evaluate(t);
            panelImage.color = new Color(0f, 0f, 0f, a);
            yield return null;
        }
        
    }
}
