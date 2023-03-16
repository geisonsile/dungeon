using UnityEngine;

namespace Behaviors.MeleeCreature
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

            // Shift object control from NavMesh to Physics
            controller.thisAgent.enabled = false;
            controller.thisRigidbody.isKinematic = false;
        }

        public override void Exit()
        {
            base.Exit();

            controller.thisLife.isVulnerable = true;

            // Shift object control from Physics back to NavMesh
            controller.thisAgent.enabled = true;
            controller.thisRigidbody.isKinematic = true;
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