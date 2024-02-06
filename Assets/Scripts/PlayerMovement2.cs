using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public GameObject gameOverCanvas; // Reference to your Game Over Canvas GameObject

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 movement = new Vector2(Input.GetKey(KeyCode.RightArrow) ? 1 : (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0), Input.GetKey(KeyCode.UpArrow) ? 1 : (Input.GetKey(KeyCode.DownArrow) ? -1 : 0)).normalized;   
        rb.velocity = movement * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion")) {  
            gameOverCanvas.SetActive(true);
            DeathSequence();
        }
    }

    private void DeathSequence() {
        enabled = false;
        Invoke("ReloadSceneAfterDelay", 3f);
    }

    private void ReloadSceneAfterDelay() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}