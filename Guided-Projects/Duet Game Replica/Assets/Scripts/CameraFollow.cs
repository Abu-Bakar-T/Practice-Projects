using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Reference ")]
    [SerializeField] Transform target;

    void Start()
    {
        target = PlayerMovement.instance.transform;
    }

    void LateUpdate()
    {
        transform.position = target.position;
    }
}
