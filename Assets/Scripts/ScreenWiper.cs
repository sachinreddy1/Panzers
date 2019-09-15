using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ScreenWiper : MonoBehaviour
{
    public GameObject SceneWiper;
    public AnimationCurve fadeCurve;
    //
    private float saved_y;
    private float saved_z;
    private float saved_width;
    //
    public float time = 2f;

    void Start()
    {
        saved_y = SceneWiper.GetComponent<RectTransform>().localPosition.y;
        saved_z = SceneWiper.GetComponent<RectTransform>().localPosition.z;
        saved_width = SceneWiper.GetComponent<RectTransform>().rect.width;
        //
        float new_width = Screen.width + 500;
        float new_height = Screen.height + 50;
        SceneWiper.GetComponent<RectTransform>().sizeDelta = new Vector2(new_width, new_height);

        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            float x = fadeCurve.Evaluate(t/time) * saved_width;
            SceneWiper.GetComponent<RectTransform>().localPosition = new Vector3(x, saved_y, saved_z);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        SceneWiper.GetComponent<RectTransform>().localPosition = new Vector3(-saved_width, saved_y, saved_z);
        //
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            float x = fadeCurve.Evaluate(t / time) * saved_width;
            SceneWiper.GetComponent<RectTransform>().localPosition = new Vector3(x - saved_width, saved_y, saved_z);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }

}
