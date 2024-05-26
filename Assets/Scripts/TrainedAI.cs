using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
using UnityEngine.UIElements;

[System.Serializable]
public class TrainedAI : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    public int cur_p;
    public NavMeshAgent agent;
    [Range(0, 100)] public float good;
    public List<float> goods = new List<float>();
    public float temp_good;
    public int isFinish;
    public Dictionary<GameObject, bool> walls = new Dictionary<GameObject, bool>();
    public List<int> randPos = new List<int>();
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            points.Add(new Vector3(randPos[Random.Range(0, randPos.Count)], 1, transform.position.z));
        }
        good = 0;
        cur_p = 0;
        isFinish = 0;
        temp_good = 0;
        for (int i = 0; i < 10; i++) { goods.Add(0); }
        agent = this.GetComponent<NavMeshAgent>();
        agent.destination = points[cur_p];
    }

    void Update()
    {
        if (agent.remainingDistance < 0.2)
        {
            goods[cur_p] = temp_good;
            temp_good = 0;
            cur_p++;
            if (cur_p >= points.Count)
            {
                //good -= 0.1f;
                this.gameObject.SetActive(false);
            }
            else
                agent.destination = points[cur_p];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            //good += 0.1f;
            goods[cur_p] = temp_good;
            isFinish = 1;
            this.gameObject.SetActive(false);
        }

        if (other.tag == "Good")
        {
            bool isgood = true;

            if (walls.ContainsKey(other.gameObject))
            {
                isgood = false;
                if (walls[other.gameObject])
                {
                    walls[other.gameObject] = false;
                    temp_good -= 0.03f;
                    good -= 0.03f;
                }
                else
                {
                    walls[other.gameObject] = true;
                    temp_good += 0.01f;
                    good += 0.01f;
                }
            }
            if (isgood)
            {
                walls.Add(other.gameObject, true);
                temp_good += 0.05f;
                good += 0.05f;
            }
        }

    }

}
