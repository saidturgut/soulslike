using System.Collections.Generic;
using UnityEngine;

public class LS
{
    public enum Language
    {
        English,

        Turkish

    }
    public static Language language = Language.English;

    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedTR;

    public static bool isInit;

    public static CSVLoader csvLoader;

    public static void Init()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        localisedEN = csvLoader.GetDictionaryValues("en");
        localisedTR = csvLoader.GetDictionaryValues("tr");

        isInit = true;
    }

    public static string LV(string key)
    {
        if (!isInit) { Init(); }

        string value = key;

        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Turkish:
                localisedTR.TryGetValue(key, out value);
                break;
        }

        return value;
    }
}
