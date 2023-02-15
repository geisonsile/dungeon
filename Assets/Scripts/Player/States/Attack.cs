using UnityEngine;

public class Attack : State 
{

    private PlayerController controller;

    public int stage = 1;
    private float stateTime;

    
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
        controller.thisAnimator.SetTrigger("tAttack" + stage);
    }

    public override void Exit() 
    {
        base.Exit();
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