namespace Mapbox.Examples
{
	using UnityEngine;
	using System.Collections.Generic;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.Utilities;
    using Mapbox.Utils;

    public class HighlightFeature : MonoBehaviour, ISelectable
	{
		private List<Color> _original = new List<Color>();
		private Color _highlight = Color.blue;
        private Color _select = Color.red;
		private List<Material> _materials = new List<Material>();
        private Vector2d location;
        private bool hover;

        void MakeRed(MeshRenderer[] comps)
        {
            foreach (MeshRenderer comp in comps) { 
                foreach (var item in comp.materials)
                {
                    _materials.Add(item);
                    _original.Add(item.color);
                 }
             }
        } 
         
         
        void SearchMeshRenderes(Transform comp)
        {
           
            foreach(Transform child in comp)
            {
                SearchMeshRenderes(child);
            }
        }

        void Update()
        {
            bool isSelected = GameManager.instance.IsBuildingSelected(location);
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

		void Start() {
            location = MapUtils.GetGeoLocation(GetComponent<Transform>());
            MakeRed(GetComponents<MeshRenderer>());

               
         
            //  SearchMeshRenderes(GetComponent<Transform>());
            //foreach(var th in GetComponentsInChildren())
            // {
            //     MakeRed()
            // }

            // foreach (var item in comp.materials)
            // {
            //     _materials.Add(item);
            //     _original.Add(item.color);
            // }
            // {
            //     foreach (var item in comp.materials)
            //     {
            //         _materials.Add(item);
            //         _original.Add(item.color);
            //     }
            // }
            // foreach (var comp in GetComponentsInChildren<MeshRenderer>(true))
            // {
            //     foreach (var item in comp.materials)
            //     {
            //         _materials.Add(item);
            //         _original.Add(item.color);
            //     }
            // }

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
            GameManager.instance.SelectBuilding(this.location);
        }
    }
}