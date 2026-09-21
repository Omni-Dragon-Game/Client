#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MapViewerWindow : EditorWindow
{
    private int mapID = 0;
    private int tmw = 0;
    private int tmh = 0;
    private int[] mapTiles = null;
    private Texture2D previewTexture = null;
    private Vector2 scrollPos;
    private float zoomScale = 1.0f;
    private string statusMessage = "Nhập Map ID và bấm 'Load Map' để xem trước.";

    private static readonly string[] MAP_NAMES = new string[]
    {
        "Làng Aru (Trái Đất)", "Đồi hoa cúc", "Thung lũng tre", "Rừng nấm", "Rừng xương",
        "Đảo Kame", "Đông Karjn", "Thung lũng Nappa", "Vực cắm", "Vực chết",
        "Rừng cọ", "Rừng đá", "Thung lũng đen", "Bờ vực đen", "Làng Mori (Namec)",
        "Đồi nấm", "Thung lũng Maima", "Thung lũng Guru", "Vực Maima", "Làng Kakarot (Xayda)",
        "Đồi hoang", "Làng Plant", "Rừng nguyên sinh", "Rừng thông", "Thung lũng Maya",
        "Vực Chết (Xayda)", "Rừng tuyết", "Núi tuyết", "Dòng sông băng", "Rừng cọ tuyết",
        "Thánh địa Kaio", "Võ đài Xên bọ hung", "Đại hội võ thuật"
    };

    [MenuItem("Tools/Map Viewer")]
    public static void ShowWindow()
    {
        MapViewerWindow window = GetWindow<MapViewerWindow>("Map Viewer");
        window.minSize = new Vector2(650, 500);
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("🎮 KHÁM PHÁ BẢN ĐỒ (MAP VIEWER)", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        mapID = EditorGUILayout.IntField("Map ID (0 - 185):", mapID);
        if (mapID < 0) mapID = 0;
        if (mapID > 185) mapID = 185;

        string mapName = (mapID < MAP_NAMES.Length) ? MAP_NAMES[mapID] : ("Bản đồ số " + mapID);
        EditorGUILayout.LabelField(mapName, EditorStyles.boldLabel, GUILayout.Width(200));

        if (GUILayout.Button("Load Map", GUILayout.Width(100), GUILayout.Height(24)))
        {
            LoadMapData(mapID);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        zoomScale = EditorGUILayout.Slider("Tỉ lệ xem (Zoom):", zoomScale, 0.25f, 2.5f);
        if (GUILayout.Button("100%", GUILayout.Width(50)))
        {
            zoomScale = 1.0f;
        }
        if (GUILayout.Button("50%", GUILayout.Width(50)))
        {
            zoomScale = 0.5f;
        }
        EditorGUILayout.EndHorizontal();

        if (tmw > 0 && tmh > 0)
        {
            EditorGUILayout.HelpBox(string.Format("Kích thước: {0} x {1} tiles ({2} x {3} px) | Tổng số ô: {4}", 
                tmw, tmh, tmw * 24, tmh * 24, tmw * tmh), MessageType.Info);
        }
        else
        {
            EditorGUILayout.HelpBox(statusMessage, MessageType.None);
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        if (previewTexture != null)
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, true);
            float drawW = previewTexture.width * zoomScale;
            float drawH = previewTexture.height * zoomScale;
            Rect rect = GUILayoutUtility.GetRect(drawW, drawH);
            GUI.DrawTexture(rect, previewTexture, ScaleMode.StretchToFill);
            EditorGUILayout.EndScrollView();
        }
    }

    private void LoadMapData(int id)
    {
        try
        {
            string path = Path.Combine(Application.dataPath, "Resources/res/mymap/" + id + ".bytes");
            if (!File.Exists(path))
            {
                statusMessage = "Không tìm thấy file map: " + path;
                previewTexture = null;
                return;
            }

            byte[] data = File.ReadAllBytes(path);
            if (data == null || data.Length < 2)
            {
                statusMessage = "File map rỗng!";
                return;
            }

            using (MemoryStream ms = new MemoryStream(data))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                tmw = reader.ReadByte();
                tmh = reader.ReadByte();
                int totalTiles = tmw * tmh;
                mapTiles = new int[totalTiles];

                for (int i = 0; i < totalTiles; i++)
                {
                    if (ms.Position < ms.Length)
                    {
                        mapTiles[i] = reader.ReadByte();
                    }
                }
            }

            RenderMapTexture();
            statusMessage = "Đã nạp bản đồ " + id + " thành công!";
        }
        catch (Exception ex)
        {
            statusMessage = "Lỗi khi nạp map: " + ex.Message;
            previewTexture = null;
        }
    }

    private void RenderMapTexture()
    {
        int tileSize = 24;
        int imgW = tmw * tileSize;
        int imgH = tmh * tileSize;

        if (previewTexture != null)
        {
            DestroyImmediate(previewTexture);
        }

        previewTexture = new Texture2D(imgW, imgH, TextureFormat.RGBA32, false);
        previewTexture.filterMode = FilterMode.Point;

        Color skyColor = new Color(0.48f, 0.72f, 0.96f, 1f);
        Color[] clearColors = new Color[imgW * imgH];
        for (int i = 0; i < clearColors.Length; i++) clearColors[i] = skyColor;
        previewTexture.SetPixels(clearColors);

        string tileFolder = Path.Combine(Application.dataPath, "Resources/res/x2/t");
        var tileCache = new Dictionary<int, Texture2D>();

        for (int y = 0; y < tmh; y++)
        {
            for (int x = 0; x < tmw; x++)
            {
                int tileIndex = mapTiles[y * tmw + x];
                if (tileIndex <= 0) continue;

                Texture2D tileTex = null;
                if (!tileCache.TryGetValue(tileIndex, out tileTex))
                {
                    string[] matchedFiles = Directory.Exists(tileFolder)
                        ? Directory.GetFiles(tileFolder, "*$" + tileIndex + ".png")
                        : new string[0];

                    if (matchedFiles.Length > 0)
                    {
                        byte[] fileData = File.ReadAllBytes(matchedFiles[0]);
                        tileTex = new Texture2D(2, 2);
                        tileTex.LoadImage(fileData);
                    }
                    tileCache[tileIndex] = tileTex;
                }

                if (tileTex != null)
                {
                    int destX = x * tileSize;
                    int destY = (tmh - 1 - y) * tileSize;

                    for (int py = 0; py < tileSize && py < tileTex.height; py++)
                    {
                        for (int px = 0; px < tileSize && px < tileTex.width; px++)
                        {
                            Color c = tileTex.GetPixel(px, py);
                            if (c.a > 0.05f)
                            {
                                previewTexture.SetPixel(destX + px, destY + py, c);
                            }
                        }
                    }
                }
            }
        }

        previewTexture.Apply();
    }

    private void OnDestroy()
    {
        if (previewTexture != null)
        {
            DestroyImmediate(previewTexture);
            previewTexture = null;
        }
    }
}
#endif
