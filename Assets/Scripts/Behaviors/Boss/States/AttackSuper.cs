using System.Collections;
using UnityEngine;

namespace Behaviors.Boss
{
    public class AttackSuper : State
    {
        private BossController controller;
        private BossHelper helper;

        private float endAttackCooldown;

        public AttackSuper(BossController controller) : base("AttackSuper")
        {
            this.controller = controller;
            helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            endAttackCooldown = controller.attackSuperDuration;
            Debug.Log("Entrou no " + this.name);

            controller.thisAnimator.SetTrigger("tAttackSuper");

            var delayStep = controller.attackSuperMagicDuration / (controller.attackSuperMagicCount - 1);
            for (int i = 0; i < controller.attackSuperMagicCount - 1; i++)
            {
                var delay = controller.attackSuperMagicDelay + delayStep * i;
                helper.StartStateCoroutine(ScheduleAttack(delay));
            }
;        }

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

            var staffTop = controller.staffTop;

            var projectile = Object.Instantiate(controller.energyballPrefab, staffTop.position, staffTop.rotation);

            var player = GameManager.Instance.player;
            var projectileRigidBody = projectile.GetComponent<Rigidbody>();

            var vectorToPlayer = (player.transform.position + controller.aimOffset - staffTop.position).normalized;
            var forceVector = staffTop.rotation * Vector3.forward;
            forceVector = new Vector3(forceVector.x, vectorToPlayer.y, forceVector.z);
            forceVector *= controller.attackSuperImpulse;
            projectileRigidBody.AddForce(forceVector, ForceMode.Impulse);

            Object.Destroy(projectile, 30);
        }
    }
}