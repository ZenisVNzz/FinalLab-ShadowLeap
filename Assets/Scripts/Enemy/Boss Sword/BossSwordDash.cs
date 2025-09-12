using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossSwordDash : IAbility
{
    private Rigidbody2D rb;
    private GhostTrail ghostTrail;
    private MonoBehaviour monoRunner;
    private Transform player => GameObject.FindGameObjectWithTag("Player").transform;

    private float dashDuration = 0.1f; 
    private float dashSpeed = 16f;

    public BossSwordDash(Rigidbody2D rb, GhostTrail ghostTrail, MonoBehaviour runner)
    {
        this.rb = rb;
        this.ghostTrail = ghostTrail;
        this.monoRunner = runner;
    }

    public void ProcessAbility()
    {
        monoRunner.StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        ghostTrail.StartTrail();

        Vector2 directionToPlayer = ((Vector2)player.position - rb.position).normalized;
        Vector2 dashDirection = -directionToPlayer;
        SFXManager.instance.PlaySFX("200002");

        float timer = dashDuration;
        while (timer > 0f)
        {
            Vector2 newPos = rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime;
            newPos.y = rb.position.y;
            rb.MovePosition(newPos);

            timer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.2f);

        ghostTrail.StopTrail();
    }
}
