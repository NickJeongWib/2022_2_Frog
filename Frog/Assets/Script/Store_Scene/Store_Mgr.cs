using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Store_Mgr : MonoBehaviour
{

    public Text Gold_Text;

    // Start is called before the first frame update
    void Start()
    {
        // 골드 저장 불러오기
        GameMgr1.LoadGameData();
        Gold_Text.text = GameMgr1.g_UserGold + " G";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickCloseBtn()
    {
        SceneManager.LoadScene("Title");
    }
}
