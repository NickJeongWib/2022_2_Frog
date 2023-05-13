using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.PackageManager;

// 22_11_08 전부 수정
// 22_11_09 UI 수정

public enum GameState
{
    Play,Pause, GameOver
}

public class GameMgr : MonoBehaviour
{
    public GameObject Pause_Panel;
    
    public static GameMgr inst;
    PlayerController PS;

    public GameState Gamestate = GameState.Play;

    public float CurScore;
    public float Speed;

    [Header ("-----버튼-----")]
    public Button m_Pause_Btn;   //정지창 OR 시스템창
    public Button m_ReStart_Btn; //다시하기
    public Button m_GoFirst_Btn; //처음화면
    public Button m_Close_Btn;   //정지창 닫음

    [Header("-----static-----")]
    public static float g_BestScore = 0;
    public static int g_UserGold = 0;

    [Header("----Text----")]
    public Text UserGold_Txt;
    public Text CurScore_Txt;
    public Text BestScore_Txt;
    public Text GameOverGold_Txt;
    public Text FinalScore_Txt;


    void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadGameData();
        PS = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamestate == GameState.Play)
        {
            CurScore += Time.deltaTime * Speed;
            CurScore_Txt.text = CurScore.ToString("N1") + " Km";
        }
    }

    public void ClickPause()
    {
        Time.timeScale = 0.0f;
        Pause_Panel.SetActive(true);
    }

    public void ClickReStart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Play");
    }

    public void ClickFirst()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
    }

    public void ClickClose()
    {
        Pause_Panel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public static void LoadGameData()
    {
        g_BestScore = PlayerPrefs.GetFloat("BestScore", 0);
        g_UserGold = PlayerPrefs.GetInt("UserGold", 0);
    }

    public void GameOverScore()
    {
        Gamestate = GameState.GameOver;

        if(CurScore > g_BestScore)
        {
           
            g_BestScore = CurScore;

            BestScore_Txt.text = "최고 점수\n" + g_BestScore.ToString("N1") + " Km";
            PlayerPrefs.SetFloat("BestScore", g_BestScore);
        }

        FinalScore_Txt.text = "현재 점수\n" + CurScore.ToString("N1") + " Km";
        BestScore_Txt.text = "최고 점수\n" + g_BestScore.ToString("N1") + " Km";
    }

}
