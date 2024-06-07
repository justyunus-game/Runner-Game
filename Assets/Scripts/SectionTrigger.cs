using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject roadSection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(roadSection,new Vector3(0, 0, 70f), Quaternion.identity);
        }
    }
}
