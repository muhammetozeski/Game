using UnityEngine;
namespace TestScripts
{
    public class Turner : MonoBehaviour
    {
        [SerializeField] GameObject _GameObject;
        [SerializeField] Vector3 dir;
        [SerializeField] float speed = 1;
        void Update()
        {
            _GameObject.transform.Rotate(dir * speed);
        }
    }
}