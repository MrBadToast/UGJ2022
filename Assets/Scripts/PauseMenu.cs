using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    // Player input 속성은 Scene 내부에 단 하나만 있어야 한다?
    public static bool GameIsPaused = false;
    public GameObject pauseMenuCanvas;
    public Button returnGame, exitGame;

    void Start()
    {
        // 유니티 에디터 창에서 직접 연결시켜주어야 함
        returnGame.onClick.AddListener(ResumeBtn);
        exitGame.onClick.AddListener(ToTitle);
        // pauseMenuCanvas도 직접 추가해주어야 함 
    }
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
        Debug.Log("Resumed");
    }

    public void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("Paused");
    }
    // 돌아가기 버튼 
    public void ResumeBtn()
    {
        Debug.Log("Resume btn");
        Resume();
    }
    // 타이틀로 가기 버튼 클릭 
    public void ToTitle()
    {
        Debug.Log("ToTitle btn");
        //Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu");
    }
}
