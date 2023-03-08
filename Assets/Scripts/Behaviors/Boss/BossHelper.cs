using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviors.Boss
{
    public class BossHelper : MonoBehaviour
    {
        private BossController controller;

        public BossHelper(BossController controller)
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

        public bool HasLowHealth()
        {
            var life = controller.thisLife;
            var lifeRate = (float) life.health / (float) life.maxHealth;
            return lifeRate <= controller.lowHealthThreshold;
        }
    }
}
