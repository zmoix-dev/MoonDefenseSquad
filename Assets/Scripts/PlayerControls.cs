using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float moveFactor = 15f;
    [SerializeField] float clampRangeX = 14f;
    [SerializeField] float clampRangeYTop = 14f;
    [SerializeField] float clampRangeYBot = -2f;
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionRollFactor = -2f;
    [SerializeField] float positionYawFactor = -3.5f;
    [SerializeField] float controlPitchFactor = -5f;
    [SerializeField] float controlRollFactor = 5f;
    [SerializeField] float controlYawFactor = 3.5f;

    [SerializeField] ParticleSystem leftwingLaser;
    [SerializeField] ParticleSystem rightwingLaser;

    void Update()
    {
        float horizThrow = Input.GetAxis("Horizontal");
        float vertThrow = Input.GetAxis("Vertical");
        ProcessTranslation(horizThrow, vertThrow);
        ProcessRotation(horizThrow, vertThrow);
        ProcessShoot();
    }

    private void ProcessTranslation(float horizThrow, float vertThrow)
    {
        float xOffset = horizThrow * Time.deltaTime * moveFactor;
        float newXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(newXPos, -clampRangeX, clampRangeX);

        float yOffset = vertThrow * Time.deltaTime * moveFactor;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYPos, clampRangeYBot, clampRangeYTop);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation(float horizThrow, float vertThrow) {
        float pitchFromPosition = transform.localPosition.y * positionPitchFactor;
        float pitchFromControl = vertThrow * controlPitchFactor;
        
        float rollFromPosition = transform.localPosition.x * positionRollFactor;
        float rollFromControl = horizThrow * controlRollFactor;
        
        float yawFromPosition = transform.localPosition.x * positionYawFactor;
        float yawFromControl = horizThrow * controlYawFactor;

        float pitch = pitchFromPosition + pitchFromControl;
        float roll = rollFromPosition + rollFromControl;
        float yaw = yawFromPosition + yawFromControl;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessShoot() {
        bool isFiring = Input.GetKey(KeyCode.Space);
        if (isFiring) {
            Debug.Log("Firing");
            if (!leftwingLaser.isEmitting) {
                leftwingLaser.Play();
            }
            if (!rightwingLaser.isEmitting) {
                rightwingLaser.Play();
            }
        } else {
            if (leftwingLaser.isEmitting) {
                leftwingLaser.Stop();
            }
            if (rightwingLaser.isEmitting) {
                rightwingLaser.Stop();
            }
        }
    }
}
