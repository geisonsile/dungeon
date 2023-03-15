using System.Collections;
using UnityEngine;

namespace Behaviors.MeleeCreature
{
    public class Attack : State
    {
        private MeleeCreatureController controller;
        private MeleeCreatureHelper helper;
        private float endAttackCooldown;
        private IEnumerator attackCoroutine;

        public Attack(MeleeCreatureController controller) : base("Attack")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            endAttackCooldown = controller.attackDuration;

            attackCoroutine = ScheduleAttack();
            controller.StartCoroutine(attackCoroutine);

            controller.thisAnimator.SetTrigger("tAttack");
        }

        public override void Exit()
        {
            base.Exit();

            if(attackCoroutine != null)
            {
                controller.StopCoroutine(attackCoroutine);
            }
        }

        public override void Update()
        {
            base.Update();

            //Face Player
            helper.FacePlayer();

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

        private IEnumerator ScheduleAttack()
        {
            yield return new WaitForSeconds(controller.damageDelay);
            PerformAttack();
        }

        private void PerformAttack()
        {
            var origin = controller.transform.position;
            var direction = controller.transform.rotation * Vector3.forward;
            var radius = controller.attackRadius;
            var maxDistance = controller.attackSphereRadius;

            //controller.drawSphereAt = origin + direction * radius;
            var attackPosition = direction * radius + origin;
            var layerMask = LayerMask.GetMask("Player");
            Collider[] colliders = Physics.OverlapSphere(attackPosition, maxDistance, layerMask);
            
            foreach (var collider in colliders)
            {
                var hitObject = collider.gameObject;
                
                var hitLifeScript = hitObject.GetComponent<LifeScript>();
                if (hitLifeScript != null)
                {
                    var attacker = controller.gameObject;
                    var attackDamage = controller.attackDamage;
                    hitLifeScript.InflictDamage(attacker, attackDamage);
                }   
            }
        }
    }
}