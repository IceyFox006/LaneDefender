using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private void FixedUpdate()
    {
        Despawn();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
       Destroy(gameObject);
    }
    private void Despawn()
    {
        if (!GetComponent<Renderer>().isVisible)
            Destroy(gameObject);
    }
}
