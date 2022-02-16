using UnityEngine;

public class CharacterHasObject : MonoBehaviour
{
    void Update()
    {
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            Camera.main.transform.eulerAngles.y,
            transform.eulerAngles.z);
    }
}
