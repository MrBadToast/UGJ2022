using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Player input 속성은 Scene 내부에 단 하나만 있어야 한다?
    public static bool GameIsPaused = false;
    public GameObject pauseMenuCanvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    // 돌아가기 버튼 
    public void ResumeBtn()
    {
        Resume();
    }
    // 타이틀로 가기 버튼 클릭 
    public void ToTitle()
    {
        Debug.Log("to title");
        //Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu");
    }
}
