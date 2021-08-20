using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Plugins.mitaywalle.HierarchyIcons
{
    public static class HierarchyIcons
    {
        public static readonly Color AttentionColor = new Color(.7f, .3f, .3f, 0.2f);
#if UNITY_EDITOR
        private const bool ASVolume = false;
        private static readonly Color ASVolumePlayingColor = new Color(0f, 1f, 0f, 0.2f);
        private static readonly Color ASVolumeColor = new Color(.5f, .75f, .5f, 0.3f);

        private static readonly Dictionary<Type, Texture2D> icons = new Dictionary<Type, Texture2D>();
        private static readonly Dictionary<Type, GUIContent> cachBuiltInIcons = new Dictionary<Type, GUIContent>();

        private static readonly HashSet<Type> IconedTypes = new HashSet<Type>()
        {
            typeof(Canvas),
            typeof(CanvasGroup),
            typeof(Mask),
            typeof(RectMask2D),
            typeof(Button),
            typeof(Toggle),
            typeof(RawImage),
            typeof(ScrollRect),
            typeof(ToggleGroup),
            typeof(HorizontalLayoutGroup),
            typeof(VerticalLayoutGroup),
            typeof(Grid),
            typeof(ContentSizeFitter),
            typeof(LayoutElement),
            typeof(ParticleSystem),
            typeof(AudioSource),
            typeof(SpriteMask),
            //typeof(SpriteRenderer),
            typeof(SortingGroup),
            typeof(GraphicRaycaster),
            typeof(MeshCollider),
            typeof(BoxCollider),
            typeof(SphereCollider),
            typeof(CapsuleCollider),
        };

        private static readonly HashSet<Type> AttentionTypes = new HashSet<Type>()
        {
            typeof(Mask),
            typeof(RectMask2D),
            typeof(RawImage),
            typeof(ScrollRect),
            typeof(HorizontalLayoutGroup),
            typeof(VerticalLayoutGroup),
            typeof(Grid),
            typeof(ContentSizeFitter),
            typeof(GraphicRaycaster),
        };

        private static Type ASType = typeof(AudioSource);

        private static Texture2D t = new Texture2D(1, 1);

        [InitializeOnLoadMethod]
        static void Init()
        {
            Color c = new Color(1f, 1f, 1f, 1f);
            t.SetPixel(1, 1, c);
            t.Apply();

            foreach (var iconedType in IconedTypes)
            {
                cachBuiltInIcons.Add(iconedType, GetBuiltInTex(iconedType));
            }

            EditorApplication.hierarchyWindowItemOnGUI += EvaluateIcons;
            EditorApplication.projectWindowItemOnGUI += ProjectOnGUI;
        }

        private static void ProjectOnGUI(string guid, Rect rect)
        {
            if (string.IsNullOrEmpty(guid)) return;
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(path)) return;
            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            if (!asset) return;
            var coloredAsset = asset as AssetProjectColors;

            if (coloredAsset == null) return;

            if (!t)
            {
                t = new Texture2D(1, 1);
                Color c = new Color(1f, 1f, 1f, 1f);
                t.SetPixel(1, 1, c);
                t.Apply();
            }

            if (coloredAsset.color == Color.clear) return;

            Rect r = new Rect(rect.x + rect.width - 16f, rect.y, 16f, 16f);
            var oldColor = GUI.color;
            GUI.color = coloredAsset.color;
            GUI.DrawTexture(rect, t, ScaleMode.StretchToFill);
            GUI.color = oldColor;
        }

        private static void EvaluateIcons(int instanceId, Rect rect)
        {
            GameObject go = EditorUtility.InstanceIDToObject(instanceId) as GameObject;

            if (go == null) return;

            var uiBehs = go.GetComponents(typeof(Component));

            if (uiBehs != null)
            {
                int length = uiBehs.Length;
                int counter = 0;

                for (int i = 0; i < length; i++)
                {
                    if (uiBehs[i] == null) continue;

                    var type = uiBehs[i].GetType();
                    if (ASVolume && type == ASType)
                    {
                        DrawASVolume(rect, uiBehs[i]);
                    }

                    if (AttentionTypes.Contains(type))
                    {
                        if ((uiBehs[i] as Behaviour).enabled)
                        {
                            DrawAttentionType(rect);
                        }
                    }

                    if (!IconedTypes.Contains(type)) continue;

                    DrawUIBehs(cachBuiltInIcons[type], rect, counter++);
                }
            }
//        else
//        {
//            Debug.LogError(uiBehs);
//        }

            IHierarchyIconBehaviour slotCon = go.GetComponent<IHierarchyIconBehaviour>();
            if (slotCon != null) DrawIcon(slotCon, rect, slotCon.EditorIconBGColor);
        }

        private static void DrawASVolume(Rect rect, Component source)
        {
            var AS = source as AudioSource;
            if (!AS.isActiveAndEnabled) return;

            if (!t)
            {
                t = new Texture2D(1, 1);
                Color c = new Color(1f, 1f, 1f, 1f);
                t.SetPixel(1, 1, c);
                t.Apply();
            }


            rect.width *= AS.volume;

            var oldColor = GUI.color;
            GUI.color = AS.isPlaying ? ASVolumePlayingColor : ASVolumeColor;
            GUI.DrawTexture(rect, t, ScaleMode.StretchToFill);
            GUI.color = oldColor;
        }


        private static void DrawAttentionType(Rect rect)
        {
            if (!t)
            {
                t = new Texture2D(1, 1);
                Color c = new Color(1f, 1f, 1f, 1f);
                t.SetPixel(1, 1, c);
                t.Apply();
            }


            var oldColor = GUI.color;
            GUI.color = AttentionColor;
            GUI.DrawTexture(rect, t, ScaleMode.StretchToFill);
            GUI.color = oldColor;
        }

        private static void DrawUIBehs(GUIContent content, Rect rect, int index)
        {
            Rect r = new Rect(rect.x + rect.width - 32f - 16f * index, rect.y, 16f, 16f);
            GUI.Label(r, content);
        }

        private static void DrawIcon(IHierarchyIconBehaviour obj, Rect rect, Color color)
        {
            if (!t)
            {
                t = new Texture2D(1, 1);
                Color c = new Color(1f, 1f, 1f, 1f);
                t.SetPixel(1, 1, c);
                t.Apply();
            }

            if (GUI.color != color)
            {
                var oldColor = GUI.color;
                GUI.color = color;
                GUI.DrawTexture(rect, t, ScaleMode.StretchToFill);
                GUI.color = oldColor;
            }

            var tex = GetTex(obj);
            if (tex)
            {
                Rect r = new Rect(rect.x + rect.width - 16f, rect.y, 16f, 16f);

                GUI.DrawTexture(r, tex);
            }
        }

        private static GUIContent GetBuiltInTex(Type type)
        {
            var typeSplit = type.ToString().Split('.');
            var typeName = typeSplit[typeSplit.Length - 1];
            return EditorGUIUtility.IconContent($"{typeName} Icon");
        }

        private static Texture2D GetTex(IHierarchyIconBehaviour obj)
        {
            var type = obj.GetType();

            if (!icons.ContainsKey(type))
            {
                if (obj.EditorIconBuiltInType != null && cachBuiltInIcons.ContainsKey(obj.EditorIconBuiltInType))
                {
                    icons.Add(type, cachBuiltInIcons[obj.EditorIconBuiltInType].image as Texture2D);
                }
                else
                {
                    icons.Add(type,
                        AssetDatabase.LoadAssetAtPath<Texture2D>(
                            $"Assets/{obj.EditorIconName}.png"));
                }
            }

            return icons[type];
        }

#endif
    }
}