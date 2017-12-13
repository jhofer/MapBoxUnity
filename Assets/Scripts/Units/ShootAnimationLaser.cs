using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAnimationLaser : MonoBehaviour,IShootingAnimation {
	
	public GameObject Shot1;
	public GameObject Shot2;
	public GameObject Wave;

	// Update is called once per frame
	public void Fire() {


		//Fire
		GameObject s1 = (GameObject)Instantiate(Shot1, this.transform.position, this.transform.rotation);
		s1.GetComponent<BeamParam>().SetBeamParam(this.GetComponent<BeamParam>());

		GameObject wav = (GameObject)Instantiate(Wave, this.transform.position, this.transform.rotation);
		wav.transform.localScale *= 0.25f;
		wav.transform.Rotate(Vector3.left, 90.0f);
		wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;

	}
}
