using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 30f;

    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 9f;

    [SerializeField] float pitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlYawFactor = 5f;
    [SerializeField] float controlRollFactor = -15f;

    float xThrow, yThrow;

    void Update()
    {
        ProccessTranslation();
        ProccessRotation();
    }

    void ProccessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * pitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * controlYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProccessTranslation()
    {
        float xThrow = Input.GetAxis("Horizontal");
        float yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * MoveSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float newXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * MoveSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float newYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }
}
