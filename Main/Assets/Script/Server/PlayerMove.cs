using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool useServer = false;
    public FriendManager Friend;
    Rigidbody myRigi;
    float times = 0;
    private void Awake()
    {
        myRigi = transform.GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {

        float hmove = Input.GetAxis("Horizontal");
        float vmove = Input.GetAxis("Vertical");
        Vector3 vec = transform.forward * vmove + transform.right * hmove;
        myRigi.position += vec * 10f* Time.fixedDeltaTime;

        times += Time.fixedDeltaTime;
        if (times >= 1.0f && useServer)
        {
            LoginManager.instance.SendPos(transform.position);
            LoginManager.instance.client.messageProcess[1] = Friend.UpdateFirend;
            Debug.Log("send" + transform.position);
            times = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
    }

}
