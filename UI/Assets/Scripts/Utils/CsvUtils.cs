using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvUtils
{
    private List<string[]> ReadCsvFile(string path)
    {
        Debug.Log("ReadCsvFile");

        TextAsset csvFile = Resources.Load(path) as TextAsset; // ResourcesにあるCSVファイルを格納
        StringReader reader = new StringReader(csvFile.text); // TextAssetをStringReaderに変換
        List<string[]> csvData = new List<string[]>();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1行ずつ読み込む
            csvData.Add(line.Split(',')); // csvDataリストに追加する
        }

        return csvData;
    }

    public void ReadNovelCsvFile()
    {
        Debug.Log("ReadNovelCsvFile");
        var csvData = ReadCsvFile("NovelData/Csv/NovelData");

        for (int i = 0; i < csvData.Count; i++) // csvDataリストの条件を満たす値の数（全て）
        {
            // データの表示
            Debug.Log($"storyNum ：{csvData[i][0]} ,characterName ：{csvData[i][1]} ,message ：{csvData[i][2]} ,placeImage ：{csvData[i][3]} ,characterImage ：{csvData[i][4]}");
        }
    }
}
