using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPlayerController : MonoBehaviour
{
    [SerializeField] GameObject camControl;
    FistPlayerMovement playerMovement;

    Animator playerAnim;
    Rigidbody playerRigi;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerRigi = GetComponent<Rigidbody>();

        playerMovement = new FistPlayerMovement();
        playerMovement.SetPlayerComponent(playerAnim, playerRigi, gameObject, camControl);
    }

    private void FixedUpdate()
    {
        playerMovement.PlayerMove(2.5f);
        playerMovement.CameraMove(1.5f);
    }

}
