using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 80.0f;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;

    // Start is called before the first frame update
    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
    }

    // Update is called once per frame
    void Update()
    {
        playerHandle.transform.Rotate(Vector3.up, pi.JRight * horizontalSpeed * Time.deltaTime);
        tempEulerX -= pi.JUp * verticalSpeed * Time.deltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, - 30, 40);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
    }
}
