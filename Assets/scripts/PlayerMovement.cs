using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public TextMeshProUGUI lives;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI gameover;
    private Vector2 pos;
    public GameObject tiles;
    private bool moving = false;
    public GameObject Child;
    private Vector2 origin, direction1, direction2, direction3, direction4;
    Ray2D newRay1, newRay2, newRay3, newRay4;
    RaycastHit2D hit;
    public int life = 3;
    public float Distance;
    public AudioSource audio;
    private float count = 60f;
    // Use this for initialization
    void Start()
    {
        gameover.gameObject.SetActive(false);
        pos = transform.position;
        audio = GetComponent<AudioSource>();
        audio.Play();
    }
    // Update is called once per frame
    void Update()
    {
        CallLife();
        SetTimer();
        PositionSetup();
        DrawRayCast();
        CheckInput();
        if (moving)
        {
            transform.position = pos;
            moving = false;
        }
        InstantiateTiles();
    }

    void CheckInput()
    {
        if (Physics2D.Raycast(origin, direction4, Distance))
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                    pos += Vector2.left;
                    moving = true;
            }
   
        }
        if (Physics2D.Raycast(origin, direction3, Distance))
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                    pos += Vector2.right;
                    moving = true;
            }
        }
        if (Physics2D.Raycast(origin, direction1, Distance))
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                    pos += Vector2.up;
                    moving = true;
            }
        }
        if (Physics2D.Raycast(origin, direction2, Distance))
        {
           if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
           { 
                    pos += Vector2.down;
                    moving = true;
           }
        }
    }
    void DrawRayCast()
    {
        Vector2 forward = transform.TransformDirection(Vector2.up) * 5;
        Vector2 backward = transform.TransformDirection(Vector2.down) * 5;
        Vector2 left = transform.TransformDirection(Vector2.left) * 5;
        Vector2 right = transform.TransformDirection(Vector2.right) * 5;

        Debug.DrawRay(origin, forward, Color.green);
        Debug.DrawRay(origin, backward, Color.blue);
        Debug.DrawRay(origin, left, Color.red);
        Debug.DrawRay(origin, right, Color.yellow);
    }
    void PositionSetup()
    {
        origin = Child.transform.position;
        direction1 = Vector2.up;
        direction2 = Vector2.down;
        direction3 = Vector2.left;
        direction4 = Vector2.right;
        newRay1 = new Ray2D(origin, direction1);
        newRay2 = new Ray2D(origin, direction2);
        newRay3 = new Ray2D(origin, direction3);
        newRay4 = new Ray2D(origin, direction4);
    }
    void InstantiateTiles()
    { 
        Instantiate(tiles, Child.transform.position, tiles.transform.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            life--;
            Debug.Log(life);
            if (life == 0)
            {
                gameover.gameObject.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
    void SetTimer()
    {
        count -= Time.deltaTime;
        timer.SetText("" + count);
        if (count < 1)
        {

            timer.SetText("TimeUP!");
            gameover.gameObject.SetActive(true);
        }
    }
    void CallLife()
    {
        lives.SetText("Life: "+ life);
    }
}
