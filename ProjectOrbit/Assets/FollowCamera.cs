using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
    public GameObject follow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, follow.transform.position - new Vector3(0,0,100), 0.1f);
	}
}
