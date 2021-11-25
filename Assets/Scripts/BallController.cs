using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public int force;
    Rigidbody2D rigid;
    Vector2 direction;
    GameObject finishPanel;
    AudioSource audioSource;
    public AudioClip hitSound;

    int scorePlayer1;
    int scorePlayer2;

    Text scoreUIPlayer1;
    Text scoreUIPlayer2;
    Text winnerText;
    public static bool isScoring = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        finishPanel = GameObject.Find("FinishPanel");
        finishPanel.SetActive(false);
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        scoreUIPlayer1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreUIPlayer2 = GameObject.Find("Score2").GetComponent<Text>();
        rigid = GetComponent<Rigidbody2D>();
        direction = new Vector2(Random.Range(1f, -1f), 0).normalized;
        rigid.AddForce(direction * force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0);
        rigid.velocity = new Vector2(0, 0);
    }

    void DisplayScore()
    {
        Debug.Log("Score P1: " + scorePlayer1 + "Score P2: " + scorePlayer2);
        scoreUIPlayer1.text = scorePlayer1 + "";
        scoreUIPlayer2.text = scorePlayer2 + "";
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(hitSound);
        if(collision.gameObject.name == "RightWall")
        {
            scorePlayer1 += 1;
            DisplayScore();
            ResetBall();
            isScoring = true;

            if (scorePlayer1 == 5)
            {
                finishPanel.SetActive(true);
                winnerText = GameObject.Find("Winner").GetComponent<Text>();
                winnerText.text = "Blue wins!";
                Destroy(gameObject);
                return;
            }    
            direction = new Vector2(-1, 0).normalized;
            rigid.AddForce(direction * force);
        } else if (collision.gameObject.name == "LeftWall")
        {
            scorePlayer2 += 1;
            DisplayScore();
            ResetBall();
            isScoring = true;

            if (scorePlayer2 == 5)
            {
                finishPanel.SetActive(true);
                winnerText = GameObject.Find("Winner").GetComponent<Text>();
                winnerText.text = "Red wins!";
                Destroy(gameObject);
                return;
            }
            direction = new Vector2(1, 0).normalized;
            rigid.AddForce(direction * force);
        }

        if(collision.gameObject.name == "Paddle" || collision.gameObject.name == "Paddle2")
        {
            float angle = (transform.position.y - collision.transform.position.y) * 5f;
            Vector2 direction = new Vector2(rigid.velocity.x, angle).normalized;
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(direction * force * 2);
        }
    }
    
}
