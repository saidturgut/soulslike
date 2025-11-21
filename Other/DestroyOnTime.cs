using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    [SerializeField] private float time;

    private void Start()
    {
        Invoke("Destroy",time);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
