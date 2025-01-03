using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player's movement
    private bool isMoving;
    private Animator animator;
    public LayerMask SolidObjectsLayer;

    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;

    // Fixed movement boundaries
    private readonly Vector2 minBounds = new Vector2(-22, -14);
    private readonly Vector2 maxBounds = new Vector2(22, 14);

    private bool isPressingButton = false;
    private Vector2 currentDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        AddButtonListener(upButton, Vector2.up);
        AddButtonListener(downButton, Vector2.down);
        AddButtonListener(leftButton, Vector2.left);
        AddButtonListener(rightButton, Vector2.right);
    }

    private void Update()
    {
        if (isPressingButton)
        {
            TryMove(currentDirection);
        }

        animator.SetBool("isMoving", isMoving);
    }

    private void AddButtonListener(Button button, Vector2 direction)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // OnPointerDown event for continuous movement
        var pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDown.callback.AddListener((_) => OnButtonPress(direction));
        trigger.triggers.Add(pointerDown);

        // OnPointerUp event to stop movement
        var pointerUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUp.callback.AddListener((_) => OnButtonRelease());
        trigger.triggers.Add(pointerUp);
    }

    private void OnButtonPress(Vector2 direction)
    {
        isPressingButton = true;
        currentDirection = direction;
    }

    private void OnButtonRelease()
    {
        isPressingButton = false;
    }

    private void TryMove(Vector2 direction)
    {
        if (!isMoving)
        {
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);

            var targetPos = transform.position + (Vector3)direction;

            // Check if the player can move to the target position
            if (isWithinBounds(targetPos))
            {
                if (isWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
                else
                {
                    HandleCollision(direction);
                }
            }
        }
    }

    private bool isWalkable(Vector3 targetPos)
    {
        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();
        if (capsuleCollider == null)
        {
            Debug.LogWarning("CapsuleCollider2D not found on player.");
            return false;
        }

        Vector2 size = capsuleCollider.size; // Width and height of the capsule
        CapsuleDirection2D direction = capsuleCollider.direction; // Horizontal or Vertical

        // Check for overlap with the capsule shape
        return Physics2D.OverlapCapsule(targetPos, size, direction, 0f, SolidObjectsLayer) == null;
    }


    private bool isWithinBounds(Vector3 targetPos)
    {
        return targetPos.x >= minBounds.x && targetPos.x <= maxBounds.x &&
               targetPos.y >= minBounds.y && targetPos.y <= maxBounds.y;
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    private void HandleCollision(Vector2 direction)
    {
        // Adjust movement to slide along the obstacle
        Vector2 adjustedDirection = Vector2.zero;

        if (direction.x != 0) // Horizontal movement
        {
            adjustedDirection = new Vector2(0, direction.y);
        }
        else if (direction.y != 0) // Vertical movement
        {
            adjustedDirection = new Vector2(direction.x, 0);
        }

        var adjustedTargetPos = transform.position + (Vector3)adjustedDirection;

        if (isWalkable(adjustedTargetPos))
        {
            StartCoroutine(Move(adjustedTargetPos));
        }
    }
}