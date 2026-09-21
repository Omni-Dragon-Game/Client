using JetBrains.Annotations;
using Mod;
using Mod.XMAP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Networking;

public partial class ModFunc
{
    public void LoadGame()
    {
        if (!loadedMusic)
        {
            InitMusic();
            loadedMusic = true;
        }
        Time.timeScale = 1.0f;
        listSkillsAuto.Clear();
        listItemAuto.Clear();
        int value = Rms.loadRMSInt("isHighFps");
        isHighFps = value == -1 || value == 1;
        ChangeFPSTarget();
    }

    public static string EncodeStringToByteArrayString(string inputString, string key)
    {
        byte[] encodedBytes = EncodeToBytes(inputString, key);
        string byteArrayString = BitConverter.ToString(encodedBytes).Replace("-", "");

        return string.Join("-", SplitByLength(byteArrayString, 2));
    }

    static byte[] EncodeToBytes(string inputString, string key)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] encodedBytes = new byte[inputBytes.Length];

        for (int i = 0; i < inputBytes.Length; i++)
        {
            encodedBytes[i] = (byte)(inputBytes[i] ^ keyBytes[i % keyBytes.Length]);
        }

        return encodedBytes;
    }

    static string[] SplitByLength(string str, int length)
    {
        int strLength = str.Length;
        int numSegments = (strLength + length - 1) / length;
        string[] segments = new string[numSegments];

        for (int i = 0; i < numSegments; i++)
        {
            int startIndex = i * length;
            int segmentLength = Math.min(length, strLength - startIndex);
            segments[i] = str.Substring(startIndex, segmentLength);
        }

        return segments;
    }

    public static string DecodeByteArrayString(string byteArrayString, string key)
    {
        try
        {

            string[] hexValues = byteArrayString.Split('-');
            string concatenatedHex = string.Join("", hexValues);
            byte[] encodedBytes = new byte[concatenatedHex.Length / 2];

            for (int i = 0; i < encodedBytes.Length; i++)
            {
                encodedBytes[i] = Convert.ToByte(concatenatedHex.Substring(i * 2, 2), 16);
            }

            string decodedString = DecodeToString(encodedBytes, key);
            return decodedString;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    static string DecodeToString(byte[] encodedBytes, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] decodedBytes = new byte[encodedBytes.Length];

        for (int i = 0; i < encodedBytes.Length; i++)
        {
            decodedBytes[i] = (byte)(encodedBytes[i] ^ keyBytes[i % keyBytes.Length]);
        }

        return Encoding.UTF8.GetString(decodedBytes);
    }

    public static void Log(string text)
    {
        if (!isDebugEnable)
        {
            return;
        }
        Debug.Log(text);
    }

    public static void LogSlow(string text, long millis)
    {
        if (!isDebugEnable)
        {
            return;
        }
        if (mSystem.currentTimeMillis() - lastTimeLog > millis)
        {
            lastTimeLog = mSystem.currentTimeMillis();
            Debug.Log(text);
        }
    }

    public static void WriteLog(string message)
    {
        if (!isDebugEnable)
        {
            return;
        }
        try
        {
            string logFileName = "log_" + DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

            FileStream fileStream = new(logFileName, FileMode.OpenOrCreate);
            // 
            StreamWriter streamWriter = new(fileStream);
            streamWriter.WriteLine(DateTime.Today.ToString("HH:mm:ss") + ": " + message);
            streamWriter.Flush();
            streamWriter.Close();
        }
        catch (Exception e)
        {
            Log(e.Message);
        }
    }

    public static string DecodeByteArrayString(string byteArrayString)
    {
        try
        {
            string[] hexValues = byteArrayString.Split('-');
            string concatenatedHex = string.Join("", hexValues);
            byte[] encodedBytes = new byte[concatenatedHex.Length / 2];

            for (int i = 0; i < encodedBytes.Length; i++)
            {
                encodedBytes[i] = Convert.ToByte(concatenatedHex.Substring(i * 2, 2), 16);
            }

            string decodedString = DecodeToString(encodedBytes, 69.ToString());
            return decodedString;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public void ChangeFPSTarget()
    {
        Rms.saveRMSInt("isHighFps", isHighFps ? 1 : 0);
        if (isHighFps)
        {
            Application.targetFrameRate = 60;
        }
        else
        {
            Application.targetFrameRate = 30;
        }
    }

    public static Npc GetNpcByTempId(int tempId)
    {
        for (int i = 0; i < GameScr.vNpc.size(); i++)
        {
            Npc npc = (Npc)GameScr.vNpc.elementAt(i);
            if (npc.template.npcTemplateId == tempId)
            {
                return npc;
            }
        }
        return null;
    }

    private static IEnumerator LoadFile(string fullPath)
    {
        string fileUri = "file://" + fullPath;

        using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(fileUri, AudioType.OGGVORBIS);
        www.certificateHandler = new BypassCertificateHandler();

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            AudioClip temp = DownloadHandlerAudioClip.GetContent(www);
            musics.Add(temp);
        }
    }

    public static void InitMusic()
    {
        int fromRms = Rms.loadRMSInt("musicSize");
        musicCount = (fromRms == -1 ? 0 : fromRms);
        for (int i = 0; i < musicCount; i++)
        {
            string fullPath = Rms.GetiPhoneDocumentsPath() + "/music_" + i + ".ogg";
            if (File.Exists(fullPath))
            {
                CoroutineRunner.Instance.RunCoroutine(LoadFile(fullPath));
            }
            else
            {
                Debug.LogWarning("File does not exist: " + fullPath);
            }
        }
    }

}
