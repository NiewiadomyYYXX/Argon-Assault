using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves based upon player input")] [SerializeField] float MoveSpeed = 30f;
    [Tooltip("Max range of player movement left and right")] [SerializeField] float xRange = 10f;
    [Tooltip("Max range of player movement up and down")] [SerializeField] float yRange = 9f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen posistion based tuning")]
    [Tooltip("idk moving something")] [SerializeField] float pitchFactor = -2f;
    [Tooltip("idk moving something")] [SerializeField] float controlYawFactor = 5f;

    [Header("Screen posistion based tuning")]
    [Tooltip("idk moving something")] [SerializeField] float controlPitchFactor = -10f;
    [Tooltip("idk moving something")] [SerializeField] float controlRollFactor = -15f;


    float xThrow, yThrow;

    void Update()
    {
        ProccessTranslation();
        ProccessRotation();
        ProccessFiring();
    }

    void ProccessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            ActiveLasers(true);
        }
        else
        {
            ActiveLasers(false);
        }
    }

    void ActiveLasers(bool isEnabled)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isEnabled;
        }
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
