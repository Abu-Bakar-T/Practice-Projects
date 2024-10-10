using UnityEngine;

public class Splatters : MonoBehaviour
{
    #region Singleton class: Splatters
    public static Splatters instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [Header("Refernces")]
    [SerializeField] Color[] colors = new Color[2];
    [SerializeField] GameObject splatterPrefab;
    [SerializeField] Sprite[] splatterSprites;
    [SerializeField] AudioSource SplatterSource;

    public void AddSplatter(Transform obstacle, Vector3 pos, int colorIndex)
    {
        GameObject splatter = Instantiate(splatterPrefab,pos,Quaternion.Euler(new Vector3(0f,0f,Random.Range(-320f,320f))), obstacle);
        SpriteRenderer sr = splatter.GetComponent<SpriteRenderer>();
        SplatterSource.Play();
        colors[colorIndex].a = 1f;
        sr.color = colors[colorIndex];
        sr.sprite = splatterSprites[Random.Range(0,splatterSprites.Length)];
    }
}
