using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {
	public Vector3 speed;

	void Update () {
		//rotate at given speed. Speed is multiplied by deltaTime to achieve framerate independent rotation
		transform.Rotate(speed * Time.deltaTime, Space.Self);
	}
}
