using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class CsvWriter : MonoBehaviour
{
    string fileName = "";
    public GameManager gm;
    public PlayerController pc;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        pc = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        fileName = Application.dataPath + "/report.csv";
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.enterKey.isPressed)
        {
            WriteCSV();
        }
    }

    public void WriteCSV()
    {

        // bool as to append or not
        // since 1st time opening file, we want to overwrite
        TextWriter writer = new StreamWriter(fileName, false);
        writer.WriteLine("Round, Time, Basic Pickups, Special Pickups, Total Pickups");
        writer.Close();

        writer = new StreamWriter(fileName, true);
        string pickupsData = "";
        for (int i = 0; i < gm.getTotalRounds(); ++i)
        {
            pickupsData += ((i + 1) + "," + pc.getTimeAt(i) + "," + gm.getBasicAt(i) + "," + gm.getSpecialAt(i) + "," + (gm.getBasicAt(i) + gm.getSpecialAt(i)) + "\n");
        }
        writer.WriteLine(pickupsData);
        writer.WriteLine("Game Statistics: ");
        writer.WriteLine("Win, Lose");
        float losePercent = gm.getTotalDeaths() * 100f / gm.getTotalRounds();
        writer.WriteLine(100 - losePercent + "%" + "," + losePercent + "%");
        writer.Close();
    }
}
