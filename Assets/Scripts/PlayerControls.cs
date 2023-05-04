using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]

    [Tooltip("How fast ship moves in response to player input.")] [SerializeField] float moveFactor = 15f;
    [Tooltip("How far the ship can move left/right inside of the PlayerRig.")] [SerializeField] float clampRangeX = 14f;
    [Tooltip("How far the ship can move up inside of the PlayerRig.")] [SerializeField] float clampRangeYTop = 14f;
    [Tooltip("How far the ship can move down inside of the PlayerRig.")] [SerializeField] float clampRangeYBot = -2f;
    [Tooltip("Ratio of ship pitch to vertical position within PlayerRig.")]  [SerializeField] float positionPitchFactor = -5f;
    [Tooltip("Ratio of ship roll to horizontal position within PlayerRig.")] [SerializeField] float positionRollFactor = -2f;
    [Tooltip("Ratio of ship yaw to horizontal position within PlayerRig.")] [SerializeField] float positionYawFactor = -3.5f;
    [Tooltip("Ratio of ship pitch to player vertical input within PlayerRig.")] [SerializeField] float controlPitchFactor = -5f;
    [Tooltip("Ratio of ship roll to player horizontal input within PlayerRig.")] [SerializeField] float controlRollFactor = 5f;
    [Tooltip("Ratio of ship yaw to player horizontal input within PlayerRig.")] [SerializeField] float controlYawFactor = 3.5f;

    [Tooltip("Ship's lasers.")] [SerializeField] ParticleSystem[] lasers;

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

        foreach (ParticleSystem laser in lasers) {
            if (isFiring) {
                if (!laser.isEmitting) {
                    laser.Play();
                }
            } else {
                if (laser.isEmitting) {
                    laser.Stop();
                }
            }
        }

        
    }
}
