using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera FirstPerson;
    public Camera ThirdPerson;
    // Start is called before the first frame update
    void Start()
    {
        EnableCamera(ThirdPerson);
        DisableCamera(FirstPerson);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            if (FirstPerson.enabled)
            {
                EnableCamera (ThirdPerson);
                DisableCamera (FirstPerson);
            }
            else if (ThirdPerson.enabled)
            {
                EnableCamera (FirstPerson);
                DisableCamera (ThirdPerson);
            }
        }
    }

    //As i want to enable and disable audio listener manually so creating function for
    //enabing and disabling of camera

    private void EnableCamera(Camera camera)
    {
        camera.enabled = true;
        // get component is used to get specific component of an object. Autocorrect helps in using it correctly.
        AudioListener listener = camera.GetComponent<AudioListener>();
        if (listener != null )
        {
            listener.enabled = true;
        }
    }
    private void DisableCamera(Camera camera)
    {
        camera.enabled = false;
        // get component is used to get specific component of an object. Autocorrect helps in using it correctly.
        AudioListener listener = camera.GetComponent<AudioListener>();
        if (listener != null)
        {
            listener.enabled = false;
        }
    }
}
