using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    public float moveSpeed;
    public float maxFloatHeight = 10;
    public float minFloatHeight;
    public float turnSpeed;

    public Camera freeLookCam;
    private float currentHeight;
    private Animator anim;
    private float xRotation;


    void Start()
    {
        currentHeight = transform.position.y;
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        xRotation = freeLookCam.transform.rotation.eulerAngles.x;

        float horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W))
        {
            MoveCharcter(horizontalInput);
        }
        else
        {
            disableMovement();
        }
        currentHeight = Mathf.Clamp(transform.position.y, currentHeight, maxFloatHeight);
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
    }

    public void MoveCharcter(float horizontalInput)
    {
        Vector3 cameraForward = new Vector3(freeLookCam.transform.forward.x, 0, freeLookCam.transform.forward.z);
        transform.rotation = Quaternion.LookRotation(cameraForward);
        transform.Rotate(new Vector3(xRotation, 0, 0), Space.Self);

         anim.SetBool("isFlying", true);

        Vector3 forward = freeLookCam.transform.forward + freeLookCam.transform.right * horizontalInput;
        Vector3 flyDirection = forward.normalized;


        currentHeight += flyDirection.y * moveSpeed * Time.deltaTime;
        currentHeight = Mathf.Clamp(currentHeight, minFloatHeight, maxFloatHeight);

        transform.position += flyDirection * moveSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

    }

    public void disableMovement()
    {
        anim.SetBool("isFlying", false);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}