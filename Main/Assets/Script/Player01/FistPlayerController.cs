using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPlayerController : MonoBehaviour
{
    [SerializeField] GameObject camControl;
    [Header("----- camera status ----")]
    public float maxAngle = 45f;
    public float minAngle = -20f;
    public float camHeight = 2.0f;

    [Header("----- player status ----")]
    public float moveSpeed = 2.5f;
    FistPlayerMovement playerMovement;

    Animator playerAnim;
    Rigidbody playerRigi;

    LayerMask ss = 1 << 9;
    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerRigi = GetComponent<Rigidbody>();

        playerMovement = new FistPlayerMovement();
        playerMovement.SetPlayerComponent(playerAnim, playerRigi, gameObject, camControl);

        //transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
    }

    private void FixedUpdate()
    {
        playerMovement.PlayerMove(moveSpeed);
        playerMovement.CameraMove(1.5f, camHeight);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Camera.main.transform.position, 0.5f);
    }
}
