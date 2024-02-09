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

    float mouseHorizontalInput;
    float mouseVerticalInput;
    Vector3 mouseRotationVector;
    [SerializeField] float mouseSensitivity = 2f;


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

#if UNITY_WEBGL && !UNITY_EDITOR
            Debug.Log("Running on WebGL");

            // Check if running on mobile device
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Debug.Log("Running on mobile device");

                // Enable WebGL for mobile
                WebGLInput.captureAllKeyboardInput = true;

                if (Input.touchCount > 0)
                {
                    if (top.Contains(Input.GetTouch(0).position))
                        return;

                    if (!top.Contains(Input.GetTouch(0).position))
                    {
                        if(Input.touches[0].phase == TouchPhase.Moved)
                        {
                            horizontalInput += Input.GetTouch(0).deltaPosition.x;
                            verticalInput += Input.GetTouch(0).deltaPosition.y;
                        }
                        
                    }
                }   
                _text.text = "IsMobile:" + MobileCheck.GetDeviceOS();

                // Add any other WebGL mobile-specific configurations here
            }
            else
            {
                Debug.Log("Running on WebGL but not on mobile");

                if (Input.GetMouseButton(0))
                {
                    mouseHorizontalInput += Input.GetAxis("Mouse X") * mouseSensitivity;
                    mouseVerticalInput -= Input.GetAxis("Mouse Y") * mouseSensitivity;
                }
                    _text.text = "IsNotMobile:" + MobileCheck.GetDeviceOS();
            }
#else
        Debug.Log("Not running on WebGL");
#endif
        //if (Input.touchCount > 0)
        //{
        //    if (top.Contains(Input.GetTouch(0).position))
        //        return;

        //    if (!top.Contains(Input.GetTouch(0).position))
        //    {
        //        if (Input.touches[0].phase == TouchPhase.Moved)
        //        {
        //            horizontalInput += Input.GetTouch(0).deltaPosition.x;
        //            verticalInput += Input.GetTouch(0).deltaPosition.y;
        //        }

        //    }
        //}
    }

    void ApplyInput()
    {
        // Rotation 

        //rotationVector.y = horizontalInput / sensitivity;
        //rotationVector.x = (-Mathf.Clamp(verticalInput / sensitivity, -verticalRotation, verticalRotation));

        //mainCameraTrans.eulerAngles = rotationVector;
#if UNITY_WEBGL && !UNITY_EDITOR
            //Debug.Log("Running on WebGL");
            // Check if running on mobile device

            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                // Enable WebGL for mobile
                //Debug.Log("Running on mobile device");

                rotationVector.y = horizontalInput / sensitivity;
                rotationVector.x = (-Mathf.Clamp(verticalInput / sensitivity, -verticalRotation, verticalRotation));

                mainCameraTrans.eulerAngles = rotationVector;
            }
            else
            {
                //Debug.Log("Running on WebGL but not on mobile");
                if (Input.GetMouseButton(0))
                {
                    Debug.Log("mouseRotationVector:" + mouseRotationVector);
                    mainCameraTrans.eulerAngles = new Vector3(mouseVerticalInput, mouseHorizontalInput, 0) ;
                }
            }
#else
        //Debug.Log("Not running on WebGL");
#endif



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

