using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Title_Mgr : MonoBehaviour
{
    public Button m_Start_Btn;
    public Button m_Store_Btn;
    public Button m_Close_Btn;
    public Button m_Ranking_Btn;
    public Button m_Cfg_Btn;
    public Text MyCoin_Text;

    public GameObject CfgBack;
    public GameObject Ranking_Scroll;

    public bool IsCfg = false;
    public bool IsRanking = false;
    // Start is called before the first frame update
    void Start()
    {
        // 골드 저장 불러오기
        
        
    }

    // Update is called once per frame
    void Update()
    {
        GameMgr1.LoadGameData();
        MyCoin_Text.text = GameMgr1.g_UserGold + " G";
    }

    public void ClickStartBtn() // 스타트 버튼 누름
    {
        SceneManager.LoadScene("Play_made");
    }

    public void ClickCloseBtn() // 닫기 버튼 누름
    {
        if(IsCfg == true)
            CfgBack.SetActive(false);

        if(IsRanking == true)
            Ranking_Scroll.SetActive(false);
    }

    public void ClickStoreBtn() // 상점 버튼 누름
    {
        SceneManager.LoadScene("Store");
    }

    public void ClickRankingBtn() // 랭킹 버튼 누름
    {
        IsRanking = true;
        Ranking_Scroll.SetActive(true);
    }
    public void ClickCfgBtn() // 환경설정 버튼 누름
    {
        CfgBack.SetActive(true);
        IsCfg = true;
    }

    public void ClickResetBtn()
    {
        PlayerPrefs.DeleteAll(); 
    }

}
