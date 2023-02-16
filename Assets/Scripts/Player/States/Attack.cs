using UnityEngine;

public class Attack : State 
{

    private PlayerController controller;

    public int stage = 1;
    private float stateTime;
    private bool firstFixedUpdate;

    
    public Attack(PlayerController controller) : base("Attack") 
    {
        this.controller = controller;
    }

    public override void Enter() 
    {
        base.Enter();

        if(stage <=0 || stage > controller.attackStages)
        {
            controller.stateMachine.ChangeState(controller.idleState);
            return;
        }

        stateTime = 0;
        firstFixedUpdate = true;

        controller.thisAnimator.SetTrigger("tAttack" + stage);

        controller.swordHitbox.SetActive(true);
    }

    public override void Exit() 
    {
        base.Exit();
        controller.swordHitbox.SetActive(false);

    }

    public override void Update() 
    {
        base.Update();

        //Switch to Attack
        if (controller.AttemptToAttack())
        {
            return;
        }

        stateTime += Time.deltaTime;
        
        //Exit after time
        if(IsStageExpired())
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

        if(firstFixedUpdate)
        {
            firstFixedUpdate = false;

            controller.RotateBodyToFaceInput(1);

            var inpulseValue = controller.attackStageImpulses[stage - 1];
            var inpulseVector = controller.thisRigidbody.rotation * Vector3.forward;
            inpulseVector *= inpulseValue;
            controller.thisRigidbody.AddForce(inpulseVector, ForceMode.Impulse);
        }
    }

    public bool CanSwitchStages()
    {
        var isLastState = stage == controller.attackStages;
        var stageDuration = controller.attackStageDurations[stage - 1];
        var stageMaxInterval = isLastState ? 0 : controller.attackStageMaxIntervals[stage - 1];
        var maxStageDuration = stageDuration + stageMaxInterval;

        return !isLastState && stateTime >= stageDuration && stateTime <= maxStageDuration;
    }

    public bool IsStageExpired()
    {
        var isLastState = stage == controller.attackStages;
        var stageDuration = controller.attackStageDurations[stage - 1];
        var stageMaxInterval = isLastState ? 0 : controller.attackStageMaxIntervals[stage - 1];
        var maxStageDuration = stageDuration + stageMaxInterval;

        return stateTime > maxStageDuration;
    }

}