using UnityEngine;
using System.Collections;

public class EmissionControl : MonoBehaviour {
	public float colorChangeFrequency = 0.5f;

	private MeshRenderer meshRenderer;
	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<MeshRenderer>();
		StartCoroutine(ChangeColor());
	}

	/**
	 * Changes the material emissive color at defined frequency
	 */
	IEnumerator ChangeColor () {
		while(true) {
			meshRenderer.material.SetColor("_EmissionColor",new Color(Random.value, Random.value, Random.value));
			yield return new WaitForSeconds(colorChangeFrequency);
		}
	}
}
