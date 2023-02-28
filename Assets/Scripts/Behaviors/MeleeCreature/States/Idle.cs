using UnityEngine;

namespace Behaviors
{
    public class Idle : State
    {
        private MeleeCreatureController controller;
        private MeleeCreatureHelper helper;

        public float searchCoolDown;

        public Idle(MeleeCreatureController controller) : base("Idle")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            searchCoolDown = controller.targetSearchInterval;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            searchCoolDown -= Time.deltaTime;
            if(searchCoolDown <= 0)
            {
                searchCoolDown = controller.targetSearchInterval;

                //Search for player
                if(helper.isPlayerOnSight())
                {
                    controller.stateMachine.ChangeState(controller.followState);
                    return;
                }
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