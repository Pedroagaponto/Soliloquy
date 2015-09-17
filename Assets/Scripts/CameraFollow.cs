using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	Camera camera;
	float cameraScale = 3;
	
	public Transform target;            // The position that that camera will be following.
	public float smoothing = 5f;        // The speed with which the camera will be following.
	Vector3 newPosition;
	Vector3 offset;                     // The initial offset from the target.
	Quaternion rotation;
	
	float distance = 15f;
	float theta = 30f;
	float phi = 140f;
	float camX, camY, camZ;

	void Awake ()
	{
		camera = GetComponent<Camera> ();
	}

	void Start ()
	{
		// Calculate the initial offset.
		//Vector3 cameraToPlayer;
		updateCameraRotation (theta, phi, cameraScale);
		
	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			updateCameraRotation (0f, 120f, 4f);
			print("pressed 1");
		} else if (Input.GetKeyDown (KeyCode.Alpha2)){
			updateCameraRotation (30f, 140f, 3f);
			print("pressed 2");
		} else if (Input.GetKeyDown (KeyCode.Alpha3)){
			updateCameraRotation (60f, 140f, 3f);
			print("pressed 3");
		} else if (Input.GetKeyDown (KeyCode.Alpha4)){
			updateCameraRotation (90f, 180f, 6f);
			print("pressed 4");
		}
		
	}
	
	void FixedUpdate ()
	{
		// Create a postion the camera is aiming for based on the offset from the target.
		Vector3 targetCamPos = target.position + offset;
		
		// Smoothly interpolate between the camera's current position and it's target position.
		
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		
		transform.rotation = Quaternion.Lerp (transform.rotation, rotation, smoothing * Time.deltaTime);

		camera.orthographicSize = Mathf.Lerp (camera.orthographicSize, cameraScale, smoothing * Time.deltaTime);;
	}
	
	public void updateCameraRotation (float theta, float phi, float cameraScale)
	{
		this.theta = theta;
		this.phi = phi;
		this.cameraScale = cameraScale;
		
		camX = distance * Mathf.Cos(theta * Mathf.PI / 180f) * Mathf.Sin(phi * Mathf.PI / 180f);
		camZ = distance * Mathf.Cos(theta * Mathf.PI / 180f) * Mathf.Cos(phi * Mathf.PI / 180f);
		camY = distance * Mathf.Sin(theta * Mathf.PI / 180f);
		
		newPosition = new Vector3(camX, camY, camZ);

		offset = newPosition + Vector3.up;

		rotation = Quaternion.Euler(new Vector3(theta, phi-180f, 0));
		
	}
	
}