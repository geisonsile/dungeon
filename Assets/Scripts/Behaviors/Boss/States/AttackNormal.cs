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

            var staffTop = controller.staffTop;

            var projectile = Object.Instantiate(controller.fireballPrefab, staffTop.position, staffTop.rotation);

            var projectileCollision = projectile.GetComponent<ProjectileCollision>();
            projectileCollision.attacker = controller.gameObject;
            projectileCollision.damage = controller.attackDamage;

            var player = GameManager.Instance.player;
            var projectileRigidBody = projectile.GetComponent<Rigidbody>();
            
            var vectorToPlayer = (player.transform.position + controller.aimOffset - staffTop.position).normalized;
            var forceVector = staffTop.rotation * Vector3.forward;
            forceVector = new Vector3(forceVector.x, vectorToPlayer.y, forceVector.z);
            forceVector *= controller.attackNormalImpulse;
            projectileRigidBody.AddForce(forceVector, ForceMode.Impulse);

            Object.Destroy(projectile, 30);
        }
    }
}