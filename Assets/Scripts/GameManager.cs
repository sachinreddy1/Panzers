using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static bool gameEnded;
    //public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
            return;

        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }
    }

    public static void EndGame()
    {
        gameEnded = true;
        Debug.Log("GAME OVER.");
    }
}
