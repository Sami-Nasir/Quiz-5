using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody2D rb;
    private GameObject gb;
    private int Life;
    //public Vector3 look;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gb = GameObject.Find("Player");
        Life = gb.GetComponent<PlayerMovement>().life;
        //Life = GameObject.Find("player").GetComponent<PlayerMovement>().life;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce((gb.transform.position - transform.position).normalized * speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            Life--;
        }
    }
}
