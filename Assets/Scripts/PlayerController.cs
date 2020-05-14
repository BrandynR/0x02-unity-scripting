using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    private int score = 0;
    public int health = 5;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            rb.AddForce(0, 0, speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            rb.AddForce(0, 0, -speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow))
            rb.AddForce(-speed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow))
            rb.AddForce(speed * Time.deltaTime, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        Destroy(other.gameObject);
        score += 1;
        Debug.Log("Score: " + score);

        if (other.CompareTag("Trap"))
        health -= 1;
        Debug.Log("Health: " + health);

        if (other.CompareTag("Goal"))
        Debug.Log("You win!");
    }
     void Update()
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("maze");
        }
    }
}
