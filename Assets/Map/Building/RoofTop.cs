using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoofTop: MonoBehaviour {
    private GameObject go;
    private Vector3 point;

    public float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }
    public float VolumeOfMesh(Mesh mesh)
    {
        float volume = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        int length = mesh.triangles.Length;
        for (int i = 0; i< length; i += 3)
     {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        return Mathf.Abs(volume);
    }

    // Use this for initialization
    void Start () {
        GameObject go = GameObject.Find("Prefabs");
        var prefabs = (PrefabContainer)go.GetComponent<PrefabContainer>();
        var prefab = prefabs.roofDeco;

        var collider = GetComponent<Collider>();
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        //var vertices = mesh.vertices;
        //for (var i = 0; i < vertices.Length; i++)
        //{
        //    var spawnPoint = transform.TransformPoint(vertices[i]);
        //    var q = Random.Range(0, vertices.Length);
        //    spawnPoint = transform.TransformPoint(vertices[q]);
        //    Instantiate(prefab, spawnPoint, transform.rotation);
        //}



        float randomX = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        float randomZ = Random.Range(collider.bounds.min.z, collider.bounds.max.z);
        float centerX = collider.bounds.center.x;
        float centerY = collider.bounds.center.y;
        float centerZ = collider.bounds.center.z;

        var pos = new Vector3(centerX + randomX, centerY + collider.bounds.max.y + 5f, centerZ + randomZ);
        var spawnPoint = transform.TransformPoint(collider.bounds.center);

        this.go = Instantiate(prefab, collider.bounds.center, transform.rotation);
        //RaycastHit hit;
        //if (Physics.Raycast(new Vector3(randomX, collider.bounds.max.y + 5f, randomZ), -Vector3.up, out hit))
        //{
        //   // if (hit.point.y >= collider.bounds.max.y-0.2) {
        //        Instantiate(prefab, hit.point, transform.rotation);
        //   // }
        //}
        //else
        //{
        //    // the raycast didn't hit, maybe there's a hole in the terrain?
        //}




        //var vertices = GetComponent<MeshFilter>().mesh.b;
        //for (var i = 0; i < vertices.Length; i++)
        //{
        //    var spawnPoint = transform.TransformPoint(vertices[i]);
        //    var q = Random.Range(0, vertices.Length);
        //    spawnPoint = transform.TransformPoint(vertices[q]);
        //    Instantiate(prefab, spawnPoint, transform.rotation);
        //}

    }
	
	// Update is called once per frame
	void Update () {
        GameObject go = GameObject.Find("Prefabs");
        var prefabs = (PrefabContainer)go.GetComponent<PrefabContainer>();
        var prefab = prefabs.roofDeco;

        var collider = GetComponent<Collider>();
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        //var vertices = mesh.vertices;
        //for (var i = 0; i < vertices.Length; i++)
        //{
        //    var spawnPoint = transform.TransformPoint(vertices[i]);
        //    var q = Random.Range(0, vertices.Length);
        //    spawnPoint = transform.TransformPoint(vertices[q]);
        //    Instantiate(prefab, spawnPoint, transform.rotation);
        //}

        if(this.point == null)
        {

            float randomX = Random.Range(mesh.bounds.min.x, mesh.bounds.max.x);
            float randomZ = Random.Range(mesh.bounds.min.z, mesh.bounds.max.z);
            float centerX = mesh.bounds.center.x;
            float centerY = mesh.bounds.center.y;
            float centerZ = mesh.bounds.center.z;
            var pos = new Vector3(randomX, mesh.bounds.max.y + 1, randomZ);
            var minHeight = (mesh.bounds.center.y + mesh.bounds.max.y) / 2;
            var lowestP = new Vector3(randomX, minHeight / 2, randomZ);
            var spawnPoint = transform.TransformPoint(pos);
            var lowestPoint = transform.TransformPoint(lowestP);

            //  this.go.transform.position = spawnPoint;
            RaycastHit hit;
            if (Physics.Raycast(spawnPoint, -Vector3.up, out hit))
            {
                if (hit.point.y > lowestPoint.y)
                {
                    this.point = hit.point;
                    this.go.transform.position = this.point;

                }
            }
        }

        

      
      
        
    }
}
