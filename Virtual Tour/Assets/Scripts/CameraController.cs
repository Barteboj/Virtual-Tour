using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera userCamera;
    public float cameraRotationSensitivity;

    void Update()
    {
        if (isUserRequestingCameraRotation)
        {
            RotateCameraAsUserRequested();
        }
    }

    public bool isUserRequestingCameraRotation
    {
        get
        {
            return Input.GetMouseButton(0);
        }
    }

    void RotateCameraAsUserRequested()
    {
        userCamera.transform.Rotate(requestedCameraRotationChange, Space.World);
    }

    public Vector3 requestedCameraRotationChange
    {
        get
        {
            return requestedCameraRotationDirection * requestedCameraRotationSpeed * Time.deltaTime;
        }
    }

    public Vector3 requestedCameraRotationDirection
    {
        get
        {
            Vector3 requestedRotationDirection = userCamera.transform.TransformDirection(-userCamera.transform.InverseTransformPoint(userCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f))).y, 0f, 0f);
            requestedRotationDirection.y = userCamera.transform.InverseTransformPoint(userCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f))).x;
            return requestedRotationDirection.normalized;
        }
    }

    public float requestedCameraRotationSpeed
    {
        get
        {
            return Vector2.Distance(Vector2.zero, new Vector2((Mathf.Clamp(Input.mousePosition.x, 0f, Screen.width) - Screen.width / 2f) / Screen.width, (Mathf.Clamp(Input.mousePosition.y, 0f, Screen.height) - Screen.height / 2f) / Screen.width)) * cameraRotationSensitivity;
        }
    }
}
