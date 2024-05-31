using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerMulti : MonoBehaviour
{
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource runSound;

    public float moveSpeed = 3;
    public float leftRightSpeed = 6;
    public static bool canMove = false;
    public bool isJumping = false;
    public bool comingDown = false;
    public GameObject playerObject;

    private Vector2 startPos;
    private float startTime;
    public float minSwipeDistY = 50f;
    public float maxSwipeTime = 0.5f;

    public float minX = -5.1f;
    public float maxX = 5.1f;

    private PhotonView photonView;

    void Start()
    {
        runSound.loop = true;
        photonView = GetComponent<PhotonView>();

        if (photonView == null)
        {
            Debug.LogError("PhotonView component is missing.");
            enabled = false; // Disable the script if PhotonView is missing
            return;
        }

        if (runSound == null)
        {
            Debug.LogError("RunSound is not assigned in the Inspector.");
        }

        if (jumpSound == null)
        {
            Debug.LogError("JumpSound is not assigned in the Inspector.");
        }

        if (playerObject == null)
        {
            Debug.LogError("PlayerObject is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        Debug.Log(transform.localPosition.x);
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        transform.rotation = Quaternion.Euler(Vector3.zero);

        HandleRunningSound();
        HandleLeftRightMovement();
        HandleJumping();
        HandleTouchInput();
    }

    private void HandleRunningSound()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && runSound != null && !runSound.isPlaying)
        {
            runSound.Play();
        }
        else if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && runSound != null && runSound.isPlaying)
        {
            runSound.Stop();
        }
    }

    private void HandleLeftRightMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
    }

    private void HandleJumping()
    {
        if (SwipeUp() && !isJumping && playerObject != null)
        {
            isJumping = true;
            playerObject.GetComponent<Animator>().Play("Jump");
            StartCoroutine(JumpSequence());

            if (jumpSound != null)
            {
                jumpSound.Play();
            }
        }
    }

    private void HandleTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x < Screen.width * 0.5f)
            {
                MoveLeft();
            }
            else if (touch.position.x >= Screen.width * 0.5f)
            {
                MoveRight();
            }

            if (touch.phase == TouchPhase.Began && touch.position.y > Screen.height * 0.7f && !isJumping && playerObject != null)
            {
                isJumping = true;
                playerObject.GetComponent<Animator>().Play("Jump");
                StartCoroutine(JumpSequence());

                if (jumpSound != null)
                {
                    jumpSound.Play();
                }
            }
        }
    }

    private bool SwipeUp()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                startTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                float swipeDistVertical = (new Vector3(0, touch.position.y) - new Vector3(0, startPos.y)).magnitude;

                if (swipeDistVertical > minSwipeDistY && (Time.time - startTime) < maxSwipeTime)
                {
                    return true; // Swipe-up detected
                }
            }
        }
        return false;
    }

    private IEnumerator JumpSequence()
    {
        yield return new WaitForSeconds(0.45f);
        comingDown = true;
        yield return new WaitForSeconds(0.45f);
        isJumping = false;
        comingDown = false;

        if (playerObject != null)
        {
            playerObject.GetComponent<Animator>().Play("Standard Run");
        }
    }

    private void MoveLeft()
    {
        if (transform.localPosition.x > minX)
        {
            transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
        }
    }

    private void MoveRight()
    {
        if (transform.localPosition.x < maxX)
        {
            transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
        }
    }
}
