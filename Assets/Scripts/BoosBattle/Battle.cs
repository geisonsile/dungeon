using UnityEngine;

namespace BossBattle
{
    public class Battle : State
    {
        LifeScript bossLife;

        public Battle() : base("Battle") 
        {
            var boss = GameManager.Instance.boss;
            bossLife = boss.GetComponent<LifeScript>();
        }
        public override void Enter()
        {
            base.Enter();

            // Update UI
            var gameplayUI = GameManager.Instance.gameplayUI;
            //var boss = GameManager.Instance.boss;
            //var bossLife = boss.GetComponent<LifeScript>();
            gameplayUI.bossHealthBar.SetMaxHealth(bossLife.maxHealth);
            gameplayUI.ToggleBossBar(true);
        }

        public override void Exit()
        {
            base.Exit();

            var gameplayUI = GameManager.Instance.gameplayUI;
            gameplayUI.ToggleBossBar(false);
        }

        public override void Update()
        {
            base.Update();

            // Update UI
            var gameplayUI = GameManager.Instance.gameplayUI;
            //var boss = GameManager.Instance.boss;
            //var bossLife = boss.GetComponent<LifeScript>();
            gameplayUI.bossHealthBar.SetHealth(bossLife.health);
        }
    }
}
