using UnityEngine;

namespace Behaviors.Boss
{
    public class AttackNormal : State
    {
        private BossController controller;
        private BossHelper helper;
        
        public AttackNormal(BossController controller) : base("AttackNormal")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Atacou com o normal");
            controller.stateMachine.ChangeState(controller.idleState);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
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