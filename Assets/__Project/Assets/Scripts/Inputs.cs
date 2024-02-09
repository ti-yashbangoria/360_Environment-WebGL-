using TMPro;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    #region Variables

    [Header("{Scripts}")]
    [SerializeField] MobileCheck mobileCheck;


    [Header("{Components}")]
    [SerializeField] Joystick joystick;
    [SerializeField] Rigidbody mainCameraRb;
    [SerializeField] Transform mainCameraTrans;

    [Space]
    [SerializeField] TMP_Text _text;

    [Header("{Values}")]
    [Header("Movement Values")]
    [SerializeField] float movSpeed;
    public Vector3 directionVector;
    public Vector3 cameraVelocity;
    float directionX;
    float directionZ;

    [Header("Rotation Values")]
    [SerializeField] float sensitivity = 2f;
    float horizontalInput;
    float verticalInput;
    Vector3 rotationVector;

    [Range(0f, 90f)]
    [SerializeField] float verticalRotation = 70;

    [Header("Checks")]
    public bool canMove = true;
    Rect top;

    #endregion


    #region Main Methods

    private void Start()
    {
        top = new Rect(0,0, Screen.width, Screen.height / 4f);


        if (MobileCheck.GetDeviceOS() == "Windows" || MobileCheck.GetDeviceOS() == "MacOS" ||
            MobileCheck.GetDeviceOS() == "Linux")
        {
            mobileCheck.ChangeRunningOSState(RunningOS.NotMobileDevice);
        }
        else
        {
            mobileCheck.ChangeRunningOSState(RunningOS.MobileDevice);
        }
    }

    private void Update()
    {
        TakeInputs();
        ApplyInput();
    }

    #endregion


    #region Helper Methods

    void TakeInputs()
    {
        directionX = joystick.Horizontal;
        directionZ = joystick.Vertical;

        if (mobileCheck.runningOS ==  RunningOS.NotMobileDevice)
        {
            _text.text = "IsNotMobile:" + MobileCheck.GetDeviceOS();
        }

        else if(mobileCheck.runningOS == RunningOS.MobileDevice)
        {
            if (Input.touchCount > 0)
            {
                if (top.Contains(Input.GetTouch(0).position))
                    return;

                if (!top.Contains(Input.GetTouch(0).position))
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        horizontalInput += Input.GetTouch(0).deltaPosition.x;
                        verticalInput += Input.GetTouch(0).deltaPosition.y;
                    }
                }
            }
            _text.text = "IsMobile:" + MobileCheck.GetDeviceOS();
        }
/*        else
        {
            _text.text =  "IsMobile:" + MobileCheck.GetDeviceOS();
        }
*/    }

    void ApplyInput()
    {
        // Rotation 
        rotationVector.y = horizontalInput / sensitivity;
        rotationVector.x = (-Mathf.Clamp(verticalInput / sensitivity, -verticalRotation, verticalRotation));

        mainCameraTrans.eulerAngles = rotationVector;

        // Movement 
        directionVector.x = directionX;
        directionVector.z = directionZ;

        if (!canMove)
        {
            mainCameraRb.velocity = Vector3.zero;
            return;
        }
        mainCameraRb.velocity = (mainCameraTrans.forward * directionVector.z + mainCameraTrans.right * directionVector.x) * movSpeed;
        cameraVelocity = mainCameraRb.velocity;
    }

#endregion
}

