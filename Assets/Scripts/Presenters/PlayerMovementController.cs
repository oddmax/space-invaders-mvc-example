using Data;
using DefaultNamespace.StaticData;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Presenters
{
	public class PlayerMovementController : MonoBehaviour
    {
        [Inject] 
        private PlayerConfig playerConfig;
        
        public GameObject shot;         // bullet prefab
        public Transform shotSpawn;     // the turret (bullet spawn location)
        public Rigidbody myRigidbody;   // reference to rigitbody
        
        public float smoothing = 5;     // this value is used for smoothing movement
        
        private float nextFire = 0.0f;
        private Vector3 smoothDirection;// used to smooth out mouse and touch control

        // Use this for initialization
        void Start()
        {
            myRigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            Vector3 pos = Input.mousePosition;

            pos.z = Camera.main.transform.position.y + 1;
            pos = Camera.main.ScreenToWorldPoint(pos);
            Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            Vector2 currentPosition = new Vector3(pos.x, pos.z);
            Vector3 directionRaw = pos - origin;

            Vector3 direction = directionRaw.normalized;

            smoothDirection = Vector3.MoveTowards(smoothDirection, direction, smoothing);

            direction = smoothDirection;
            Vector3 movement = new Vector3(direction.x, 0, direction.z);
            myRigidbody.velocity = movement * playerConfig.movementSpeed;
            
            var movementBoundary = playerConfig.movementBoundary;
            transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, movementBoundary.xMin, movementBoundary.xMax),
                0.0f,
                Mathf.Clamp(transform.position.z, movementBoundary.zMin, movementBoundary.zMax)
            );

            myRigidbody.rotation = Quaternion.Euler(0, 0, myRigidbody.velocity.x * -playerConfig.movementTiltFactor);
        }

        void Update()
        {
            // Should we fire a bullet?
            if ((Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Space)) && Time.time > nextFire)
            {
                nextFire = Time.time + playerConfig.fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
        }
	}
}