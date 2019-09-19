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
    private bool fadeIn;

    void Awake()
    {
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;

        saved_y = panel.GetComponent<RectTransform>().localPosition.y;
        saved_z = panel.GetComponent<RectTransform>().localPosition.z;
    }

    void Start()
    {
        canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        StartCoroutine(FadeIn());
    }

    // ----------------------------------------------- //

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    public void GameOver()
    {
        StartCoroutine(EndGame());
    }

    // ----------------------------------------------- //

    IEnumerator EndGame()
    {
        while (fadeIn)
            yield return new WaitForSeconds(0.1f);

        gameOverPanel.SetActive(true);
        panel.GetComponent<RectTransform>().localPosition = new Vector3(-canvasWidth, saved_y, saved_z);
        gameOverPanel.GetComponent<RectTransform>().localPosition = new Vector3(-canvasWidth, saved_y, saved_z);
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            float x = fadeCurve.Evaluate(t / time) * canvasWidth;
            panel.GetComponent<RectTransform>().localPosition = new Vector3(x - canvasWidth, saved_y, saved_z);
            gameOverPanel.GetComponent<RectTransform>().localPosition = new Vector3(x - canvasWidth, saved_y, saved_z);
            yield return 0;
        }
    }

    // ----------------------------------------------- //

    IEnumerator FadeIn()
    {
        fadeIn = true;
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            float x = fadeCurve.Evaluate(t/time) * canvasWidth;
            panel.GetComponent<RectTransform>().localPosition = new Vector3(x, saved_y, saved_z);
            yield return 0;
        }
        fadeIn = false;
    }

    IEnumerator FadeOut(string scene)
    {
        while (fadeIn)
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
