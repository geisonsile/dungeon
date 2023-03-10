using System.Collections;
using UnityEngine;

namespace Behaviors.Boss
{
    public class AttackNormal : State
    {
        private BossController controller;
        private BossHelper helper;

        private float endAttackCooldown;

        public AttackNormal(BossController controller) : base("AttackNormal")
        {
            this.controller = controller;
            helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            endAttackCooldown = controller.attackNormalDuration;
            Debug.Log("Entrou no " + this.name);

            controller.thisAnimator.SetTrigger("tAttackNormal");

            helper.StartStateCoroutine(ScheduleAttack(controller.attackNormalMagicDelay));
        }

        public override void Exit()
        {
            base.Exit();

            helper.ClearStateCoroutines();
        }

        public override void Update()
        {
            base.Update();

            //End Attack
            if ((endAttackCooldown -= Time.deltaTime) <= 0)
            {
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

        private IEnumerator ScheduleAttack(float delay)
        {
            yield return new WaitForSeconds(delay);

            Debug.Log("Atacou com " + this.name);
        }
    }
}