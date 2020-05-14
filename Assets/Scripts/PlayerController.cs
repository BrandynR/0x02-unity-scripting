using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    private Vector3 movement;
    public Canvas hud;
    private int score = 0;
    public int health = 5;
    private float startHealth;
    private Text scoreText;
    private Text healthText;
    private Gradient healthColor;
    public Color healthStart = Color.green;
    public Color healthEnd = Color.red;

    // Start is called before the first frame update
    void Start()
    {
       
        healthColor = new Gradient();
        
        var colorkey = new GradientColorKey[2];
        
        colorkey[0].color = healthStart;
        colorkey[0].time = 1f;
        colorkey[1].color = healthEnd;
        colorkey[1].time = 0f;
        
        healthColor.colorKeys = colorkey;



        healthText = hud.transform.Find("Health").GetComponent<UnityEngine.UI.Text>();
        scoreText = hud.transform.Find("Score").GetComponent<UnityEngine.UI.Text>();

        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + health;
        healthText.color = healthColor.Evaluate(1);

        startHealth = health;

    }

    private void Update()
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("maze");
        }
    }
    private void FixedUpdate()
    {
        movement.Set(0, 0, 0);

        if (UpdateDirection(ref movement))
            GetComponent<Rigidbody>().AddForce((movement * speed));

    }

    private static bool UpdateDirection(ref Vector3 dir)
    {
        if (!Input.anyKey) return false;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            dir.x += 1;
        }


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x -= 1;
        }


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            dir.z += 1;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            dir.z -= 1;
        }

        return true;

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Pickup"))
        {
            score += 1;
            scoreText.text = "Score: " + score;
            Debug.Log("Score: " + score);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Trap"))
        {
            health -= 1;
            healthText.text = "Health: " + health;
            healthText.color = healthColor.Evaluate(health / startHealth);
            Debug.Log("Health: " + health);
        }

        if (other.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }
    }
}
