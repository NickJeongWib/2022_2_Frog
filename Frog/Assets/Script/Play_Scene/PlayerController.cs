using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    Jump, HighJump, Run, Stand, Death
}

public class PlayerController : MonoBehaviour
{
    public PlayerState Playerstate;

    public GameObject m_GameOver_Panel;

    public float High_JumpPower = 1000.0f;
    public float JumpPower = 500.0f;

    public AudioClip[] Sound;

    public Button JumpBtn;
    public Button HighJumpBtn;

    public Animator Anima;

    public GameMgr Gamemgr;

    

    // Start is called before the first frame update
    void Start()
    {
        Playerstate = PlayerState.Run;

        Gamemgr = GetComponent<GameMgr>();

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpMethod()
    {
       
        if (Playerstate != PlayerState.HighJump && Playerstate != PlayerState.Jump)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, JumpPower, 0));

            Playerstate = PlayerState.Jump;
        }

        Anima.SetTrigger("Jump");
        Anima.SetBool("Ground", false);
  
    }

    public void HighJumpMethod()
    {

        if (Playerstate != PlayerState.HighJump && Playerstate != PlayerState.Jump)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, High_JumpPower, 0));

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
        Anima.SetTrigger("Frog_Run");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathZone" && Playerstate != PlayerState.Death)
        {
            GameOver();
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

        GameMgr.inst.GameOverScore();

        m_GameOver_Panel.SetActive(true);

        Time.timeScale = 0.0f;

    }
}
