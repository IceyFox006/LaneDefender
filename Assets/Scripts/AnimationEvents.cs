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
    public void ShowSmoke()
    {
        Debug.Log("Smoke");
        GameController.Instance.PlayerObject.transform.GetChild(0).GetComponent<Animator>().Play("Smoke");
    }
    public void PlaySoundEffect(AudioClip soundEffect)
    {
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent <AudioSource>().clip = soundEffect;
            GetComponent<AudioSource>().Play();
        }
    }
}
