using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    public int position;
    public People peopleObject;
    public bool isExist = false;

    public void Spawn()
    {
        isExist = true;
        People p = Instantiate(peopleObject, transform.position, Quaternion.identity);
        p.position = position;
    }
}
