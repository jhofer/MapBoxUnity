namespace Mapbox.Examples
{
	using UnityEngine;
	using System.Collections.Generic;

	public class HighlightFeature : MonoBehaviour
	{
		private List<Color> _original = new List<Color>();
		private Color _highlight = Color.red;
		private List<Material> _materials = new List<Material>();


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

		void Start() {

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
			foreach (var item in _materials)
			{
				item.color = _highlight;
			}
		}

		public void OnMouseExit()
		{
			for (int i = 0; i < _materials.Count; i++)
			{
				_materials[i].color = _original[i];
			}
		}
	}
}