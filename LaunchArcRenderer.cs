using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaunchArcRenderer : MonoBehaviour {

	LineRenderer lr;

	public float velocity;
	public float angle;
	public int resolution = 10;

	float g; //force of gravity on the y axis
	float radianAngle;

	void Awake(){
		lr = GetComponent<LineRenderer> ();
		g = Mathf.Abs (Physics2D.gravity.y);
	}

	void OnValidate(){
		//check that lr is not null and that the game is playing
		if (lr != null && Application.isPlaying){
			RenderArc ();
		}
	}

	// Use this for initialization
	void Start () {
		RenderArc ();
	}

	//populating the LineRender with the appropriate settings
	void RenderArc(){
		lr.SetVertexCount (resolution + 1);
		lr.SetPositions (CalculateArcArray ());
	}

	//create an array of Vector 3 positions for arc
	Vector3[] CalculateArcArray(){
		Vector3[] arcArray = new Vector3[resolution + 1];

		radianAngle = Mathf.Deg2Rad * angle;
		float maxDistance = (velocity * velocity * Mathf.Sin (2 * radianAngle)) / g;

		for (int i = 0; i <= resolution; i++) {
			float t = (float)i / (float)resolution;
			arcArray [i] = CalculateArcPoint (t, maxDistance);
		}

		return arcArray;

	}

	//calculate height and distance of each vertex
	Vector3 CalculateArcPoint(float t, float maxDistance){
		float x = t * maxDistance;
		float y = x * Mathf.Tan (radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos (radianAngle) * Mathf.Cos (radianAngle)));
		return new Vector3 (x, y);
	}
	

}
