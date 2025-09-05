using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void StartAnimationClip(AnimationClip animationClip)
    {
        GetComponent<Animator>().Play(animationClip.name);
    }
    public void SpawnProjectile()
    {
        InputController.Instance.SpawnProjectile();
    }
}
