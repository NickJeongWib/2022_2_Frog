using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.PackageManager;
using UnityEditor.UIElements;

// 22_11_08 전부 수정
// 22_11_09 UI 수정
// 22_11_22 캐릭터 멈춤 시 점수 배경화면 멈춤, 블럭생성 재구현
// 22_12_01 시간 초과시 죽음, 코인 구현, 게임준비 판넬 구현, 유저코인정보 저장

public enum Gamesta
{
    Ready,Play,Pause,GameOver
}

public class GameMgr1 : MonoBehaviour
{
    public GameObject Pause_Panel;
    
    public static GameMgr1 inst;
    PlayerController PS;

    public Gamesta Gamestate = Gamesta.Play;

    public float CurScore;       // 현재 스코어
    public float Speed;          // 점수 올라가는 스피드

    [Header ("-----버튼-----")]
    public Button m_Pause_Btn;   //정지창 OR 시스템창
    public Button m_ReStart_Btn; //다시하기
    public Button m_GoFirst_Btn; //처음화면
    public Button m_Close_Btn;   //정지창 닫음

    [Header("-----static-----")]
    public static float g_BestScore = 0; // 최고점수 변수
    public static int g_UserGold = 0;    // 유저골드 유지 변수
    int Coin = 0;
    

    [Header("----Text----")]
    public Text UserGold_Txt;      // 게임 플레이 획득 골드
    public Text CurScore_Txt;      // 현재 점수 표시
    public Text BestScore_Txt;     // 최고점수 표시
    public Text GameOverGold_Txt;  // 게임오버판넬 골드 표시
    public Text FinalScore_Txt;    // 게임오버판넬 점수 표시

    [Header("----Player----")]
    public GameObject Player;      // 플레이어 찾기 

    [Header("----ReadyPanel----")]
    public GameObject ReadyPanel;  // 준비시작 시 카운트 판넬
    public Text CountNum_Text;     // 카운트 다운 표시 텍스트
    float CountDown = 4.0f;        // 카운트다운 넘버


    void Awake()
    {
        inst = this;    //싱글턴 문법인데 그대로 쓰셈 보다가 inst 나오는데 있으면 그냥 그대로 쓰면됨
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadGameData();                        // 유저정보 불러옴
        Gamestate = Gamesta.Ready;             // 시작시 레디 상태
        PS = GetComponent<PlayerController>(); // 상태 PlayerController 불러옴
    }

    // Update is called once per frame
    void Update()
    {
        ReadyCount(); // 메서드 가져옴

        if (Gamestate == Gamesta.Play)
        {

            if (Player.transform.position.y != -2.0f) // 플레이어 y값 -2가 아니면 점수 올라감 (움직이는 판정)
            {
                Speed = 1.0f;
                CurScore += Time.deltaTime * Speed;

            }
            else if (Player.transform.position.y == -2.0f) // 플레이어 y값 -2면 점수 안 올라감 (멈춤 상태)
            {
                Speed = 0.0f;
                CurScore += Time.deltaTime * Speed;
            }
            
            CurScore_Txt.text = CurScore.ToString("N1") + " m"; // 현재 점수 표시
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
        SceneManager.LoadScene("Play_made");
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

    public static void LoadGameData() // 유저 데이터 저장
    {
        g_BestScore = PlayerPrefs.GetFloat("BestScore", 0);
        g_UserGold = PlayerPrefs.GetInt("UserGold", 0);
    }

    public void GameOverScore()
    {
        Gamestate = Gamesta.GameOver; // 게임오버상태

        if(CurScore > g_BestScore) // 현재점수가 저장된 최고점수보다 높으면 밑에 실행
        {
            g_BestScore = CurScore;

            BestScore_Txt.text = "최고 점수\n" + g_BestScore.ToString("N1") + " m";
            PlayerPrefs.SetFloat("BestScore", g_BestScore);
        }

        FinalScore_Txt.text = "현재 점수 : " + CurScore.ToString("N1") + " m";
        BestScore_Txt.text = "최고 점수 : " + g_BestScore.ToString("N1") + " m";
        GameOverGold_Txt.text = "획득 골드 : " + Coin + " G";

        g_UserGold += Coin;
        PlayerPrefs.SetInt("UserGold", g_UserGold);
    }

    void ReadyCount() // 시작할 때 카운트 다운
    {
        if (Gamestate == Gamesta.Ready)
        {
            CountDown -= Time.deltaTime;

            if (CountDown >= 0)
            {
                CountNum_Text.text = ((int)CountDown).ToString();
            }
            else if (CountDown >= -1.0f)
            {
                CountNum_Text.text = "Start!";
                
            }
            else if (CountDown < -1.0f)
            {
                Gamestate = Gamesta.Play;
                ReadyPanel.SetActive(false);
            }
        }
    }

    public void GetCoin() // 코인
    {
        Coin++;

        UserGold_Txt.text = Coin.ToString() + " G";

    }
}
