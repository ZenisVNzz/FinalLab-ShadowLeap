using System.Collections;
using UnityEngine;

public class BossSwordDash2 : IAbility
{
    private Rigidbody2D rb;
    private GhostTrail ghostTrail;
    private MonoBehaviour monoRunner;
    private Transform player => GameObject.FindGameObjectWithTag("Player").transform;

    private float dashDuration = 0.05f;
    private float dashSpeed = 16f;

    public BossSwordDash2(Rigidbody2D rb, GhostTrail ghostTrail, MonoBehaviour runner)
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

        Vector2 behindPlayer = (Vector2)player.position + (directionToPlayer * 1.5f); 

        float timer = dashDuration;
        while (timer > 0f)
        {
            rb.MovePosition(Vector2.Lerp(rb.position, behindPlayer, dashSpeed * Time.fixedDeltaTime));
            timer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.position = behindPlayer;
        rb.position = new Vector2(rb.position.x, 16.534f);
        ghostTrail.StopTrail();
    }
}
