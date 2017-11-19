namespace Mapbox.Unity.MeshGeneration.Modifiers
{
	using Mapbox.Unity.MeshGeneration.Data;
    using UnityEngine;
    using Mapbox.Unity.MeshGeneration.Components;
    
    [CreateAssetMenu(menuName = "Mapbox/Modifiers/Collider Modifier")]
    public class ColliderModifier : GameObjectModifier
    {
        [SerializeField]
        private ColliderType _colliderType;

		public override void Run(FeatureBehaviour fb, UnityTile tile)
        {
            switch (_colliderType)
            {
                case ColliderType.BoxCollider:
                    fb.gameObject.AddComponent<BoxCollider>().isTrigger=true;
                    break;
                case ColliderType.MeshCollider:
                    var collider =  fb.gameObject.AddComponent<MeshCollider>();
                    
                    //fb.gameObject.AddComponent<Rigidbody>();


                    break;
                case ColliderType.SphereCollider:
                    fb.gameObject.AddComponent<SphereCollider>();
                    break;
                default:
                    break;
            }
        }


        public enum ColliderType
        {
            None,
            BoxCollider,
            MeshCollider,
            SphereCollider
        }

    }
}
