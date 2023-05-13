using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeOver : MonoBehaviour
{
    [Header("----Time----")]
    [SerializeField]public Image TimeBar;
    float MaxTime = 1.0f;
    float DownTime;

    public GameObject Deathzone;
    public GameObject Player;

    GameMgr1 GameMgr;

    // Start is called before the first frame update
    void Start()
    {
        TimeBar = GetComponent<Image>();

        DownTime = MaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        TimeOut();

    }

    public void TimeOut()
    {
        if (Player.transform.position.y == -2.0f)
        {
            if (DownTime > 0 && GameMgr1.inst.Gamestate == Gamesta.Play)
            {
                DownTime -= Time.deltaTime;
                TimeBar.fillAmount = DownTime / MaxTime;
            }
            else if(DownTime <= 0)
            {
                DownTime = 0;
                Deathzone.transform.position = new Vector3(Player.transform.position.x + 6.6F, Player.transform.position.y + 2.0f, Player.transform.position.z);
                GameMgr1.inst.Gamestate = Gamesta.GameOver;
            }
        }
        else
            DownTime = 1.0f;

    }

}
