using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {

  }


  void OnTriggerStay(Collider other)
  {
   
	 
      if (other.gameObject.tag.Contains("Building"))
      {
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", Color.red);
      }

    
  }


  // Update is called once per frame
  void Update()
  {

  }
}
