using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    #region Singleton class: PlayerMovement
    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [Header("Colliders: ")]
    [SerializeField] CircleCollider2D redBallCollider;
    [SerializeField] CircleCollider2D blueBallCollider;

    [Header("Variables: ")]
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float touchPosX;
    [SerializeField] bool isGamePaused = false;
    [SerializeField] int level = 0;

    [Header("References: ")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector3 startPosition;
    [SerializeField] Camera cam;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] GameOver gameOver;

    private void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        MoveUp();
    }
    void Update()
    {
        if(!GameManager.instance.isGameOver)
        {
            // Mobile Inputs
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    // Procesing the Touch
                    HandleInput(cam.ScreenToWorldPoint(touch.position));
                }
            }
            else
            {
                rb.angularVelocity = 0f;
            }

            // Mouse Input

            if (Input.GetMouseButton(0))
            {
                touchPosX = cam.ScreenToWorldPoint(Input.mousePosition).x;
                if (touchPosX > 0.01f)
                {
                    RotateRight();
                }
                else
                {
                    RotateLeft();
                }
            }
            else
            {
                rb.angularVelocity = 0f; 
            }


            // Keyboard Input
            if (Input.GetKey(KeyCode.LeftArrow))
                RotateLeft();
            else if (Input.GetKey(KeyCode.RightArrow))
                RotateRight();
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                rb.angularVelocity = 0f;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                if(isGamePaused)
                {
                    pauseMenu.Continue();
                }
                else
                    pauseMenu.Pause();
            }
            //Stop Rotation when key is released
        }
    }

    private void HandleInput(Vector2 touchPosition)
    {
        if(touchPosition.x > 0.01f)
        {
            RotateRight();
        }
        else
        {
            RotateLeft();
        }
    }

    void MoveUp()
    {
        rb.velocity = Vector2.up * speed;
    }

    void RotateLeft()
    {
        rb.angularVelocity = rotationSpeed;
    }

    void RotateRight()
    {
        rb.angularVelocity = -rotationSpeed;
    }

    public void Restart()
    {
        redBallCollider.enabled = false;
        blueBallCollider.enabled = false;
        rb.angularVelocity = 0f;
        rb.velocity = Vector2.zero;

        //Use dotween to move back to start
        transform.DORotate(Vector3.zero, 1f).SetDelay(1f).SetEase(Ease.InOutBack);
        transform.DOMove(startPosition, 1f).SetDelay(1f).SetEase(Ease.OutFlash).OnComplete(() =>
        {
            redBallCollider.enabled = true;
            blueBallCollider.enabled = true;
            GameManager.instance.isGameOver = false;
            MoveUp();
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelEnd"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Win");

            if(level != 2)
            {
                int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

                if (currentLevelIndex < SceneManager.sceneCountInBuildSettings)
                    SceneManager.LoadSceneAsync(++currentLevelIndex);
            }
            else
            {
                if (gameOver != null)
                {
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                    gameOver.ActiveGameOver();
                }
            }
        }
    }
}
