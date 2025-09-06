using UnityEngine;

public class ButtonsMethods : MonoBehaviour
{
    public void EnableGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void DisableGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
