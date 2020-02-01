using UnityEngine;
using System.Collections;

public class MouseLookTest : MonoBehaviour {

	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
    public float invertX = 1;
    public float invertY = 1;
	public bool clampVerticalRotation = true;
	public float MinimumX = -90F;
	public float MaximumX = 90F;
	public bool smooth;
	public float smoothTime = 5f;
	public Transform camera;
    


	private Quaternion characterTargetRot;
	private Quaternion cameraTargetRot;
    private Vector3 upright;
    private Quaternion uprightTargetRot;
    private bool lockCursor = true;
    private bool MLMode = true;


	public void Awake()
	{
        //Sets the character and camera's target rotation to their current one when the script starts running.
		cameraTargetRot = camera.localRotation;
        setCursorLock(lockCursor);
	}
    
	private void Update()
	{
        //Updates every frame. If the player's in mouselook mode, it runs the function that does the work and checks to see if they want to lock/unlock the mouse.
        if (MLMode)
        {
            if (lockCursor) lookRotation();
            updateCursorLock();
        }
	}


	private void lookRotation()
	{
        //This is what runs the mouselook, moving the character and camera transforms to follow the mouse. Called every Update.
		float yRot = Input.GetAxis("Mouse X") * XSensitivity * invertX;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity * invertY;

		//characterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
		cameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

		


        //turn the camera
		if(smooth)
		{
			camera.localRotation = Quaternion.Slerp (camera.localRotation, cameraTargetRot,
				smoothTime * Time.deltaTime);
		}
		else
		{
			camera.localRotation = characterTargetRot;
		}

        cameraTargetRot *= Quaternion.Euler(0f, yRot, 0f);

        //then turn the camera
        if (clampVerticalRotation)
            cameraTargetRot = clampRotationAroundXAxis(cameraTargetRot);

        if (smooth)
        {
            camera.localRotation = Quaternion.Slerp(camera.localRotation, cameraTargetRot,
                smoothTime * Time.deltaTime);
        }
        else
        {
            camera.localRotation = cameraTargetRot;
        }
        upright = camera.localEulerAngles;
        upright.z = 0;
        uprightTargetRot = Quaternion.Euler(upright);
        camera.localRotation = uprightTargetRot;

	}

    private Quaternion clampRotationAroundXAxis(Quaternion q)
    {
        //This is used by lookRotation to clamp the axis to vertical up and down so the player can't do terrible things with the camera.
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    private void updateCursorLock()
	{
        //this checks every update if the player hit escape to unlock the mouse, or clicked back in.
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            setCursorLock(false);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            setCursorLock(true);
        }
	}


    private void setCursorLock(bool value)
    {
        //this is called internally to do the locking and unlocking when you want to lock or unlock the cursor.
        lockCursor = value;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void setMLMode(bool value)
    {
        //This is an external function to be called by other scripts to change into and out of mouselook mode entirely.
        MLMode = value;
        setCursorLock(MLMode);
    }

    //These two are for inverting the X and Y movement.
    public void invertXLook()
    {
        invertX *= -1;
    }
    public void invertYLook()
    {
        invertY *= -1;
    }
}
