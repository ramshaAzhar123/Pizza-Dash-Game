using Photon.Pun;
using System.Collections;
using UnityEngine;
public class PlayerMove : MonoBehaviourPunCallbacks
{
    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource runSound;

    public float moveSpeed = 3;
    public float leftRightSpeed = 6;
    static public bool canMove = false;
    public bool isJumping = false;
    public bool comingDown = false;
    public GameObject playerObject;

    private Vector2 startPos;
    private float startTime;
    public float minSwipeDistY = 50f;
    public float maxSwipeTime = 0.5f;

    public float minX = -5.1f;
    public float maxX = 5.1f;

    void Start()
    {
        runSound.loop = true;
    }

    void Update()
    {
        Debug.Log(transform.localPosition.x);
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        transform.rotation = Quaternion.Euler(Vector3.zero);
    
        if (photonView.IsMine)
        {
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !runSound.isPlaying)
            {
                runSound.Play();
            }
            else if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && runSound.isPlaying)
            {
                runSound.Stop();
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }

            if (SwipeUp())
            {
                if (!isJumping)
                {
                    isJumping = true;
                    playerObject.GetComponent<Animator>().Play("Jump");
                    StartCoroutine(JumpSequence());
                    jumpSound.Play();
                }
            }

            // Touch input for mobile
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x < Screen.width * 0.5f)
                {
                    // Left side of the screen is touched
                    MoveLeft();
                }
                else if (touch.position.x >= Screen.width * 0.5f)
                {
                    // Right side of the screen is touched
                    MoveRight();
                }

                // Swipe-up for jump
                if (touch.phase == TouchPhase.Began && touch.position.y > Screen.height * 0.7f)
                {
                    if (isJumping == false)
                    {
                        isJumping = true;
                        playerObject.GetComponent<Animator>().Play("Jump");
                        StartCoroutine(JumpSequence());
                        jumpSound.Play();
                    }
                }
            }
        }
    }

           

    bool SwipeUp()
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

    IEnumerator JumpSequence()
    {
        yield return new WaitForSeconds(0.45f);
        comingDown = true;
        yield return new WaitForSeconds(0.45F);
        isJumping = false;
        comingDown = false;
        playerObject.GetComponent<Animator>().Play("Standard Run");
    }

    void MoveLeft()
    {
        if (transform.localPosition.x > minX)
        {
            transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
        }
    }

    void MoveRight()
    {
        if (transform.localPosition.x < maxX)
        {
            transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
        }
    }
}
