using UnityEngine;

namespace Behaviors
{
    public class Hurt : State
    {
        private MeleeCreatureController controller;
        private MeleeCreatureHelper helper;

        private float timePassed;


        public Hurt(MeleeCreatureController controller) : base("Hurt")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
            
            timePassed = 0;

            controller.thisLife.isVulnerable = false;

            controller.thisAnimator.SetTrigger("tHurt");
        }

        public override void Exit()
        {
            base.Exit();

            controller.thisLife.isVulnerable = true;
        }

        public override void Update()
        {
            base.Update();

            timePassed += Time.deltaTime;

            if(timePassed >= controller.hurtDuration)
            {
                if (controller.thisLife.IsDead())
                    controller.stateMachine.ChangeState(controller.DeadState);
                else
                    controller.stateMachine.ChangeState(controller.idleState);
                
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