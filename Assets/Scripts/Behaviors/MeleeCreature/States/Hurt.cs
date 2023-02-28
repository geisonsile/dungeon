namespace Behaviors
{
    public class Hurt : State
    {
        private MeleeCreatureController controller;
        private MeleeCreatureHelper helper;


        public Hurt(MeleeCreatureController controller) : base("Hurt")
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