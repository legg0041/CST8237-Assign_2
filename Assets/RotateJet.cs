using UnityEngine;
using System.Collections;

/// <summary>
/// Rotates the jets on the y axis to simulate them being animated
/// </summary>
public class RotateJet : MonoBehaviour {
    //speed to rotate the jets
    private int _jetSpeed = 1000;
	
	// Update is called once per frame
	void Update () {
        //rotate the jet to make it look animated
        transform.Rotate(0, Time.deltaTime * _jetSpeed, 0);
    }
}
