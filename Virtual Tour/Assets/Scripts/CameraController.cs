using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera userCamera;
    public float cameraRotationSensitivity;
    public Canvas mouseCursorCanvas;
    public GameObject mouseCursorWhenRotatingCamera;

    void Update()
    {
        if (isUserRequestingCameraRotation)
        {
            RotateCameraAsUserRequested();
            SetMouseCursorToIndicateCameraRotation();
        }
        else
        {
            SetMouseCursorToStandard();
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

    void SetMouseCursorToIndicateCameraRotation()
    {
        Vector3 newMousePos = new Vector3();
        Cursor.visible = false;
        mouseCursorWhenRotatingCamera.SetActive(true);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(mouseCursorCanvas.GetComponent<RectTransform>(), new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.1f), null, out newMousePos);
        mouseCursorWhenRotatingCamera.GetComponent<RectTransform>().position = newMousePos;
        mouseCursorWhenRotatingCamera.transform.up = new Vector3(Mathf.Clamp(Input.mousePosition.x, 0f, Screen.width) - Screen.width / 2f, Mathf.Clamp(Input.mousePosition.y, 0f, Screen.height) - Screen.height / 2f).normalized;
    }

    void SetMouseCursorToStandard()
    {
        mouseCursorWhenRotatingCamera.SetActive(false);
        Cursor.visible = true;
    }
}
