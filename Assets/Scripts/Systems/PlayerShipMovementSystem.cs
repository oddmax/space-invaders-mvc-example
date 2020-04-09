using DefaultNamespace.StaticData;
using UnityEngine;

namespace DefaultNamespace.Presenters
{
	public class PlayerShipMovementSystem
    {
        private PlayerConfig playerConfig;
        
        private float smoothing = 5;     // this value is used for smoothing movement
        private Vector3 smoothDirection;// used to smooth out mouse and touch control

        public PlayerShipMovementSystem(PlayerConfig playerConfig)
        {
            this.playerConfig = playerConfig;
        }
        
        public void Update(PlayerView playerView)
        {
            var shipRigidbody = playerView.Rigidbody;
            var shipTransform = playerView.transform;
            Vector3 pos = Input.mousePosition;

            pos.z = Camera.main.transform.position.y + 1;
            pos = Camera.main.ScreenToWorldPoint(pos);
            Vector3 origin = new Vector3(shipTransform.position.x, shipTransform.position.y, shipTransform.position.z);

            Vector3 directionRaw = pos - origin;

            Vector3 direction = directionRaw.normalized;

            smoothDirection = Vector3.MoveTowards(smoothDirection, direction, smoothing);

            direction = smoothDirection;
            Vector3 movement = new Vector3(direction.x, 0, direction.z);
            shipRigidbody.velocity = movement * playerConfig.movementSpeed;
            
            var movementBoundary = playerConfig.movementBoundary;
            shipTransform.position = new Vector3
            (
                Mathf.Clamp(shipTransform.position.x, movementBoundary.xMin, movementBoundary.xMax),
                0.0f,
                Mathf.Clamp(shipTransform.position.z, movementBoundary.zMin, movementBoundary.zMax)
            );

            shipRigidbody.rotation = Quaternion.Euler(0, 0, shipRigidbody.velocity.x * -playerConfig.movementTiltFactor);
        }
    }
}