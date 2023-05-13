using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerSta
{
    Jump, HighJump, Run, Stand, Death
}

public class PlayerController1 : MonoBehaviour
{
    public PlayerState Playerstate;

    public GameObject m_GameOver_Panel;
    public GameObject DeathZone_A;
    public GameObject DeathZone_B;
    

    public float High_JumpPower = 1000.0f;
    public float JumpPower = 500.0f;
    public float ForWardPower = 100.0f;

    public AudioClip[] Sound;

    public Button JumpBtn;
    public Button HighJumpBtn;

    public Animator Anima;

    public GameMgr1 Gamemgr;

    public Block_Gen1 Cube_A;

    // Start is called before the first frame update
    void Start()
    {
        Cube_A = GetComponent<Block_Gen1>();

        Playerstate = PlayerState.Run;

        Gamemgr = GetComponent<GameMgr1>();

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DeathZone_A.transform.position = new Vector3(transform.position.x + 10.0f, -15.0f, 0.0f);
        DeathZone_B.transform.position = new Vector3(transform.position.x - 10.0f, -10.0f, 0.0f);
    }

    public void JumpMethod()
    {
       
        if (Playerstate != PlayerState.HighJump && Playerstate != PlayerState.Jump)
        {
            // GetComponent<Rigidbody>().AddForce(new Vector3(0, JumpPower, 0));
            GetComponent<Rigidbody>().AddForce(new Vector3(ForWardPower * 1.2f, JumpPower, 0));
           

            Playerstate = PlayerState.Jump;
        }

        Anima.SetTrigger("Jump");
        Anima.SetBool("Ground", false);
  
    }

    public void HighJumpMethod()
    {

        if (Playerstate != PlayerState.HighJump && Playerstate != PlayerState.Jump)
        {
            //GetComponent<Rigidbody>().AddForce(new Vector3(0, High_JumpPower, 0));
            GetComponent<Rigidbody>().AddForce(new Vector3(ForWardPower * 1.5f, High_JumpPower, 0)) ;

            Playerstate = PlayerState.HighJump;
        }

        Anima.SetTrigger("Jump");
        Anima.SetBool("Ground", false);
    }
    
    void Death()
    {
        Playerstate = PlayerState.Death;
    }

    void Run()
    {
        Playerstate = PlayerState.Run;
        Anima.SetBool("Ground", true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathZone" && Playerstate != PlayerState.Death)
        {
            GameOver();
        }
        else if(other.gameObject.name == "Coin")
        {
            GameMgr1.inst.GetCoin();

            Destroy(other.gameObject);
        }
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Playerstate != PlayerState.Run && Playerstate != PlayerState.Death)
            Run();
        
    } //  void OnCollisionEnter(Collision collision)


    public void GameOver()
    {
        Playerstate = PlayerState.Death;

        GameMgr1.inst.GameOverScore();

        m_GameOver_Panel.SetActive(true);

        Time.timeScale = 0.0f;
    }
}
