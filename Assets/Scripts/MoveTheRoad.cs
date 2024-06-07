using UnityEngine;

public class MoveTheRoad : MonoBehaviour
{
    
    void Update()
    {
        transform.position += new Vector3(0, 0, -6) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
