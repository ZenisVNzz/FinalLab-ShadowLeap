using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class BossSwordAbilityHandler : AbilityDependency, IAbility
{
    private BossSwordMove bossSwordMove;
    private BossSwordDash bossSwordDash;
    private BossSwordDash2 bossSwordDash2;
    private BossSwordAttack1 bossSwordAttack1;
    private BossSwordAttack2 bossSwordAttack2;
    private BossSwordAttack4 bossSwordAttack4;
    private MonoBehaviour monoRunner;

    private bool actionDone = false;
    private bool active = false;

    public BossSwordAbilityHandler(MonoBehaviour monoRunner)
    {
        this.monoRunner = monoRunner;
    }

    public override void Initialize(AbilityManager abilityManager)
    {
        base.Initialize(abilityManager);
        bossSwordMove = abilityManager.GetAbility<BossSwordMove>();
        bossSwordDash = abilityManager.GetAbility<BossSwordDash>();
        bossSwordDash2 = abilityManager.GetAbility<BossSwordDash2>();
        bossSwordAttack1 = abilityManager.GetAbility<BossSwordAttack1>();
        bossSwordAttack2 = abilityManager.GetAbility<BossSwordAttack2>();
        bossSwordAttack4 = abilityManager.GetAbility<BossSwordAttack4>();
    }

    public void SetActive(bool active)
    {
        this.active = active;
    }

    public void ProcessAbility()
    {
        if (!isRunning)
        {
            monoRunner.StartCoroutine(ActionLoop());
            isRunning = true;
        }
    }

    private bool isRunning = false;

    private IEnumerator ActionLoop()
    {
        while (true)
        {
            int action = Random.Range(0, 10);

            if (action < 3) Attack1();       
            else if (action < 8) Attack4();
            else if (action < 6) Attack2(); 
            else if (action == 8) DashBack();          
            else DashToBehindPlayer(); 

            yield return new WaitUntil(() => actionDone);

            actionDone = false;
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void Attack1()
    {
        bossSwordMove.SetMove(true);
        if (bossSwordMove.isCloseToPLayer)
        {
            bossSwordAttack1.ProcessAbility();
            bossSwordMove.SetMove(false);
        }
        actionDone = true;
    }

    private void Attack2()
    {
        bossSwordMove.SetMove(true);
        if (bossSwordMove.isCloseToPLayer)
        {
            bossSwordAttack2.ProcessAbility();
            bossSwordMove.SetMove(false);
        }
        actionDone = true;
    }


    private void Attack4()
    {
        bossSwordMove.SetMove(true);
        if (bossSwordMove.isCloseToPLayer)
        {
            bossSwordAttack4.ProcessAbility();
            bossSwordMove.SetMove(false);
        }
        actionDone = true;
    }

    private void DashBack()
    {
        bossSwordDash.ProcessAbility();
        actionDone = true;
    }

    private void DashToBehindPlayer()
    {
        bossSwordDash2.ProcessAbility();
        actionDone = true;
    }
}
