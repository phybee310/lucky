using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Monster's movement speed
    public float changeDirectionTime = 2f; // Time before the monster changes direction
    private Vector2 moveDirection; // Current movement direction
    private float timeSinceDirectionChange = 0f; // Timer for changing direction
    public GameObject gameOverPanel; // Reference to the Game Over panel


    // Boundaries for the movement area
    public float minX = -22f;
    public float maxX = 22f;
    public float minY = -14f;
    public float maxY = 14f;

    [SerializeField] private GameOverManager gameOverManager; // Reference to GameOverManager

    void Start()
    {
        ChangeDirection(); // Start with a random direction
    }

    void Update()
    {
        MoveMonster();
        HandleDirectionChange();
    }

    void MoveMonster()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        if (transform.position.x != clampedX || transform.position.y != clampedY)
        {
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
            ChangeDirection(); // Change direction when hitting boundaries
        }
    }

    void HandleDirectionChange()
    {
        timeSinceDirectionChange += Time.deltaTime;

        if (timeSinceDirectionChange >= changeDirectionTime)
        {
            ChangeDirection();
            timeSinceDirectionChange = 0f;
        }
    }

    void ChangeDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player collided with the monster. Game Over!");
            if (gameOverManager != null)
            {
                gameOverManager.ShowGameOverPanel(); // Show the Game Over panel
            }
            else
            {
                Debug.LogError("GameOverManager reference is missing in MonsterMovement script!");
            }
        }
        else
        {
            ChangeDirection();
        }
    }
}
