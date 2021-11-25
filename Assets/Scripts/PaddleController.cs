using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed;
    public string axis;
    public float topCollide;
    public float bottomCollide;

    GameObject paddle1, paddle2;

    void Start()
    {
        paddle1 = GameObject.Find("Paddle");
        paddle2 = GameObject.Find("Paddle2");
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis(axis) * speed * Time.deltaTime;
        float nextY = transform.position.y + move;
        if (nextY > topCollide || nextY < bottomCollide)
        {
            move = 0;
        }

        transform.Translate(0, move, 0);

        if (BallController.isScoring == true)
        {
            ResetPaddle();
            BallController.isScoring = false;
        }
    }

    public void ResetPaddle()
    {
        paddle1.transform.position = new Vector2(paddle1.transform.position.x, 0);
        paddle2.transform.position = new Vector2(paddle2.transform.position.x, 0);
    }
}
