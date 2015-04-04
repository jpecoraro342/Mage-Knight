using UnityEngine;
using System.Collections;

public class TimedObject : MonoBehaviour {
	
	public float objectLife = 1f;
	
	void Start () {
		StartCoroutine(DelayDestroy(objectLife));
	}

	IEnumerator DelayDestroy(float time) {
		yield return null;
		yield return new WaitForSeconds(time);
		
		Destroy(gameObject);
	}
}
