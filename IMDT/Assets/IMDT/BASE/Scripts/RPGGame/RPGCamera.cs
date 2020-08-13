using UnityEngine;
using System.Collections;

public class RPGCamera : MonoBehaviour
{
    public Transform character;
    public Transform cam;

    public Vector3 pivotOffset = new Vector3(0.0f, 1.0f, 0.0f);       // Offset to repoint the camera.
    public Vector3 camOffset = new Vector3(0.4f, 0.5f, -2.0f);       // Offset to relocate the camera related to the player position.
    public float smooth = 10f;                                         // Speed of camera responsiveness.

    public float angleH = 0;                                          // Float to store camera horizontal angle related to horizontal axis movement.
    public float angleV = 0;                                          // Float to store camera vertical angle related to vertical axis movement.

    public Vector3 relCameraPos;                                      // Current camera position relative to the player.
    public float relCameraPosMag;                                     // Current camera distance to the player.
    public Vector3 smoothPivotOffset;                                 // Camera current pivot offset on interpolation.
    public Vector3 smoothCamOffset;                                   // Camera current offset on interpolation.
    public Vector3 targetPivotOffset;                                 // Camera pivot offset target to iterpolate.
    public Vector3 targetCamOffset;                                   // Camera offset target to interpolate.
    public float defaultFOV;                                          // Default camera Field of View.
    public float targetFOV;                                           // Target camera Field of View.
    public float targetMaxVerticalAngle;

    // Use this for initialization
    void Start()
    {

        

    }

    public void ResetFOV()
    {
        this.targetFOV = defaultFOV;
    }


    // Set camera offsets to custom values.
    public void SetTargetOffsets(Vector3 newPivotOffset, Vector3 newCamOffset)
    {
        targetPivotOffset = newPivotOffset;
        targetCamOffset = newCamOffset;
    }

    // Reset camera offsets to default values.
    public void ResetTargetOffsets()
    {
        targetPivotOffset = pivotOffset;
        targetCamOffset = camOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (character == null) return;




        // Set camera orientation.
        Quaternion camYRotation = Quaternion.Euler(0, angleH, 0);
        Quaternion aimRotation = Quaternion.Euler(-angleV, angleH, 0);
        cam.rotation = aimRotation;

        // Set FOV.
        cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, targetFOV, Time.deltaTime);

        // Test for collision with the environment based on current camera position.
        Vector3 baseTempPosition = character.position + camYRotation * targetPivotOffset;
        Vector3 noCollisionOffset = targetCamOffset;
        for (float zOffset = targetCamOffset.z; zOffset <= 0; zOffset += 0.5f)
        {
            noCollisionOffset.z = zOffset;
            if (DoubleViewingPosCheck(baseTempPosition + aimRotation * noCollisionOffset, Mathf.Abs(zOffset)) || zOffset == 0)
            {
                break;
            }
        }

        // Repostition the camera.
        smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, targetPivotOffset, smooth * Time.deltaTime);
        smoothCamOffset = Vector3.Lerp(smoothCamOffset, noCollisionOffset, smooth * Time.deltaTime);

        cam.position = character.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset;
    }

    public void SetCharacter(Transform _char)
    {
        this.character = _char;
        if (_char == null) return;

        cam = this.transform;

        cam.position = character.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset;
        cam.rotation = Quaternion.identity;

        // Get camera position relative to the player, used for collision test.
        relCameraPos = transform.position - character.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;

        // Set up references and default values.
        smoothPivotOffset = pivotOffset;
        smoothCamOffset = camOffset;
        defaultFOV = cam.GetComponent<Camera>().fieldOfView;
        angleH = character.eulerAngles.y;

        ResetTargetOffsets();
        ResetFOV();
    }

    bool DoubleViewingPosCheck(Vector3 checkPos, float offset)
    {
        float playerFocusHeight = character.GetComponent<CapsuleCollider>().height * 0.75f;
        return ViewingPosCheck(checkPos, playerFocusHeight) && ReverseViewingPosCheck(checkPos, playerFocusHeight, offset);
    }

    // Check for collision from camera to player.
    bool ViewingPosCheck(Vector3 checkPos, float deltaPlayerHeight)
    {
        // Cast target.
        Vector3 target = character.position + (Vector3.up * deltaPlayerHeight);
        // If a raycast from the check position to the player hits something...
        if (Physics.SphereCast(checkPos, 0.2f, target - checkPos, out RaycastHit hit, relCameraPosMag))
        {
            // ... if it is not the player...
            if (hit.transform != character && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                // This position isn't appropriate.
                return false;
            }
        }
        // If we haven't hit anything or we've hit the player, this is an appropriate position.
        return true;
    }

    bool ReverseViewingPosCheck(Vector3 checkPos, float deltaPlayerHeight, float maxDistance)
    {
        // Cast origin.
        Vector3 origin = character.position + (Vector3.up * deltaPlayerHeight);
        if (Physics.SphereCast(origin, 0.2f, checkPos - origin, out RaycastHit hit, maxDistance))
        {
            if (hit.transform != character && hit.transform != transform && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                return false;
            }
        }
        return true;
    }
}
