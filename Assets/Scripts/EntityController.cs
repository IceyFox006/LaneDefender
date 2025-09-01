using System.Collections;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] private EntitySO _entityType;
    private int currentHP;

    private Rigidbody2D entityRB2D;

    public EntitySO EntityType { get => _entityType; set => _entityType = value; }
    public Rigidbody2D EntityRB2D { get => entityRB2D; set => entityRB2D = value; }

    private void Start()
    {
        currentHP = _entityType.MaxHP;

        entityRB2D = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EntityController>() != null)
        {
            EntityController collisionEC = collision.GetComponent<EntityController>();
            if (_entityType.Alligence == Enums.EntityAlligence.Player && collisionEC.EntityType.Alligence == Enums.EntityAlligence.Enemy)
            {
                DamageEntity();
                GameController.Instance.UpdatePlayerHPUI(currentHP, _entityType.MaxHP);
                collisionEC.KillEntity();
            }
        }
    }
    public void StartMovement(Transform startPoint)
    {
        entityRB2D.AddForce(-startPoint.right * _entityType.Speed, ForceMode2D.Impulse);
    }
    public void StunEntity()
    {
        entityRB2D.linearVelocity = Vector2.zero;
        StartCoroutine(StunWait());
    }
    public IEnumerator StunWait()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("HERE");
        //entityRB2D.linearVelocity = -transform.right * _entityType.Speed;
        StartMovement(transform);
    }
    public void DamageEntity()
    {
        currentHP -= 1;
        if (currentHP <= 0)
        {
            if (_entityType.Alligence == Enums.EntityAlligence.Player)
                KillPlayer();
            else
                KillEntity();
        }
    }
    private void KillPlayer()
    {
        Time.timeScale = 0f;
        GameController.Instance.GameOverCanvas.SetActive(true);
    }
    private void KillEntity()
    {
        Destroy(gameObject);
    }
}
