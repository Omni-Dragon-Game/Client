using System;
using System.Security.Cryptography;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using Mod.XMAP;
using UnityEngine;

public partial class Controller
{
    private void createData(myReader d, bool isSaveRMS)
    {
        GameScr.vcData = d.readByte();
        if (isSaveRMS)
        {
            Rms.saveRMS("NR_dart", NinjaUtil.readByteArray(d));
            Rms.saveRMS("NR_arrow", NinjaUtil.readByteArray(d));
            Rms.saveRMS("NR_effect", NinjaUtil.readByteArray(d));
            Rms.saveRMS("NR_image", NinjaUtil.readByteArray(d));
            Rms.saveRMS("NR_part", NinjaUtil.readByteArray(d));
            Rms.saveRMS("NR_skill", NinjaUtil.readByteArray(d));
            Rms.DeleteStorage("NRdata");
        }
    }

    private Image createImage(sbyte[] arr)
    {
        try
        {
            return Image.createImage(arr, 0, arr.Length);
        }
        catch (Exception)
        {
        }
        return null;
    }

    public int[] arrayByte2Int(sbyte[] b)
    {
        int[] array = new int[b.Length];
        for (int i = 0; i < b.Length; i++)
        {
            int num = b[i];
            if (num < 0)
            {
                num += 256;
            }
            array[i] = num;
        }
        return array;
    }

    private void readGetImgByName(Message msg)
    {
        try
        {
            string text = msg.reader().readUTF();
            sbyte nFrame = msg.reader().readByte();
            sbyte[] array = null;
            array = NinjaUtil.readByteArray(msg);
            Image img = createImage(array);
            ImgByName.SetImage(text, img, nFrame);
            if (array != null)
            {
                ImgByName.saveRMS(text, nFrame, array);
            }
        }
        catch (Exception)
        {
        }
    }

    public Image createImage(sbyte[] arr, string key)
    {
        try
        {
            string keyOrigin = DecryptString(key);
            sbyte[] decrypt = Array.ConvertAll(DecryptImage(Array.ConvertAll(arr, a => (byte)a), keyOrigin), a => (sbyte)a);
            return Image.createImage(decrypt, 0, decrypt.Length);
        }
        catch (Exception)
        {
        }
        return null;
    }

    public static string DecryptString(string encryptedText)
    {
        byte[] keyData = System.Text.Encoding.UTF8.GetBytes(ServerListScreen.keyDecryptString);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = keyData;
            aesAlg.Mode = CipherMode.ECB;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            byte[] byteArray = Convert.FromBase64String(encryptedText);
            for (int i = 0; i < 4; i++)
            {
                byteArray = decryptor.TransformFinalBlock(byteArray, 0, byteArray.Length);
            }

            return System.Text.Encoding.UTF8.GetString(byteArray);
        }
    }

    public static byte[] DecryptImage(byte[] dataImage, string key)
    {
        Texture2D encryptedTexture = new Texture2D(1, 1);
        encryptedTexture.LoadImage(dataImage);
        int width = encryptedTexture.width;
        int height = encryptedTexture.height;
        ulong seed = (ulong)JavaHashCode(key);
        Assets.Assets.Scripts.Assembly_CSharp.Random random = new Assets.Assets.Scripts.Assembly_CSharp.Random(seed);

        Color32[] pixels = encryptedTexture.GetPixels32();
        Color32[] rearrangedPixels = new Color32[pixels.Length];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                rearrangedPixels[(height - y - 1) * width + x] = pixels[y * width + x];
            }
        }
        for (int i = 0; i < rearrangedPixels.Length; i++)
        {
            Color32 pixel = rearrangedPixels[i];
            if (pixel.a != 0)
            {
                int red = (pixel.r - random.NextInt(256) + 256) % 256;
                int green = (pixel.g - random.NextInt(256) + 256) % 256;
                int blue = (pixel.b - random.NextInt(256) + 256) % 256;
                rearrangedPixels[i] = new Color32((byte)red, (byte)green, (byte)blue, pixel.a);
            }
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                encryptedTexture.SetPixel(x, y, rearrangedPixels[y * width + (width - 1 - x)]);
            }
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                encryptedTexture.SetPixel(x, height - 1 - y, rearrangedPixels[y * width + x]);
            }
        }
        encryptedTexture.Apply();
        return encryptedTexture.EncodeToPNG();
    }

    private Color ConvertToUnityColor(Color32 color32)
    {
        return new Color32(color32.r, color32.g, color32.b, color32.a);
    }

    public static int JavaHashCode(string str)
    {
        int hash = 0;
        for (int i = 0; i < str.Length; i++) { hash = 31 * hash + str[i]; }
        return hash;
    }

    public static void checkDoneDataUpdate()
    {
        if (!LoginScr.isUpdateMap && !LoginScr.isUpdateSkill && !LoginScr.isUpdateItem)
        {
            try
            {
                if (!GameScr.isLoadAllData)
                {
                    GameScr.gI().readDart();
                    GameScr.gI().readEfect();
                    GameScr.gI().readArrow();
                    GameScr.gI().readSkill();
                }
            }
            catch (Exception exLoad)
            {
                Debug.LogWarning("Ignored pre-load asset error: " + exLoad.Message);
            }
            Debug.Log("All data updates finished -> sending clientOk()");
            Service.gI().clientOk();
        }
    }

    private void readFrameBoss(Message msg, int mobTemplateId)
    {
        try
        {
            int num = msg.reader().readByte();
            int[][] array = new int[num][];
            for (int i = 0; i < num; i++)
            {
                int num2 = msg.reader().readByte();
                array[i] = new int[num2];
                for (int j = 0; j < num2; j++)
                {
                    array[i][j] = msg.reader().readByte();
                }
            }
            frameHT_NEWBOSS.put(mobTemplateId + string.Empty, array);
        }
        catch (Exception)
        {
        }
    }

    private int[][] readArrHead(myReader d)
    {
        int[][] array = new int[1][] { new int[2] { 542, 543 } };
        try
        {
            int num = d.readShort();
            array = new int[num][];
            for (int i = 0; i < array.Length; i++)
            {
                int num2 = d.readByte();
                array[i] = new int[num2];
                for (int j = 0; j < num2; j++)
                {
                    array[i][j] = d.readShort();
                }
            }
        }
        catch (Exception)
        {
        }
        return array;
    }

}
