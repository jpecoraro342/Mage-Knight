using UnityEngine;
using System.Collections;

public class MainCameraFollow : MonoBehaviour 
{
	public Transform target;
	public float smoothing = 10f;
	
	Vector3 offset;
	
	void Start() 
	{
		//Creates initial offset
		offset = transform.position - target.position;
	}
	
	//We used fixedupdate to move the camera since we are following a physics object
	void FixedUpdate() 
	{
		Vector3 targetCamPos = target.position + offset;
		
		//Lerp is a smoothing movement
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
