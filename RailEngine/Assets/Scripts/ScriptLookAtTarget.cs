using UnityEngine;
using System.Collections;

/// <summary>
/// Author:Andrew Seba
/// Description: Controls the transform to look at a specified target and return
/// </summary>
public class ScriptLookAtTarget : MonoBehaviour {

    [Tooltip("How fast the camera will rotate to the target.")]
    public float[] rotateSpeed;

    [Tooltip("Place the target object for the camera to look at.")]
    public GameObject[] targets;

    [Tooltip("How long you will lock on target.")]
    public float[] lockTime;

	[Tooltip("How long you will free look for.")]
	public float facingTime;

    Quaternion startRotation;
    
    //void Update(){
    //    if (Input.GetButtonDown ("Jump")) {
    //        Activate(rotateSpeed, targets, lockTime);
    //    }
    //}
	
	public void Activate(float[] pRotateSpeed, GameObject[] pTargets, float[]pLockTimes)
	{
		rotateSpeed = pRotateSpeed;
		targets = pTargets;
		lockTime = pLockTimes;
		startRotation = transform.rotation;
		StopCoroutine ("ReturnLook");
		StartCoroutine("LookAtTarget");
	}
	
	public void RunFreeLook(float pfacingTime)
	{
		facingTime = pfacingTime;
		startRotation = transform.rotation;
		StopCoroutine ("ReturnLook");
		StartCoroutine("FacingFreeLook");
	}

    IEnumerator LookAtTarget()
    {
        for (int i = 0; i < targets.Length; i++ )
        {
            float timeElapsed = 0.0f;

            //while (timeElapsed < lockTime[i])
            while(timeElapsed * rotateSpeed[i] < 1)
            {
                timeElapsed += Time.deltaTime;
                Quaternion neededRotation = Quaternion.LookRotation(targets[i].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, neededRotation, Time.deltaTime * rotateSpeed[i]);
				yield return null;
            }

            timeElapsed = 0.0f;
            while(timeElapsed < lockTime[i])
            {
                timeElapsed += Time.deltaTime;
                transform.LookAt(targets[i].transform.position);
                yield return null;
            }
        }
        StartCoroutine("ReturnLook");
    }

    IEnumerator ReturnLook()
    {
        while (transform.forward != Vector3.forward)
        {

            transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, rotateSpeed[rotateSpeed.Length - 1]);

            yield return null;
        }

    }
	
	// @author: Nathan Boehning (Project 2)
	// Modified for Project 3 by: Craig Broskow
	IEnumerator FacingFreeLook()
	{
		float elapsedTime = 0f; // keeps track of elapsed time to continue free looking
		float xRotation = 0f;
		float yRotation = 0f;
		float lookSensitivity = 0.5f;
		float curXRotation = transform.rotation.x;
		float curYRotation = transform.rotation.y;
		float lookSmoothDamp = 0.1f;
		float xRotationV = 0;
		float yRotationV = 0;

		// Make cursor invisible and locked in the middle of the screen
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		while (elapsedTime < facingTime) // iterate through the loop for facingTime seconds
		{
			
			// Increment the yRotation using mouse input multiplied by the mouse sensitivity
			yRotation += Input.GetAxis("Mouse X")*lookSensitivity;
			
			// Decrement the xRotation using mouse input. (incrementing creates an inverted effect)
			xRotation -= Input.GetAxis("Mouse Y")*lookSensitivity;
			
			// Lock the rotation so camera can only look straight up or straight down without going circular
			xRotation = Mathf.Clamp(xRotation, -90, 90);
			
			// Smooth the rotation to prevent screen tearing
			curXRotation = Mathf.SmoothDamp(curXRotation, xRotation, ref xRotationV, lookSmoothDamp);
			curYRotation = Mathf.SmoothDamp(curYRotation, yRotation, ref yRotationV, lookSmoothDamp);
			
			// Set the rotation of the camera to new rotation
			transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

			// Increment the elapsed time by the change in time
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		StartCoroutine("ReturnLook");
	} // end method FacingFreeLook
}
