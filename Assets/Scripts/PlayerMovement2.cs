using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 5f;
     private float powerUpTimer = 0f;
    private float powerUpDuration = 5f;
    private bool isPowerUpActive = false;
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

        if(isPowerUpActive) {
            powerUpTimer -= Time.deltaTime;
            if(powerUpTimer <= 0f) {
                speed = 5f;
                isPowerUpActive = false;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion")) {  
            gameOverCanvas.SetActive(true);
            DeathSequence();
        }
        if(other.CompareTag("SpeedBoost")) {
            Destroy(other.gameObject);
            ActivatePower();
        }
    }

    private void DeathSequence() {
        enabled = false;
        Invoke("ReloadSceneAfterDelay", 3f);
    }

    private void ActivatePower() {
        powerUpTimer = powerUpDuration;
        isPowerUpActive = true;
        speed = 10f;
    } 

    private void ReloadSceneAfterDelay() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}