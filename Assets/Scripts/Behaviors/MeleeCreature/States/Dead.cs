namespace Behaviors
{
    public class Dead : State
    {
        private MeleeCreatureController controller;
        private MeleeCreatureHelper helper;


        public Dead(MeleeCreatureController controller) : base("Dead")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            controller.thisLife.isVulnerable = false;

            controller.thisAnimator.SetTrigger("tDead");
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