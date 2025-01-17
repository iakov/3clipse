﻿
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace PluginMaster
{
    [InitializeOnLoad]
    public class HDRPDefine
    {
        static HDRPDefine()
        {
            var currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
            if (currentRenderPipeline == null) return;
            if (!currentRenderPipeline.GetType().ToString().Contains("HighDefinition")) return;
            var target = EditorUserBuildSettings.activeBuildTarget;
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(target);
            var definesSCSV = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            const string PWB_HDRP = "PWB_HDRP";
            if (definesSCSV.Contains(PWB_HDRP)) return;
            definesSCSV += ";" + PWB_HDRP;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, definesSCSV);
        }
    }

    public class ThumbnailUtils
    {
        private static  LayerMask layerMask => 1 << PWBCore.staticData.thumbnailLayer;
        public const int SIZE = 256;
        private const int MIN_SIZE = 24;
        private static Texture2D _emptyTexture = null;

        private class ThumbnailEditor
        {
            public ThumbnailSettings settings = null;
            public GameObject root = null;
            public Camera camera = null;
            public RenderTexture renderTexture = null;
            public Light light = null;
            public Transform pivot = null;
            public GameObject target = null;
        }

        public static void RenderTextureToTexture2D(RenderTexture renderTexture, Texture2D texture)
        {
            var prevActive = RenderTexture.active;
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, SIZE, SIZE), 0, 0);
            texture.Apply();
            RenderTexture.active = prevActive;
        }

        private static Texture2D emptyTexture
        {
            get
            {
                if (_emptyTexture == null) _emptyTexture = Resources.Load<Texture2D>("Sprites/Empty");
                return _emptyTexture;
            }
        }


        public static void UpdateThumbnail(ThumbnailSettings settings, Texture2D thumbnailTexture, GameObject prefab)
        {
            var magnitude = BoundsUtils.GetMagnitude(prefab.transform);
            var thumbnailEditor = new ThumbnailEditor();
            thumbnailEditor.settings = new ThumbnailSettings(settings);

            if (magnitude == 0)
            {
                if (_emptyTexture == null) _emptyTexture = Resources.Load<Texture2D>("Sprites/Empty");
                var pixels = _emptyTexture.GetPixels32();
                for (int i = 0; i < pixels.Length; ++i)
                {
                    if (pixels[i].a == 0) pixels[i] = thumbnailEditor.settings.backgroudColor;
                }
                thumbnailTexture.SetPixels32(pixels);
                thumbnailTexture.Apply();
                return;
            }
            var sceneLights = Object.FindObjectsOfType<Light>().ToDictionary(comp => comp, light => light.cullingMask);

            const string rootName = "PWBThumbnailEditor";

            do
            {
                var obj = GameObject.Find(rootName);
                if (obj == null) break;
                else GameObject.DestroyImmediate(obj);
            } while (true);

            thumbnailEditor.root = new GameObject(rootName);

            var camObj = new GameObject("PWBThumbnailEditorCam");
            thumbnailEditor.camera = camObj.AddComponent<Camera>();
            thumbnailEditor.camera.transform.SetParent(thumbnailEditor.root.transform);
            thumbnailEditor.camera.transform.localPosition = new Vector3(0f, 1.2f, -4f);
            thumbnailEditor.camera.transform.localRotation = Quaternion.Euler(17.5f, 0f, 0f);
            thumbnailEditor.camera.fieldOfView = 20f;
            thumbnailEditor.camera.clearFlags = CameraClearFlags.SolidColor;
            thumbnailEditor.camera.backgroundColor = thumbnailEditor.settings.backgroudColor;
            thumbnailEditor.camera.cullingMask = layerMask;
            thumbnailEditor.renderTexture = new RenderTexture(SIZE, SIZE, 24);
            thumbnailEditor.camera.targetTexture = thumbnailEditor.renderTexture;

            var lightObj = new GameObject("PWBThumbnailEditorLight");
            thumbnailEditor.light = lightObj.AddComponent<Light>();
            thumbnailEditor.light.type = LightType.Directional;
            thumbnailEditor.light.transform.SetParent(thumbnailEditor.root.transform);
            thumbnailEditor.light.transform.localRotation = Quaternion.Euler(thumbnailEditor.settings.lightEuler);
            thumbnailEditor.light.color = thumbnailEditor.settings.lightColor;
            thumbnailEditor.light.intensity = thumbnailEditor.settings.lightIntensity;
            thumbnailEditor.light.cullingMask = layerMask;

            var pivotObj = new GameObject("PWBThumbnailEditorPivot");
            pivotObj.layer = PWBCore.staticData.thumbnailLayer;
            thumbnailEditor.pivot = pivotObj.transform;
            thumbnailEditor.pivot.transform.SetParent(thumbnailEditor.root.transform);
            thumbnailEditor.pivot.localPosition = thumbnailEditor.settings.targetOffset;
            thumbnailEditor.pivot.transform.localRotation = Quaternion.identity;
            thumbnailEditor.pivot.transform.localScale = Vector3.one;

            Transform InstantiateBones(Transform source, Transform parent)
            {
                var obj = new GameObject();
                obj.name = source.name;
                obj.transform.SetParent(parent);
                obj.transform.position = source.position;
                obj.transform.rotation = source.rotation;
                obj.transform.localScale = source.localScale;
                foreach (Transform child in source) InstantiateBones(child, obj.transform);
                return obj.transform;
            }

            bool Requires(System.Type obj, System.Type requirement)
            {
                return System.Attribute.IsDefined(obj, typeof(RequireComponent))
                    && System.Attribute.GetCustomAttributes(obj, typeof(RequireComponent)).OfType<RequireComponent>()
                       .Any(rc => rc.m_Type0.IsAssignableFrom(requirement));
            }

            bool CanDestroy(GameObject go, System.Type t)
                => !go.GetComponents<Component>().Any(c => Requires(c.GetType(), t));

            void CopyComponents(GameObject source, GameObject destination)
            {
                var srcComps = source.GetComponentsInChildren<Component>();
                foreach (var srcComp in srcComps)
                {
                    if (srcComp is MonoBehaviour) continue;
                    var destComp = srcComp is Transform ? destination.transform : destination.AddComponent(srcComp.GetType());
                    EditorUtility.CopySerialized(srcComp, destComp);
                }
                foreach (Transform srcChild in source.transform)
                {
                    var destChild = new GameObject();
                    destChild.transform.SetParent(destination.transform);
                    CopyComponents(srcChild.gameObject, destChild);
                }
            }

            GameObject InstantiateAndRemoveMonoBehaviours(GameObject source)
            {
                var sourceIsActive = source.activeSelf;
                source.SetActive(false);
                var obj = Object.Instantiate(prefab);
                var toBeDestroyed = new List<Component>(obj.GetComponentsInChildren<Component>());

                while (toBeDestroyed.Count > 0)
                {
                    var components = toBeDestroyed.ToArray();
                    int compCount = components.Length;
                    toBeDestroyed.Clear();
                    foreach (var comp in components)
                    {
                        if (comp is MonoBehaviour)
                        {
                            var monoBehaviour = comp as MonoBehaviour;
                            monoBehaviour.enabled = false;
                            monoBehaviour.runInEditMode = false;
                            if (CanDestroy(obj, comp.GetType())) Object.DestroyImmediate(comp);
                            else toBeDestroyed.Add(comp);
                        }
                    }
                    if (compCount == toBeDestroyed.Count) break;
                }
                if (toBeDestroyed.Count > 0)
                {
                    var noMonoBehaviourObj = new GameObject();
                    CopyComponents(noMonoBehaviourObj, obj);
                    Object.DestroyImmediate(obj);
                    obj = noMonoBehaviourObj;
                }
                source.SetActive(sourceIsActive);
                obj.SetActive(sourceIsActive);
                return obj;
            }

            thumbnailEditor.target = InstantiateAndRemoveMonoBehaviours(prefab);

            var monoBehaviours = thumbnailEditor.target.GetComponentsInChildren<MonoBehaviour>();
            foreach (var monoBehaviour in monoBehaviours)
                if (monoBehaviour != null) monoBehaviour.enabled = false;

            magnitude = BoundsUtils.GetMagnitude(thumbnailEditor.target.transform);
            var targetScale = magnitude > 0 ? 1f / magnitude : 1f;
            var targetBounds = BoundsUtils.GetBoundsRecursive(thumbnailEditor.target.transform);
            var localPosition = (thumbnailEditor.target.transform.localPosition - targetBounds.center) * targetScale;
            thumbnailEditor.target.transform.SetParent(thumbnailEditor.pivot);
            thumbnailEditor.target.transform.localPosition = localPosition;
            thumbnailEditor.target.transform.localRotation = Quaternion.identity;
            thumbnailEditor.target.transform.localScale = prefab.transform.localScale * targetScale;
            thumbnailEditor.pivot.localScale = Vector3.one * thumbnailEditor.settings.zoom;
            thumbnailEditor.pivot.localRotation = Quaternion.Euler(thumbnailEditor.settings.targetEuler);


#if PWB_HDRP
            var HDCamData = camObj.AddComponent<UnityEngine.Rendering.HighDefinition.HDAdditionalCameraData>();
            HDCamData.volumeLayerMask = layerMask | 1;
            HDCamData.probeLayerMask = 0;
            HDCamData.clearColorMode = UnityEngine.Rendering.HighDefinition.HDAdditionalCameraData.ClearColorMode.Color;
            HDCamData.backgroundColorHDR = thumbnailEditor.settings.backgroudColor;
            HDCamData.antialiasing
                = UnityEngine.Rendering.HighDefinition.HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing;

            thumbnailEditor.light.intensity *= 100;
#endif

            var children = thumbnailEditor.root.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                child.gameObject.layer = PWBCore.staticData.thumbnailLayer;
                child.gameObject.hideFlags = HideFlags.HideAndDontSave;
            }
            foreach (var light in sceneLights.Keys) light.cullingMask = light.cullingMask & ~layerMask;
            thumbnailEditor.camera.Render();
            foreach (var light in sceneLights.Keys) light.cullingMask = sceneLights[light];

            RenderTextureToTexture2D(thumbnailEditor.camera.targetTexture, thumbnailTexture);

            Object.DestroyImmediate(thumbnailEditor.root);
        }

        public static void UpdateThumbnail(ThumbnailSettings settings, Texture2D thumbnailTexture, Texture2D[] subThumbnails)
        {
            if (subThumbnails.Length == 0)
            {
                thumbnailTexture.SetPixels(new Color[SIZE * SIZE]);
                thumbnailTexture.Apply();
                return;
            }
            var sqrt = Mathf.Sqrt(subThumbnails.Length);
            var sideCellsCount = Mathf.FloorToInt(sqrt);
            if (Mathf.CeilToInt(sqrt) != sideCellsCount) ++sideCellsCount;
            var spacing = (SIZE * sideCellsCount) / MIN_SIZE;
            var bigSize = SIZE * sideCellsCount + spacing * (sideCellsCount - 1);
            var texture = new Texture2D(bigSize, bigSize);
            var pixelCount = bigSize * bigSize;
            var pixels = new Color32[pixelCount];
            texture.SetPixels32(pixels);
            int subIdx = 0;
            for (int i = sideCellsCount - 1; i >= 0; --i)
            {
                for (int j = 0; j < sideCellsCount; ++j)
                {
                    var x = j * (SIZE + spacing);
                    var y = i * (SIZE + spacing);
                    if (subThumbnails[subIdx] == null) continue;
                    var subPixels = subThumbnails[subIdx].GetPixels32();
                    texture.SetPixels32(x, y, SIZE, SIZE, subPixels);
                    ++subIdx;
                    if (subIdx == subThumbnails.Length) goto Resize;
                }
            }
        Resize:
            texture.filterMode = FilterMode.Trilinear;
            texture.Apply();
            var renderTexture = new RenderTexture(SIZE, SIZE, 24);
            var prevActive = RenderTexture.active;
            RenderTexture.active = renderTexture;
            Graphics.Blit(texture, renderTexture);
            thumbnailTexture.ReadPixels(new Rect(0, 0, SIZE, SIZE), 0, 0);
            thumbnailTexture.Apply();
            RenderTexture.active = prevActive;
            Object.DestroyImmediate(texture);
        }

        public static void UpdateThumbnail(MultibrushItemSettings brushItem)
        {
            if (brushItem.prefab == null) return;
            UpdateThumbnail(brushItem.thumbnailSettings, brushItem.thumbnailTexture, brushItem.prefab);
        }

        public static void UpdateThumbnail(MultibrushSettings brushSettings)
        {
            var brushItems = brushSettings.items;
            var subThumbnails = new List<Texture2D>();
            foreach (var item in brushItems)
            {
                if (item.includeInThumbnail) subThumbnails.Add(item.thumbnail);
                UpdateThumbnail(item);
            }
            UpdateThumbnail(brushSettings.thumbnailSettings, brushSettings.thumbnailTexture, subThumbnails.ToArray());
        }
    }
}
