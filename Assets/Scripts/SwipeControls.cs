using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    private AudioSource jumpSound;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    private int desiredLane = 1; // 0: Left, 1: Middle, 2: Right
    private float laneDistance = 1.5f; // The distance between two lanes

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    [SerializeField]
    private float jumpForce = 3f;

    [SerializeField]
    private float transitionSpeed = 10f;

    public enum SwipeDirection { None, Up, Down, Left, Right }

    public delegate void SwipeAction(SwipeDirection direction);
    public static event SwipeAction OnSwipe;

    private Vector3 targetPosition;

    private Rigidbody rb;

    private void Start()
    {
        jumpSound = GetComponent<AudioSource>();
        targetPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetTouchInput();
        Vector3 newPosition = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), Time.deltaTime * transitionSpeed);
        transform.position = newPosition;
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            Vector2 direction = fingerUpPosition - fingerDownPosition;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                // Horizontal swipe
                if (direction.x > 0)
                {
                    OnSwipe?.Invoke(SwipeDirection.Right);
                    desiredLane++;
                    if (desiredLane == 3)
                    {
                        desiredLane = 2;
                    }
                }
                else
                {
                    OnSwipe?.Invoke(SwipeDirection.Left);
                    desiredLane--;
                    if (desiredLane == -1)
                    {
                        desiredLane = 0;
                    }
                }
            }
            else
            {
                // Vertical swipe
                if (direction.y > 0)
                {
                    Jump();
                    jumpSound.Play();
                    OnSwipe?.Invoke(SwipeDirection.Up);
                }
                //else                                                         !! if you want to add a swipe down feature !!
                //{
                //    GetComponentInChildren<Animator>().SetTrigger("roll");
                //    OnSwipe?.Invoke(SwipeDirection.Down);
                //}
            }

            targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

            if (desiredLane == 0)
            {
                targetPosition += Vector3.left * laneDistance;
            }
            else if (desiredLane == 2)
            {
                targetPosition += Vector3.right * laneDistance;
            }
        }
    }

    private void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
            }

            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerUpPosition = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUpPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    private void Jump()
    {
        if (player && Mathf.Abs(rb.linearVelocity.y) < 0.001f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            GetComponentInChildren<Animator>().SetTrigger("jump");
        }
    }

    private bool SwipeDistanceCheckMet()
    {
        return Vector3.Distance(fingerDownPosition, fingerUpPosition) > minDistanceForSwipe;
    }
}
