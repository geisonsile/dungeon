using UnityEngine;

namespace Behaviors.MeleeCreature
{
    public class Follow : State
    {
        private MeleeCreatureController controller;
        private MeleeCreatureHelper helper;

        private readonly float updateInterval = 1;
        private float updateCoolDown;
        private float ceaseFollowCoolDown;


        public Follow(MeleeCreatureController controller) : base("Follow")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            updateCoolDown = 0;
            ceaseFollowCoolDown = controller.ceaseFollowInterval;
        }

        public override void Exit()
        {
            base.Exit();

            //Stop following
            controller.thisAgent.ResetPath();
        }

        public override void Update()
        {
            base.Update();

            //Update destination
            if ((updateCoolDown -= Time.deltaTime) <= 0)
            {
                updateCoolDown = updateInterval;
                var player = GameManager.Instance.player;
                var playerPosition = player.transform.position;
                controller.thisAgent.SetDestination(playerPosition);
            }

            // Cease Follow
            if((ceaseFollowCoolDown -=  Time.deltaTime) <= 0)
            {
                if (!helper.isPlayerOnSight())
                {
                    controller.stateMachine.ChangeState(controller.idleState);
                    return;
                }
            }

            //Attempt to attack
            if(helper.GetDistanceToPlayer() <= controller.distanceToAttack)
            {
                controller.stateMachine.ChangeState(controller.AttackState);
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
