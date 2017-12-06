namespace Mapbox.Examples
{
    using System;
    using UnityEngine;

    public class CameraMovement : MonoBehaviour
	{
		[SerializeField]
		float _panSpeed = 20f;

		[SerializeField]
		float _zoomSpeed = 50f;

		[SerializeField]
		Camera _referenceCamera;

        [SerializeField]
        Transform playerReference;

        [SerializeField]
        float speed;

        [SerializeField]
        float distance;

        Quaternion _originalRotation;
		Vector3 _origin;
		Vector3 _delta;
		public bool _shouldDrag;

        public bool moveToPlayer = true;


		void Awake()
		{
			_originalRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

			if (_referenceCamera == null)
			{
				_referenceCamera = GetComponent<Camera>();
				if (_referenceCamera == null)
				{
					throw new System.Exception("You must have a reference camera assigned!");
				}
			}
		}


        void Start()
        {
            GameManager.instance.mouseManager.OnDrag += Drag;
            GameManager.instance.mouseManager.OnDragStop += DragStop;
        }

        private void DragStop(int button, Vector3 arg2)
        {
            _shouldDrag = false;
        }

        private void Drag(int button, Vector3 mouseOrigin)
        {
            if(button == 0)
             _shouldDrag = true;

        }

        void LateUpdate()
		{
			var x = 0f;
			var y = 0f;
			var z = 0f;

            if (moveToPlayer)
            {
                var target = new Vector3(playerReference.position.x, playerReference.position.y +150, playerReference.position.z -180);
             

                Vector3 targetDir = target - transform.position;
                float step = speed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                transform.position = Vector3.MoveTowards(transform.position, target, step);

                if (Vector3.Distance(transform.position, target) < 0.1f)
                {
                    moveToPlayer = false;
                }
            }


            if (Input.GetMouseButton(0))
            {
                var mousePosition = Input.mousePosition;
                mousePosition.z = _referenceCamera.transform.localPosition.y;
                _delta = _referenceCamera.ScreenToWorldPoint(mousePosition) - _referenceCamera.transform.localPosition;
                _delta.y = 0f;
                if (_shouldDrag == false)
                { 
                  
                    _origin = _referenceCamera.ScreenToWorldPoint(mousePosition);
                }
            }
           

            if (_shouldDrag == true)
            {
                var offset = _origin - _delta;
                offset.y = transform.localPosition.y;
                transform.localPosition = offset;

                
            }

            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            y = -Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
            transform.localPosition += transform.forward * y + (_originalRotation * new Vector3(x * _panSpeed, 0, z * _panSpeed));
        }
    }
}