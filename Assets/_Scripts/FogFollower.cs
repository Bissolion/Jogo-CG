using UnityEngine;

public class FogFollower : MonoBehaviour
{
    [Header("Alvo a ser seguido")]
    public Transform target; 
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
        }
    }
}