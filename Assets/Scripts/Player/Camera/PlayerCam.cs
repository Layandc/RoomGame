using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    public static PlayerCam Instance;

    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    public Transform orientation;

    private float xRotation;
    private float yRotation;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void AbleCamerToMove()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void DisAbleCamerToMove()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {


        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        //rotate orientation and cam

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); 
        //orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        PlayerMovement.Instance.transform.rotation = Quaternion.Euler(0, yRotation, 0);

    }

}
