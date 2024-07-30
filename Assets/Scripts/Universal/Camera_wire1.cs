using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_wire1 : MonoBehaviour
{
    [Header("移动速度")]
    public float camera_moveSpeed = 2f;
    [Header("旋转速度")]
    public float camera_rotateSpeed = 2f;

    private bool a, b = false;
    private Vector3 movement = Vector3.zero;
    public float Altitude = 4;//高度

    private Vector3 newPosition;
    void Update()
    {
        camer_Keydetection();
    }

    public void camer_Keydetection()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Altitude += 0.1f;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                Altitude -= 0.1f;
            }

            if (Altitude <= 2f)
            {
                Altitude = 2f;
            }
            newPosition.y = Altitude;
            transform.position = newPosition;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            a = true;
        }
        else
        {
            a = false;
        }

        if (Input.GetMouseButton(1))
        {
            b = true;
        }
        else
        {
            b = false;
        }
        camera_move(camera_moveSpeed);
        camera_rotation(camera_rotateSpeed);
    }

    public void camera_move(float camera_moveSpeed)
    {

        if (a == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                movement += transform.forward;//1.317
            }
            if (Input.GetKey(KeyCode.S))
            {
                movement -= transform.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movement -= transform.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movement += transform.right;
            }
            movement.Normalize();

             newPosition = transform.position + (movement * Time.deltaTime * camera_moveSpeed);
            transform.position = newPosition;
        }

    }

    public void camera_rotation(float camera_rotateSpeed)
    {
        if (b == true)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
           // transform.Rotate(new Vector3(-mouseY, mouseX) * camera_rotateSpeed);
            transform.Rotate(new Vector3(-0, mouseX) * camera_rotateSpeed);
            //锁定上下轴
        }
    }
}
