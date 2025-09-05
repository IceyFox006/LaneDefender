using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private void FixedUpdate()
    {
        Despawn();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().Play("Explode");
        if (collision.GetComponent<EntityController>() != null)
        {
            EntityController collisionEC = collision.GetComponent<EntityController>();
            if (collisionEC.EntityType.Alligence == Enums.EntityAlligence.Enemy)
            {
                collisionEC.DamageEntity();
                collisionEC.StunEntity();
                GameController.Instance.UpdateScore(1);
            }
        }
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
       StartCoroutine(DestroyAfterAnimation());
    }
    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
    private void Despawn()
    {
        if (!GetComponent<Renderer>().isVisible)
            Destroy(gameObject);
    }
}
