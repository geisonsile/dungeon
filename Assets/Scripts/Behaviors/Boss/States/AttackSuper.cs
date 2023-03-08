using UnityEngine;

namespace Behaviors.Boss
{
    public class AttackSuper : State
    {
        private BossController controller;
        private BossHelper helper;
        
        public AttackSuper(BossController controller) : base("AttackSuper")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Atacou com o super");
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