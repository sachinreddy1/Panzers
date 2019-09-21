using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ScreenWiper : MonoBehaviour
{
    public GameObject panel;
    public Canvas canvas;
    public AnimationCurve fadeCurve;
    public GameObject gameOverPanel;
    //
    private float saved_y;
    private float saved_z;
    //
    private float canvasWidth;
    private float canvasHeight;
    //
    public float time = 1.5f;
    //
    private bool inTransition;
    //

    
    void Awake()
    {
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;

        saved_y = panel.GetComponent<RectTransform>().localPosition.y;
        saved_z = panel.GetComponent<RectTransform>().localPosition.z;
    }

    void Start()
    {
        canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        StartCoroutine(SlideIn());
    }

    // ----------------------------------------------- //

    public void SlideTo(string scene)
    {
        StartCoroutine(SlideOut(scene));
    }

    public void GameOver()
    {
        StartCoroutine(FadeOut());
    }

    // ----------------------------------------------- //

    IEnumerator FadeOut()
    {
        while (inTransition)
            yield return new WaitForSeconds(0.1f);

        panel.GetComponent<RectTransform>().localPosition = new Vector3(0, saved_y, saved_z);

        float fadeIn_time = 0.5f;
        float t = 0f;
        while (t < fadeIn_time)
        {
            t += Time.deltaTime;
            float a = fadeCurve.Evaluate(t/fadeIn_time);
            panel.GetComponent<Image>().color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        gameOverPanel.SetActive(true);
    }

    // ----------------------------------------------- //

    IEnumerator SlideIn()
    {
        inTransition = true;
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            float x = fadeCurve.Evaluate(t/time) * canvasWidth;
            panel.GetComponent<RectTransform>().localPosition = new Vector3(x, saved_y, saved_z);
            yield return 0;
        }
        inTransition = false;
    }

    IEnumerator SlideOut(string scene)
    {
        while (inTransition)
            yield return new WaitForSeconds(0.1f);

        panel.GetComponent<RectTransform>().localPosition = new Vector3(-canvasWidth, saved_y, saved_z);
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            float x = fadeCurve.Evaluate(t / time) * canvasWidth;
            panel.GetComponent<RectTransform>().localPosition = new Vector3(x - canvasWidth, saved_y, saved_z);
            yield return 0;
        }
        SceneManager.LoadScene(scene);
    }
}
