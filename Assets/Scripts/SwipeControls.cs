using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    public bool isPlaying = true;
    private bool isGrounded = true;

    [SerializeField]
    private AudioSource switchSideSound;

    [SerializeField]
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
    private Collider playerCollider;

    private void Start()
    {
        targetPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        //IsPlaying();
        GetTouchInput();
        Vector3 newPosition = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), Time.deltaTime * transitionSpeed);
        transform.position = newPosition;
        CheckGroundStatus();
    }

    private void DetectSwipe()
    {
        if (isPlaying)
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
                        switchSideSound.Play();
                        desiredLane++;
                        if (desiredLane == 3)
                        {
                            desiredLane = 2;
                        }
                    }
                    else
                    {
                        OnSwipe?.Invoke(SwipeDirection.Left);
                        switchSideSound.Play();
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
                    if (direction.y > 0 && isGrounded)
                    {
                        Jump();
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
        if (player && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            GetComponentInChildren<Animator>().SetTrigger("jump");
            jumpSound.Play();
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private bool SwipeDistanceCheckMet()
    {
        return Vector3.Distance(fingerDownPosition, fingerUpPosition) > minDistanceForSwipe;
    }

    //private void IsPlaying()
    //{
    //    if (Time.timeScale == 0)
    //    {
    //        isPlaying = false;
    //    }
    //    else
    //    {
    //        isPlaying = true;
    //    }
    //}

    private void CheckGroundStatus()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCollider.bounds.center, Vector3.down, out hit, playerCollider.bounds.extents.y + 0.1f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }
    }
}
