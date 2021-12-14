using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    bool inputLeft;
    bool inputRight;
    bool inputUp;
    bool back;

    public float maxMoveSpeed;
    float[] moveSpeed;
    public float collisionSpeed;
    public float speedIncreaseSpeed;

    Rigidbody rig;


    public int speedForward = 12;
    public int speedSide = 6;

    private float dirX = 0;
    private float dirZ = 0;

    void Start()
    {
        rig = this.GetComponent<Rigidbody>();
        inputLeft = false;
        inputRight = false;
        inputUp = false;
        moveSpeed = new float[4]; //0=forward, 1=left, 2=right

        inputUp = true;


    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.tag == "Obstacle")
        {

            Debug.Log("장애물 충돌");
            ScoreManager.downScore = true;
            other.GetComponent<Collider>().enabled = false;
            other.GetComponent<itemObject>().getCollision();
            back = true;

        }
        else if (other.tag == "Star")
        {
            Debug.Log("별 충돌");
            ScoreManager.getScore = true;
            other.GetComponent<Collider>().enabled = false;
            other.GetComponent<itemObject>().getCollision();
        }
        else if (other.tag == "finish") {
            ScoreManager.isFinish = true;
            Debug.Log("끝");
            other.GetComponent<Collider>().enabled = false;
        }
    }

    void Update()
    {

        MovePlayer();

        if (back) {

            rig.MovePosition(rig.position + (-1)*transform.forward * collisionSpeed * Time.deltaTime);
            moveSpeed[0] = -1;
            back = false; 

        }
        // keydown
        //if (Input.GetKey(KeyCode.UpArrow)) {

            // 키입력이 아닌 자동움직임으로 변화시킬것. (지금은 키입력 = Start)

            //inputUp = true;
            if (moveSpeed[0] <= maxMoveSpeed)
            {
                moveSpeed[0] += speedIncreaseSpeed;
            }
            else if (moveSpeed[0] > maxMoveSpeed) {
                moveSpeed[0] = maxMoveSpeed;
            }
            //transform.localScale = new Vector3(1, 1, 1);
        //}
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (inputRight) {
                inputRight = false;
            }
            if (moveSpeed[1] <= maxMoveSpeed)
            {
                moveSpeed[1] += speedIncreaseSpeed;
            }
            else if (moveSpeed[1] > maxMoveSpeed)
            {
                moveSpeed[1] = maxMoveSpeed;
            }
            inputLeft = true;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            if (inputLeft) {
                inputLeft = false;
            }
            if (moveSpeed[2] <= maxMoveSpeed)
            {
                moveSpeed[2] += speedIncreaseSpeed;
            }
            else if (moveSpeed[2] > maxMoveSpeed)
            {
                moveSpeed[2] = maxMoveSpeed;
            }
            inputRight = true;
        }

        // key up
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            // remark => Auto Movement 

            // inputUp = false;
            
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            inputLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            inputRight = false;
        }
        
    }

    void MovePlayer()
    {
        Debug.Log("무브플레이어");
        dirX = 0; //좌우이동방향  
        dirZ = 0; //전진후진이동방향

        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))
        {
            Debug.Log("오큘러스인풋");
            Vector2 coord = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            var absX = Mathf.Abs(coord.x);
            var absY = Mathf.Abs(coord.y);

            if (absX > absY)
            {
                //right
                if (coord.x > 0)
                {
                    
                    dirX += 1;
                }
                else
                {
                    dirX = -1;

                    

                }
            }
            else
            {
                if (coord.y > 0)
                {
                    //dirZ = +1;
                    if (inputLeft)
                    {
                        inputLeft = false;
                    }
                    inputRight = true;
                }
                else
                {

                    if (inputRight)
                    {
                        inputRight = false;
                    }
                    inputLeft = true;
                    //dirZ = -1;
                }
            }
            Vector3 moveDir = new Vector3(dirX * speedSide, 0, dirZ * speedForward);
            transform.Translate(moveDir * Time.smoothDeltaTime);
        }

    }

    private void FixedUpdate()
    {
        // AddForce를 적용하면 관성에의해 회전되므로 회전을 막기위해 오브젝트스페이스계의 transform 사용함

        if (inputUp)
        {
            rig.MovePosition(rig.position + transform.forward * moveSpeed[0] * Time.deltaTime);

        }
        else {
            if (moveSpeed[0] > 0)
            {
                rig.MovePosition(rig.position + transform.forward * moveSpeed[0] * Time.deltaTime);
                moveSpeed[0] -= speedIncreaseSpeed;
            }
            else if (moveSpeed[0] < 0)
            {
                moveSpeed[0] = 0;
            }
        }
        if (inputLeft)
        {
            rig.MovePosition(rig.position + ((-1) * transform.right * moveSpeed[1] * Time.deltaTime));
        }
        else {
            if (moveSpeed[1] > 0)
            {
                rig.MovePosition(rig.position + ((-1) * transform.right * moveSpeed[1] * Time.deltaTime));
                moveSpeed[1] -= speedIncreaseSpeed;
            }
            else if (moveSpeed[1] < 0)
            {
                moveSpeed[1] = 0;
            }

        }
        if (inputRight)
        {
            rig.MovePosition(rig.position + (transform.right * moveSpeed[2] * Time.deltaTime));
        }
        else
        {
            if (moveSpeed[2] > 0)
            {
                rig.MovePosition(rig.position + (transform.right * moveSpeed[2] * Time.deltaTime));
                moveSpeed[2] -= speedIncreaseSpeed;
            }
            else if (moveSpeed[2] < 0) {
                moveSpeed[2] = 0;
            }

        }
    }
}
