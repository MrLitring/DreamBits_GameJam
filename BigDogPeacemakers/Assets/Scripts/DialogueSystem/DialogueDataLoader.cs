using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class DialogueDataLoader
{
    private static readonly string NoneData = "NONE.DialogueDataLoader()";
    private static readonly string FileName;
    private static readonly string TableName;



    public static List<DialogueData> GetDatas(string query)
    {
        List<DialogueData> data = new List<DialogueData>();
        Divide(query, out string name, out int[] ids);

        foreach(int id in ids)
            data.Add(DialogueDataRead(name, id));

        return data;
    }



    private static DialogueData DialogueDataRead(string name_id, object value_id)
    {
        DialogueData data = new DialogueData(name_id, value_id.ToString());

        // Здесь должно быть чтение из XML или DataBase пока не решено. Чисто загатовка

        return data;
    }

    private static void Divide(string query, out string name, out int[] indexs)
    {
        query = query.Replace(" ", "");
        name = NoneData;
        indexs = null;

        string[] parts = query.Split(":");
        name = parts[0];

        parts = parts[1].Split(",");
        string value = "";


        int len = parts.Length;

        for(int i = 0; i < len; i++)
        {
            string[] parts_1 = parts[i].Split("-");

            if (parts_1.Length == 2)
                value += Format(int.Parse(parts_1[0]), int.Parse(parts_1[1]));
            else
                value += parts_1[0];

            if (i != len - 1) value += ",";
        }

        indexs = Format(value);

    }

    private static int[] Format(string value)
    {
        string[] parts = value.Split(",");
        int[] result = new int[parts.Length];

        for(int i = 0; i < parts.Length; i++)
        {
            result[i] = int.Parse(parts[i]);
        }

        return result;
    }

    private static string Format(int from, int to)
    {
        string text = from.ToString();
        for (int i = from + 1; i < to; i++)
            text += $", {i}";

        return text;
    }
}
