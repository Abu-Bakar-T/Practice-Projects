using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Board : MonoBehaviour
{
    [Header("Input Settings: ")]
    [SerializeField] private LayerMask boxLayerMask;
    [SerializeField] private float touchRadius;
    [SerializeField] private AudioSource SFX;

    [Header("Mark Sprites: ")]
    [SerializeField] private Sprite spriteX;
    [SerializeField] private Sprite spriteO;

    [Header("Mark Colors: ")]
    [SerializeField] private Color colorX;
    [SerializeField] private Color colorO;

    [SerializeField] private TextMeshProUGUI turnText;

    public UnityAction<Mark,Color> onWinAction;

    public Mark[] marks;
    private Camera cam;
    private Mark currentMark;
    private bool canPlay;
    private LineRenderer lineRenderer;
    private int marksCount = 0;

    private void Start()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        currentMark = Mark.X;
        marks = new Mark[9];
        canPlay = true;
    }

    private void Update()
    {
        // Mouse Input
        if (canPlay && Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            HandleInput(touchPosition);
        }

        if (canPlay && Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++) 
            {
                Touch touch = Input.GetTouch(i);

                // Process touch only if it is in beginning phase
                if(touch.phase == TouchPhase.Began)
                {
                    HandleInput(cam.ScreenToWorldPoint(touch.position));
                }
            }
        }
    }

    private void HandleInput(Vector2 touchPosition)
    {
        Collider2D hit = Physics2D.OverlapCircle(touchPosition, touchRadius, boxLayerMask);

        if (hit)
        {
            HitBox(hit.GetComponent<Box>());
        }
    }
    private void HitBox(Box box)
    {
        if(!box.isMarked)
        {
            marks[box.index] = currentMark;

            SFX.Play();
            box.SetAsMarked(GetSprite(), currentMark, GetColor());
            marksCount++;

            // Win Check
            bool won = CheckIfWin();
            if (won)
            {
                if(onWinAction != null)
                {
                    onWinAction.Invoke(currentMark, GetColor());
                }
                canPlay = false;
                return;
            }

            if (marksCount == 9)
            {
                if(onWinAction != null)
                {
                    onWinAction.Invoke(Mark.None, Color.white);
                }
                canPlay= false;
                return;
            }
            SwitchPlayer();
        }        
    }

    private bool CheckIfWin()
    {
        return
            AreBoxesMatched(0, 1, 2) || AreBoxesMatched(3, 4, 5) || AreBoxesMatched(6, 7, 8) ||
            AreBoxesMatched(0, 3, 6) || AreBoxesMatched(1, 4, 7) || AreBoxesMatched(2, 5, 8) ||
            AreBoxesMatched(0, 4, 8) || AreBoxesMatched(2, 4, 6);
    }

    private bool AreBoxesMatched(int i, int j, int k)
    {
        Mark m = currentMark;
        bool matched = (marks[i] == m && marks[j] == m && marks[k] == m);
        if (matched)
        {
            DrawLine(i, k);
        }
        return matched;
    }

    private void DrawLine(int i, int k)
    {
        lineRenderer.SetPosition(0, transform.GetChild(i).position);
        lineRenderer.SetPosition(1, transform.GetChild(k).position);
        Color color = GetColor();
        color.a = 0.7f;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.enabled = true;
    }

    private void SwitchPlayer()
    {
        turnText.text = (currentMark == Mark.X) ? Mark.O.ToString() + " Turn" : Mark.X.ToString() + " Turn";
        
        currentMark = (currentMark == Mark.X) ? Mark.O : Mark.X;
    }

    private Color GetColor()
    {
        return (currentMark == Mark.X) ? colorX : colorO;
    }

    private Sprite GetSprite()
    {
        return (currentMark == Mark.X) ? spriteX : spriteO;
    }
}
