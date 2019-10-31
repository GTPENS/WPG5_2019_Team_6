using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    public GameObject peopleObject;
    public bool isExist = false;

    public void Spawn()
    {
        isExist = true;
        Instantiate(peopleObject, transform.position, Quaternion.identity);
    }
}
