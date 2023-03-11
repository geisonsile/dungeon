using System.Collections;
using UnityEngine;

namespace Behaviors.Boss
{
    public class AttackRitual : State
    {
        private BossController controller;
        private BossHelper helper;

        private float endAttackCooldown;

        public AttackRitual(BossController controller) : base("AttackRitual")
        {
            this.controller = controller;
            helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            endAttackCooldown = controller.attackRitualDuration;
            Debug.Log("Entrou no " + this.name);

            controller.thisAnimator.SetTrigger("tAttackRitual");

            helper.StartStateCoroutine(ScheduleAttack(controller.attackRitualDelay));
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

            var gameObject = Object.Instantiate(
                controller.ritualPrefab, 
                controller.staffBottom.position, 
                controller.ritualPrefab.transform.rotation
            );
            Object.Destroy(gameObject, 6);

            if(helper.GetDistanceToPlayer() <= controller.distanceToRitual)
            {
                var playerLife = GameManager.Instance.player.GetComponent<LifeScript>();
                playerLife.InflictDamage(controller.gameObject, controller.attackDamage);
            }
        }
    }
}