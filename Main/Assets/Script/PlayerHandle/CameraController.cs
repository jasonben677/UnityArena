using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 80.0f;
    public float cameraDampValue = 0.01f;

    private GameObject playerHandle; //camera水平旋轉操控
    private GameObject cameraHandle; //camera垂直旋轉操控
    private float tempEulerX;
    private GameObject model;
    private Camera camera;

    private Vector3 cameraDampvelocity;

    // Start is called before the first frame update
    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        model = playerHandle.GetComponent<ActorController>().model;
        camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tempModelEuler = model.transform.eulerAngles;

        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, - 20, 30); //限制camera上下的旋轉角度
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

        //cameraHandle.transform.Rotate(Vector3.right, pi.Jup * verticalSpeed * Time.fixedDeltaTime);

        model.transform.eulerAngles = tempModelEuler; //模型不會再跟著攝影機旋轉了~

        //讓camera追空物件，有延遲視覺效果
        //camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 0.15f); 
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampvelocity, cameraDampValue);
        camera.transform.eulerAngles = transform.eulerAngles;
    }
}
