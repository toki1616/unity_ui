using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class CsvUtils
    {
        private static List<string[]> ReadCsvFile(string path)
        {
            //Debug.Log("ReadCsvFile");

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

        public static List<NovelMessage> ReadNovelCsvFile()
        {
            //Debug.Log("ReadNovelCsvFile");
            var csvData = ReadCsvFile("NovelData/Csv/NovelData");
            List<NovelMessage> novelData = new List<NovelMessage>();

            //初めの行はカテゴリの値が入っているため、i=1から始める
            for (int i = 1; i < csvData.Count; i++)
            {
                // データの表示
                //Debug.Log($"csv : storyNum ：{csvData[i][0]}, route ：{csvData[i][1]}, displayRouteConditions : {csvData[i][2]}, message ：{csvData[i][3]}, selectMessage ：{csvData[i][4]}, characterName ：{csvData[i][5]}, characterImagePath : {csvData[i][6]}, backgroundImagePath : {csvData[i][7]}");
                NovelMessage novelMessage = new NovelMessage(storyNum: int.Parse(csvData[i][0]), route: csvData[i][1], displayRouteCondition: csvData[i][2], message: csvData[i][3], selectMessage: csvData[i][4], characterName: csvData[i][5], characterImagePath: csvData[i][6], backgroundImagePath: csvData[i][7]);
                novelData.Add(novelMessage);
            }

            return novelData;
        }
    }
}
