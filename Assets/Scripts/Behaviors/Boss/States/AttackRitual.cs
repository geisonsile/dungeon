using UnityEngine;

namespace Behaviors.Boss
{
    public class AttackRitual : State
    {
        private BossController controller;
        private BossHelper helper;
        
        public AttackRitual(BossController controller) : base("AttackRitual")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Atacou com o ritual");
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