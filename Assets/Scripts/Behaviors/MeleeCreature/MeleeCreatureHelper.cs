using UnityEngine;

namespace Behaviors.MeleeCreature
{
    public class MeleeCreatureHelper
    {
        private MeleeCreatureController controller;

        public MeleeCreatureHelper(MeleeCreatureController controller)
        {
            this.controller = controller;
        }

        public float GetDistanceToPlayer()
        {
            var player = GameManager.Instance.player;
            var playerPosition = player.transform.position;
            var origin = controller.transform.position;
            var positionDifference = playerPosition - origin;
            var distance = positionDifference.magnitude;
            
            return distance;
        }

        public bool isPlayerOnSight()
        {
            var player = GameManager.Instance.player;
            var playerPosition = player.transform.position;
            var origin = controller.transform.position;
            var positionDifference = playerPosition - origin;
            var direction = positionDifference.normalized;
            var distance = positionDifference.magnitude;
            var searchRadius = controller.searchRadius;

            //Error: to far
            if (distance > searchRadius)
                return false;

            //Error: Found obstacle
            var layerMask = LayerMask.GetMask("Default", "Player");
            if(Physics.Raycast(origin, direction,out var hitInfo,searchRadius, layerMask))
            {
                if (hitInfo.transform.gameObject != player)
                    return false;
            }

            //All good
            return true;
        }

        public void FacePlayer()
        {
            var transform = controller.transform;
            var player = GameManager.Instance.player;
            var vecToPlayer = player.transform.position - transform.position;
            vecToPlayer.y = 0;
            vecToPlayer.Normalize();
            var desiredRotation = Quaternion.LookRotation(vecToPlayer);
            var newRotation = Quaternion.LerpUnclamped(transform.rotation, desiredRotation, 0.1f);
            transform.rotation = newRotation;
        }
    }
}

