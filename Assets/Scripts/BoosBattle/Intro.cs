using UnityEngine;

namespace BossBattle
{
    public class Intro : State
    {
        private readonly float duration = 3f;
        private float timeElapsed = 0;


        public Intro() : base("Intro") { }
        public override void Enter()
        {
            base.Enter();

            timeElapsed = 0;

            GameManager.Instance.bossBattleParts.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            timeElapsed += Time.deltaTime;
            if(timeElapsed >= duration)
            {
                var bossBattleHandler = GameManager.Instance.bossBattleHandler;
                bossBattleHandler.stateMachine.ChangeState(bossBattleHandler.stateBattle);
            }

        }
    }
}
