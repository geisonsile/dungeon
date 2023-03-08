using UnityEngine;

namespace Behaviors.Boss
{
    public class Follow : State
    {
        private BossController controller;
        private BossHelper helper;

        private readonly float attackAttemptInterval = 0.5f;
        private float attackAttemptCooldown = 0f;
        private readonly float targetUpdateInterval = 0.5f;
        private float targetUpdateCooldown = 0;
        private float ceaseFollowCooldown = 0;

        public Follow(BossController controller) : base("Follow")
        {
            this.controller = controller;
            helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            attackAttemptCooldown = 0;
            targetUpdateCooldown = 0;
            ceaseFollowCooldown = 0;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            //Update destination
            if ((targetUpdateCooldown -= Time.deltaTime) <= 0)
            {
                targetUpdateCooldown = targetUpdateInterval;
                var player = GameManager.Instance.player;
                var playerPosition = player.transform.position;
                controller.thisAgent.SetDestination(playerPosition);
            }

            //Attempt to attack with Ritual
            if ((attackAttemptCooldown -= Time.deltaTime) <= 0)
            {
                attackAttemptCooldown = attackAttemptInterval;

                //Ritual
                var distanceToPlayer = helper.GetDistanceToPlayer();
                var isCloseEnoughToRitual = distanceToPlayer <= controller.distanceToRitual;
                if (isCloseEnoughToRitual)
                {
                    controller.stateMachine.ChangeState(controller.AttackRitualState);
                    return;
                }
            }

            //Attempt to cease follow (super e normal)
            if ((ceaseFollowCooldown -= Time.deltaTime) <= 0)
            {
                State newState = helper.HasLowHealth() ? controller.AttackSuperState : controller.AttackNormalState;
                controller.stateMachine.ChangeState(newState);
                return;
            }

        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}