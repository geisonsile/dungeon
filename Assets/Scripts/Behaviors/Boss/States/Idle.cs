using UnityEngine;

namespace Behaviors.Boss
{
    public class Idle : State
    {
        private BossController controller;
        private BossHelper helper;

        private float stateTime;
        
        public Idle(BossController controller) : base("Idle")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
            stateTime = 0;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            stateTime += Time.deltaTime;

            // Ignore if game is over
            if (GameManager.Instance.isGameOver) return;

            // Switch to Follow
            if (stateTime >= controller.idleDuration)
            {
                controller.stateMachine.ChangeState(controller.followState);
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