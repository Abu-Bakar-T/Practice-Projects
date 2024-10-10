using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager
    public static GameManager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    [HideInInspector] public bool isGameOver = false;
    
}
