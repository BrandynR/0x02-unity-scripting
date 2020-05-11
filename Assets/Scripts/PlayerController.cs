using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float speed;
    private int score = 0;
    public int health = 5;

    private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}


	private void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		rb.AddForce(movement * speed);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            score++;
            Debug.Log("Score: " + score.ToString());
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            health--;
            Debug.Log("Health: " + health.ToString());
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }
    }

    private void Update()
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene(0);
        }
    }
}
