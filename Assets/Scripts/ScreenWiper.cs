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
    public GameObject gameOverPanel;
    public GameObject pauseMenuPanel;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    // ----------------------------------------------- //

    public void SlideTo(string scene)
    {
        StartCoroutine(SlideOut(scene));
    }

    public void SlideToAlternate(string scene)
    {
        StartCoroutine(SlideOutAlternate(scene));
    }

    public void SlideToAlternatePause(string scene)
    {
        StartCoroutine(SlideOutAlternatePause(scene));
    }

    public void GameOver()
    {
        if (pauseMenuPanel.activeSelf)
        {
            StartCoroutine(WaitTime(1.5f));
        }

        StartCoroutine(FadeOut());
    }

    public void TogglePause()
    {
        if (!pauseMenuPanel)
            return;

        if (GameManager.gameEnded)
            return;

        if (!pauseMenuPanel.activeSelf)
        {
            StartCoroutine(PauseFadeOut());
        }
        else
        {
            StartCoroutine(PauseFadeIn());
        }
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
            float a = fadeCurve.Evaluate(t / fadeIn_time);
            panel.GetComponent<Image>().color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        gameOverPanel.SetActive(true);
    }

    // 

    IEnumerator PauseFadeOut()
    {
        while (inTransition)
            yield return new WaitForSeconds(0.1f);

        panel.GetComponent<RectTransform>().localPosition = new Vector3(0, saved_y, saved_z);

        float fadeIn_time = 0.3f;
        float t = 0f;
        while (t < fadeIn_time)
        {
            t += Time.deltaTime;
            float a = fadeCurve.Evaluate(t / fadeIn_time);
            panel.GetComponent<Image>().color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        pauseMenuPanel.SetActive(true);
        pauseMenuPanel.GetComponent<CanvasGroup>().alpha = 1.0f;
        Time.timeScale = 0.0f;
    }

    IEnumerator PauseFadeIn()
    {
        Time.timeScale = 1.0f;
        if (pauseMenuPanel != null)
        {
            float t_go = 0f;
            while (t_go < 0.3f)
            {
                t_go += Time.deltaTime;
                float x = fadeCurve.Evaluate(t_go / 0.3f);
                pauseMenuPanel.GetComponent<CanvasGroup>().alpha = 1.0f - x;
                yield return 0;
            }
            pauseMenuPanel.SetActive(false);
        }

        while (inTransition)
            yield return new WaitForSeconds(0.1f);

        StartCoroutine(SlideIn());
    }

    // ----------------------------------------------- //

    IEnumerator SlideOutAlternate(string scene)
    {
        if (gameOverPanel != null)
        {
            float t_go = 0f;
            while (t_go < 0.5f)
            {
                t_go += Time.deltaTime;
                float x = fadeCurve.Evaluate(t_go / 0.5f);
                gameOverPanel.GetComponent<CanvasGroup>().alpha = 1.0f - x;
                yield return 0;
            }
            gameOverPanel.SetActive(false);
        }

        while (inTransition)
            yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(scene);
    }

    IEnumerator SlideOutAlternatePause(string scene)
    {
        Time.timeScale = 1.0f;
        if (pauseMenuPanel != null)
        {
            float t_go = 0f;
            while (t_go < 0.5f)
            {
                t_go += Time.deltaTime;
                float x = fadeCurve.Evaluate(t_go / 0.5f);
                pauseMenuPanel.GetComponent<CanvasGroup>().alpha = 1.0f - x;
                yield return 0;
            }
            pauseMenuPanel.SetActive(false);
        }

        while (inTransition)
            yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(scene);
    }

    // ----------------------------------------------- //

    IEnumerator SlideIn()
    {
        inTransition = true;
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            float x = fadeCurve.Evaluate(t / time) * canvasWidth;
            panel.GetComponent<RectTransform>().localPosition = new Vector3(x, saved_y, saved_z);
            yield return 0;
        }
        inTransition = false;
    }

    IEnumerator SlideOut(string scene)
    {
        // Game Over Panel
        if (gameOverPanel != null)
        {
            float t_go = 0f;
            while (t_go < 0.5f)
            {
                t_go += Time.deltaTime;
                float x = fadeCurve.Evaluate(t_go / 0.5f);
                gameOverPanel.GetComponent<CanvasGroup>().alpha = 1.0f - x;
                yield return 0;
            }
            gameOverPanel.SetActive(false);
        }

        // Pause Menu Panel
        if (pauseMenuPanel != null)
        {
            float t_go = 0f;
            while (t_go < 0.5f)
            {
                t_go += Time.deltaTime;
                float x = fadeCurve.Evaluate(t_go / 0.5f);
                gameOverPanel.GetComponent<CanvasGroup>().alpha = 1.0f - x;
                yield return 0;
            }
            gameOverPanel.SetActive(false);
        }

        // ------------------------ //

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

    // ----------------------------------------------- //

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
