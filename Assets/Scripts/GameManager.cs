using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public GameObject ai;
    public GameObject Trainedai;
    public List<AI> listAI = new List<AI>();
    public List<TrainedAI> listTrainedAI = new List<TrainedAI>();
    public string filename;
    public Dictionary<int, int> pers;
    public List<int> randPos;
    public int countMod;
    public int countOfPoints;
    void Start()
    {

    }

    public void Spawn()
    {
        if (ai != null)
        {
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < countMod; j++)
                {
                    this.transform.position = new Vector3(-j - 12, this.transform.position.y, -25 + i);
                    GameObject newAi = Instantiate(ai, this.transform.position, Quaternion.identity);
                    newAi.GetComponent<NavMeshAgent>().avoidancePriority = i * countMod + j;
                    newAi.GetComponent<AI>().pCount = countOfPoints;
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
                for (int j = 0; j < countMod; j++)
                {
                    this.transform.position = new Vector3(-j - 12, this.transform.position.y, -25 + i);
                    GameObject newAi = Instantiate(Trainedai, this.transform.position, Quaternion.identity);
                    newAi.GetComponent<NavMeshAgent>().avoidancePriority = i * countMod + j;
                    newAi.GetComponent<TrainedAI>().randPos = this.randPos;
                    newAi.GetComponent<TrainedAI>().pCount = countOfPoints;
                    listTrainedAI.Add(newAi.GetComponent<TrainedAI>());
                }
            }
            Debug.Log($"SPAWN");
        }
    }

    public string RowBuilder(int N)
    {
        string res = "";

        for (int i = 0; i < N; i++)
        {
            res += $"P{i},";
        }

        return res + "Points," +
                     "Finish";
    }

    public string PosXRowBuilder(int N)
    {
        string res = "";

        for (int i = 0; i < N; i++)
        {
            if (i == N - 1)
                res += $"P{i}";
            else
                res += $"P{i},";
        }

        return res;
    }

    public string PointsBuilder(int N, AI item)
    {
        string res = "";
        for (int i = 0; i < N; i++)
        {
            res += $"{Math.Round(item.goods[i], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + ",";
        }

        return res + $"{Math.Round(item.good, 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," + item.isFinish.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
    }

    public string PointsTrainedBuilder(int N, TrainedAI item)
    {
        string res = "";
        for (int i = 0; i < N; i++)
        {
            res += $"{Math.Round(item.goods[i], 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + ",";
        }

        return res + $"{Math.Round(item.good, 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + "," + item.isFinish.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
    }

    public string PosXBuilder(int N, AI item)
    {
        string res = "";
        for (int i = 0; i < N; i++)
        {
            if (i == N - 1)
                res += $"{Math.Round(item.points[i].x, 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}";
            else
                res += $"{Math.Round(item.points[i].x, 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + ",";
        }

        return res;
    }

    public string PosXTrainedBuilder(int N, TrainedAI item)
    {
        string res = "";
        for (int i = 0; i < N; i++)
        {
            if (i == N - 1)
                res += $"{Math.Round(item.points[i].x, 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}";
            else
                res += $"{Math.Round(item.points[i].x, 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"))}" + ",";
        }

        return res;
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
            tw.WriteLine(RowBuilder(countOfPoints));
            tw.Close();
            tw = new StreamWriter(filename, true);
            foreach (var item in listAI)
            {
                if (item.good <= 0) item.good = 0.0f;
                tw.WriteLine(PointsBuilder(countOfPoints, item));
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
            tw.WriteLine(RowBuilder(countOfPoints));
            tw.Close();
            tw = new StreamWriter(filename, true);
            foreach (var item in listAI)
            {
                if (item.good <= 0) item.good = 0.0f;
                tw.WriteLine(PointsBuilder(countOfPoints, item));
            }
            tw.Close();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("POSX");
            filename = Application.dataPath + "/posx.csv";
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine(PosXRowBuilder(countOfPoints));
            tw.Close();
            tw = new StreamWriter(filename, true);
            foreach (var item in listAI)
            {
                if (item.good <= 0) item.good = 0.0f;
                tw.WriteLine(PosXBuilder(countOfPoints,item));
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
            tw.WriteLine(RowBuilder(10));
            tw.Close();
            tw = new StreamWriter(filename, true);
            foreach (var item in listTrainedAI)
            {
                if (item.good <= 0) item.good = 0.0f;
                tw.WriteLine(PointsTrainedBuilder(countOfPoints, item));
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
            tw.WriteLine(RowBuilder(countOfPoints));
            tw.Close();
            tw = new StreamWriter(filename, true);
            foreach (var item in listTrainedAI)
            {
                if (item.good <= 0) item.good = 0.0f;
                tw.WriteLine(PointsTrainedBuilder(countOfPoints, item));
            }
            tw.Close();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("POSX");
            filename = Application.dataPath + "/posx.csv";
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine(PosXRowBuilder(countOfPoints));
            tw.Close();
            tw = new StreamWriter(filename, true);
            foreach (var item in listTrainedAI)
            {
                if (item.good <= 0) item.good = 0.0f;
                tw.WriteLine(PosXTrainedBuilder(countOfPoints, item));
            }
            tw.Close();
        }
    }
}
