using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public GameObject ai;
    public GameObject Trainedai;
    public List<AI> listAI = new List<AI>();
    public List<TrainedAI> listTrainedAI = new List<TrainedAI>();
    public string filename;
    public Dictionary<int, int> pers;
    public List<int> randPos;
    void Start()
    {

    }

    public void Spawn()
    {
        if (ai != null)
        {
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    this.transform.position = new Vector3(-j - 12, this.transform.position.y, -25 + i);
                    Debug.Log($"Create new obj{i * 20 + j}");
                    GameObject newAi = Instantiate(ai, this.transform.position, Quaternion.identity);
                    newAi.GetComponent<NavMeshAgent>().avoidancePriority = i * 20 + j;
                    listAI.Add(newAi.GetComponent<AI>());
                }
            }
            Debug.Log($"SPAWN");
        }
    }

    public void SpawnTrained()
    {
        if (Trainedai != null)
        {
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    this.transform.position = new Vector3(-j - 12, this.transform.position.y, -25 + i);
                    GameObject newAi = Instantiate(Trainedai, this.transform.position, Quaternion.identity);
                    newAi.GetComponent<NavMeshAgent>().avoidancePriority = i * 20 + j;
                    newAi.GetComponent<TrainedAI>().randPos = this.randPos;
                    listTrainedAI.Add(newAi.GetComponent<TrainedAI>());
                }
            }
            Debug.Log($"SPAWN");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Spawn();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SpawnTrained();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("TRAIN");
            filename = Application.dataPath + "/train.csv";
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("P0," +
                         "P1," +
                         "P2," +
                         "P3," +
                         "P4," +
                         "P5," +
                         "P6," +
                         "P7," +
                         "P8," +
                         "P9," +
                     "Points," +
                     "Finish");
            tw.Close();
            tw = new StreamWriter(filename, true);
            foreach (var item in listAI)
            {
                if (item.good <= 0) item.good = 0.0f;
                tw.WriteLine($"{Math.Round(item.goods[0], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[1], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[2], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[3], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[4], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[5], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[6], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[7], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[8], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[9], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                                 $"{Math.Round(item.good, 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             item.isFinish.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            }
            tw.Close();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reset");
            listAI.Clear();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("TEST");
            filename = Application.dataPath + "/test.csv";
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("P0," +
                         "P1," +
                         "P2," +
                         "P3," +
                         "P4," +
                         "P5," +
                         "P6," +
                         "P7," +
                         "P8," +
                         "P9," +
                     "Points," +
                     "Finish");
            tw.Close();
            tw = new StreamWriter(filename, true);
            foreach (var item in listAI)
            {
                if (item.good <= 0) item.good = 0.0f;
                tw.WriteLine($"{Math.Round(item.goods[0], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[1], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[2], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[3], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[4], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[5], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[6], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[7], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[8], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[9], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                                 $"{Math.Round(item.good, 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             item.isFinish.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            }
            tw.Close();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("POSX");
            filename = Application.dataPath + "/posx.csv";
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("P0," +
                         "P1," +
                         "P2," +
                         "P3," +
                         "P4," +
                         "P5," +
                         "P6," +
                         "P7," +
                         "P8," +
                         "P9"
                     //"Points," +
                     //"Finish"
                     );
            tw.Close();
            tw = new StreamWriter(filename, true);
            foreach (var item in listAI)
            {
                if (item.good <= 0) item.good = 0.0f;
                tw.WriteLine($"{item.points[0].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[1].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[2].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[3].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[4].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[5].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[6].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[7].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[8].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[9].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}"
                             //$"{Math.Round(item.good, 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             //item.isFinish.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))
                             );
            }
            tw.Close();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("PERS");
            pers = new Dictionary<int, int>();
            randPos = new List<int>();

            filename = Application.dataPath + "/pers.csv";

            string[] lines = File.ReadAllLines(filename);
            string[] keys = lines[0].Split(',');
            string[] values = lines[1].Split(',');

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            for (int i = 0; i < keys.Length; i++)
            {
                dictionary.Add(keys[i], values[i]);
            }

            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                Debug.Log($"{pair.Key}: {pair.Value}");
                pers.Add(int.Parse(pair.Key), int.Parse(pair.Value));
            }

            foreach (KeyValuePair<int, int> pair in pers)
            {
                for (int i = 0; i < pair.Value; i++)
                {
                    randPos.Add(pair.Key);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("TRAIN");
            filename = Application.dataPath + "/train.csv";
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("P0," +
                         "P1," +
                         "P2," +
                         "P3," +
                         "P4," +
                         "P5," +
                         "P6," +
                         "P7," +
                         "P8," +
                         "P9," +
                     "Points," +
                     "Finish");
            tw.Close();
            tw = new StreamWriter(filename, true);
            foreach (var item in listTrainedAI)
            {
                if (item.good <= 0) item.good = 0.0f;
                tw.WriteLine($"{Math.Round(item.goods[0], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[1], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[2], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[3], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[4], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[5], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[6], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[7], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[8], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[9], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                                 $"{Math.Round(item.good, 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             item.isFinish.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            }
            tw.Close();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Reset");
            listTrainedAI.Clear();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("TEST");
            filename = Application.dataPath + "/test.csv";
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("P0," +
                         "P1," +
                         "P2," +
                         "P3," +
                         "P4," +
                         "P5," +
                         "P6," +
                         "P7," +
                         "P8," +
                         "P9," +
                     "Points," +
                     "Finish");
            tw.Close();
            tw = new StreamWriter(filename, true);
            foreach (var item in listTrainedAI)
            {
                if (item.good <= 0) item.good = 0.0f;
                tw.WriteLine($"{Math.Round(item.goods[0], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[1], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[2], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[3], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[4], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[5], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[6], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[7], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[8], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{Math.Round(item.goods[9], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                                 $"{Math.Round(item.good, 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             item.isFinish.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            }
            tw.Close();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("POSX");
            filename = Application.dataPath + "/posx.csv";
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("P0," +
                         "P1," +
                         "P2," +
                         "P3," +
                         "P4," +
                         "P5," +
                         "P6," +
                         "P7," +
                         "P8," +
                         "P9"
                     //"Points," +
                     //"Finish"
                     );
            tw.Close();
            tw = new StreamWriter(filename, true);
            foreach (var item in listTrainedAI)
            {
                if (item.good <= 0) item.good = 0.0f;
                tw.WriteLine($"{item.points[0].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[1].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[2].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[3].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[4].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[5].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[6].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[7].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[8].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             $"{item.points[9].x.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}"
                             //$"{Math.Round(item.good, 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," +
                             //item.isFinish.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))
                             );
            }
            tw.Close();
        }
    }
}
