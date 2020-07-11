using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDistancer : MonoBehaviour
{
    public float m_minDistanceToPlayer;

    public GameObject planet;

    public List<GameObject> m_ObjsToAvoid = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        KeepDistance();
    }

    private void KeepDistance()
    {
        for (int i = 0; i < m_ObjsToAvoid.Count; i++)
        {
            if (Vector3.Distance(this.gameObject.transform.position, m_ObjsToAvoid[i].transform.position) < m_minDistanceToPlayer)
            {

                transform.position =
                    (this.transform.position - m_ObjsToAvoid[i].transform.position).normalized
                    * m_minDistanceToPlayer + m_ObjsToAvoid[i].transform.position;
            }
        }
    }
}
