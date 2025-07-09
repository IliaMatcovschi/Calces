using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    [SerializeField] private float sens_X;
    [SerializeField] private float sens_Y;

    //Player and hands ref
    [SerializeField] private GameObject Player;

    //Camera position ref
    [SerializeField] Transform CameraPos;

    //Axis
    private float rotation_X;
    private float rotation_Y;

    void Start()
    {
        //Cursor settings
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Camera follow
        transform.position = CameraPos.position;

        //Get Axis
        float mouse_X = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens_X;
        float mouse_Y = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens_Y;

        //X and Y rotation values changes every frame
        rotation_X -= mouse_Y;
        rotation_Y += mouse_X;

        //X rotation limit
        rotation_X = Mathf.Clamp(rotation_X, -90, 90);

        //Camera rotation
        transform.rotation = Quaternion.Euler(/*rotation_X*/ 0, rotation_Y, 0);
    }
    private void FixedUpdate()
    {
        Player.transform.rotation = Quaternion.Euler(0, rotation_Y, 0);
    }
}
