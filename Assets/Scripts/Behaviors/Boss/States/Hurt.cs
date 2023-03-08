using UnityEngine;

namespace Behaviors.Boss
{
    public class Hurt : State
    {
        private BossController controller;
        private BossHelper helper;
        
        public Hurt(BossController controller) : base("Hurt")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
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