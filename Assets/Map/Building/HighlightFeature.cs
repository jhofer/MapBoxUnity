namespace Mapbox.Examples
{
    using UnityEngine;
    using System.Collections.Generic;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.Utilities;
    using Mapbox.Utils;
    using System.Linq;
    using System.Collections;

    public class HighlightFeature : MonoBehaviour, ISelectable
    {
        private List<Color> _original = new List<Color>();
        private Color _highlight = Color.blue;
        private Color _select = Color.red;
        private List<Material> _materials = new List<Material>();
        private Vector2d mylocation;
        private bool hover;
     



        void MakeRed(MeshRenderer[] comps)
        {
            foreach (MeshRenderer comp in comps)
            {
                foreach (var item in comp.materials)
                {
                    _materials.Add(item);
                    _original.Add(item.color);
                }
            }
        }


        void SearchMeshRenderes(Transform comp)
        {

            foreach (Transform child in comp)
            {
                SearchMeshRenderes(child);
            }
        }

        void Update()
        {
           
            bool isSelected = GameManager.instance.IsBuildingSelected( MapUtils.GetGeoLocation(GetComponent<MeshRenderer>().bounds.center));
            if (this.hover)
            {
                foreach (var item in _materials)
                {
                    item.color = _highlight;
                }
            }
            else if (isSelected)
            {
                foreach (var item in _materials)
                {
                    item.color = _select;
                }
            }

            else
            {
                for (int i = 0; i < _materials.Count; i++)
                {
                    _materials[i].color = _original[i];
                }
            }
        }



        bool IsTouching(GameObject go, GameObject other)
        {
            var vertices = go.GetComponent<MeshFilter>().mesh.vertices.Select(v => go.transform.TransformPoint(v));


            var otherVertices = other.GetComponent<MeshFilter>().mesh.vertices.Select(v => other.transform.TransformPoint(v));
            foreach (var v in otherVertices)
            {

                foreach (var ov in vertices)
                {
                    if (Vector3.Distance(v, ov) <= 0.5) return true;
                }

            }
            return false;

        }

        void FindNearBuildings(GameObject go, ref HashSet<Vector2d> closeColliders)
        {
            var location = MapUtils.GetGeoLocation(go.GetComponent<MeshRenderer>().bounds.center);
            closeColliders.Add(location);

            var buildings = GameObject.FindGameObjectsWithTag("Building");
                    
            foreach (var item in buildings)
            {
                var otherLocation = MapUtils.GetGeoLocation(item.GetComponent<MeshRenderer>().bounds.center);
                if (!closeColliders.Contains(otherLocation))
                {
                    if (IsTouching(go, item))
                    {
                        FindNearBuildings(item, ref closeColliders);
                    }
                }

               
            }

         
        }



        void Start()
        {
            //MeshCollider mc = GetComponent<MeshCollider>();
            //mc.sharedMesh = ThickerMeshUsingNormals(mc.sharedMesh, 1);
            //   gameObject.GetComponent<MeshCollider>().transform += new Vector3(0.2f, 0.2f, 0.2f);
            
            
            
            MakeRed(GetComponents<MeshRenderer>());
            tag = "Building";

        }

        public void OnMouseEnter()
        {

            this.hover = true;


        }

        public void OnMouseExit()
        {
            this.hover = false;

        }

        public void Select()
        {
            Debug.Log("click building");
            var closeBuildings = new HashSet<Vector2d>();

            FindNearBuildings(gameObject, ref closeBuildings);
        
            GameManager.instance.SelectBuildings(closeBuildings);
        }
    }
}
