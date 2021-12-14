using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour
{

    public int speedForward = 12;
    public int speedSide = 6;

    private float dirX = 0;
    private float dirZ = 0;


    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer() {

        dirX = 0; //좌우이동방향  
        dirZ = 0; //전진후진이동방향

        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick)) {
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
            else {
                if (coord.y > 0)
                {
                    dirZ = +1;
                }
                else {
                    dirZ = -1;
                }
            }
            Vector3 moveDir = new Vector3(dirX * speedSide, 0, dirZ * speedForward);
            transform.Translate(moveDir * Time.smoothDeltaTime);
        }

    }
}
