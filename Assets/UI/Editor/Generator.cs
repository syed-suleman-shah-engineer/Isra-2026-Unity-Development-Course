// Assets/Editor/Generator.cs
//
// Editor-only UI structure generator (Unity 6, built-in UI + TextMeshPro).
// Right-click a Canvas in the Hierarchy -> Generate -> Settings Panel.
//
// This tool builds STRUCTURE ONLY. No runtime code, no wiring, no data assets.
// Style it by hand afterwards.

using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Generates the structure of a UI screen inside a selected Canvas.
/// Editor-only. Every created GameObject is registered with Undo.
/// </summary>
public static class Generator
{
    // ------------------------------------------------------------------
    // SCREEN MODEL - hardcoded here on purpose, edit these values directly.
    // ------------------------------------------------------------------
    private const string PanelName    = "Settings Panel";
    private const string TitleLabel   = "Settings";
    private const string SectionLabel = "Audio";
    private const string SliderLabel  = "Music";
    private const string ButtonLabel  = "Close";
    private const string VersionLabel = "v1.0.0";

    private const float PanelWidth        = 320f;  // panel width in pixels
    private const float PanelPadding      = 20f;   // vertical layout padding
    private const float RowControlHeight  = 44f;   // buttons / sliders

    private const float TitleFontSize   = 48f;
    private const float SectionFontSize = 24f;
    private const float ButtonFontSize  = 20f;
    private const float VersionFontSize = 14f;

    private static readonly Color ButtonBlue   = new Color(0.24f, 0.49f, 1f, 1f);
    private static readonly Color VersionGrey  = new Color(0.5f, 0.5f, 0.5f, 1f);
    private static readonly Color TrackGrey    = new Color(0.25f, 0.25f, 0.25f, 1f);
    private static readonly Color HandleWhite  = new Color(0.9f, 0.9f, 0.9f, 1f);

    private const string UndoName = "Generate Settings Panel";

    // ==================================================================
    // MENU
    // ==================================================================

    /// <summary>Builds the Settings panel under the selected Canvas.</summary>
    [MenuItem("GameObject/Generate/Settings Panel", false, 10)]
    public static void GenerateSettingsPanel()
    {
        GameObject canvasGO = Selection.activeGameObject;
        if (canvasGO == null || canvasGO.GetComponent<Canvas>() == null)
        {
            EditorUtility.DisplayDialog(
                PanelName,
                "Select a Canvas in the Hierarchy first.",
                "OK");
            return;
        }

        Transform canvas = canvasGO.transform;

        // ---- Guard: don't run twice -------------------------------------
        Transform existing = canvas.Find(PanelName);
        if (existing != null)
        {
            bool replace = EditorUtility.DisplayDialog(
                PanelName,
                "A \"" + PanelName + "\" already exists under \"" + canvas.name + "\".\n\n" +
                "Replace it?",
                "Replace",
                "Cancel");

            if (!replace)
                return;

            Undo.DestroyObjectImmediate(existing.gameObject);
        }

        // ---- Panel root --------------------------------------------------
        GameObject panel = CreateUIObject(PanelName, canvas);

        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.5f, 0.5f);
        panelRect.anchorMax = new Vector2(0.5f, 0.5f);
        panelRect.pivot = new Vector2(0.5f, 0.5f);
        panelRect.anchoredPosition = Vector2.zero;
        panelRect.sizeDelta = new Vector2(PanelWidth, 0f);

        // Vertical stack, driven by ContentSizeFitter below.
        VerticalLayoutGroup vlg = panel.AddComponent<VerticalLayoutGroup>();
        vlg.padding = new RectOffset(
            Mathf.RoundToInt(PanelPadding), Mathf.RoundToInt(PanelPadding),
            Mathf.RoundToInt(PanelPadding), Mathf.RoundToInt(PanelPadding));
        vlg.spacing = 12f;
        vlg.childAlignment = TextAnchor.MiddleCenter;
        vlg.childControlWidth = true;
        vlg.childControlHeight = true;
        vlg.childForceExpandWidth = true;
        vlg.childForceExpandHeight = false;

        ContentSizeFitter fitter = panel.AddComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        // Keeps the fitted width at PanelWidth instead of collapsing to the
        // widest child (judgement call - remove if you want pure fitting).
        LayoutElement panelLayout = panel.AddComponent<LayoutElement>();
        panelLayout.preferredWidth = PanelWidth;

        // ---- Contents ----------------------------------------------------
        // Title
        CreateText(panel.transform, "Title", TitleLabel,
            TitleFontSize, FontStyles.Bold, Color.white, TextAlignmentOptions.Center);

        // Section header
        CreateText(panel.transform, SectionLabel + " Header", SectionLabel,
            SectionFontSize, FontStyles.Bold, Color.white, TextAlignmentOptions.Left);

        // Music row: label + slider
        Transform row = CreateHorizontalRow(panel.transform, "Music Row");

        TextMeshProUGUI sliderLabel = CreateText(row, "Music Label", SliderLabel,
            ButtonFontSize, FontStyles.Normal, Color.white, TextAlignmentOptions.Left);
        LayoutElement labelLayout = sliderLabel.gameObject.AddComponent<LayoutElement>();
        labelLayout.minWidth = 90f;
        labelLayout.preferredWidth = 90f;
        labelLayout.flexibleWidth = 0f;

        CreateSlider(row, "Music Slider", 0f, 1f, 0.75f);

        // Close button
        CreateButton(panel.transform, ButtonLabel);

        // Version label
        CreateText(panel.transform, "Version", VersionLabel,
            VersionFontSize, FontStyles.Normal, VersionGrey, TextAlignmentOptions.Center);

        // ---- Finish ------------------------------------------------------
        Selection.activeGameObject = panel;
        EditorGUIUtility.PingObject(panel);
        Debug.Log("[Generator] Created \"" + PanelName + "\" under \"" + canvas.name + "\".");
    }

    /// <summary>Greys the menu item out when the selection is not a Canvas.</summary>
    [MenuItem("GameObject/Generate/Settings Panel", true)]
    private static bool ValidateGenerateSettingsPanel()
    {
        return IsCanvasSelected();
    }

    /// <summary>Shared by the validator: true when the active object is a Canvas.</summary>
    private static bool IsCanvasSelected()
    {
        return Selection.activeGameObject != null
            && Selection.activeGameObject.GetComponent<Canvas>() != null;
    }

    // ==================================================================
    // HELPERS
    // ==================================================================

    /// <summary>Creates an empty RectTransform object under <paramref name="parent"/> (undo-aware).</summary>
    private static GameObject CreateUIObject(string name, Transform parent)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        Undo.RegisterCreatedObjectUndo(go, UndoName);
        return go;
    }

    /// <summary>Creates a TextMeshProUGUI label.</summary>
    private static TextMeshProUGUI CreateText(
        Transform parent,
        string name,
        string content,
        float size,
        FontStyles style,
        Color color,
        TextAlignmentOptions alignment = TextAlignmentOptions.Center)
    {
        GameObject go = CreateUIObject(name, parent);

        TextMeshProUGUI text = go.AddComponent<TextMeshProUGUI>();
        text.text = content;
        text.fontSize = size;
        text.fontStyle = style;
        text.color = color;
        text.alignment = alignment;

        return text;
    }

    /// <summary>Creates a blue button with a stretched, centred label.</summary>
    private static GameObject CreateButton(Transform parent, string label)
    {
        GameObject go = CreateUIObject(label + " Button", parent);

        Image image = go.AddComponent<Image>();
        image.color = ButtonBlue;

        Button button = go.AddComponent<Button>();
        button.targetGraphic = image;

        LayoutElement layout = go.AddComponent<LayoutElement>();
        layout.minHeight = RowControlHeight;
        layout.preferredHeight = RowControlHeight;

        TextMeshProUGUI text = CreateText(go.transform, "Label", label,
            ButtonFontSize, FontStyles.Normal, Color.white, TextAlignmentOptions.Center);

        // Stretch the label over the whole button.
        RectTransform textRect = text.rectTransform;
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        return go;
    }

    /// <summary>Creates a horizontal row (HorizontalLayoutGroup) and returns its Transform.</summary>
    private static Transform CreateHorizontalRow(Transform parent, string name)
    {
        GameObject go = CreateUIObject(name, parent);

        HorizontalLayoutGroup hlg = go.AddComponent<HorizontalLayoutGroup>();
        hlg.padding = new RectOffset(10, 10, 10, 10);
        hlg.spacing = 10f;
        hlg.childAlignment = TextAnchor.MiddleCenter;
        hlg.childControlWidth = true;
        hlg.childControlHeight = true;
        hlg.childForceExpandWidth = true;
        hlg.childForceExpandHeight = false;

        return go.transform;
    }

    /// <summary>Creates a grid container (GridLayoutGroup) and returns its Transform.</summary>
    private static Transform CreateGrid(Transform parent, string name, float cellSizeX, float cellSizeY)
    {
        GameObject go = CreateUIObject(name, parent);

        GridLayoutGroup grid = go.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(cellSizeX, cellSizeY);
        grid.spacing = new Vector2(10f, 10f);
        grid.padding = new RectOffset(10, 10, 10, 10);
        grid.childAlignment = TextAnchor.MiddleCenter;

        return go.transform;
    }

    /// <summary>Creates a bare-bones Slider (background / fill / handle) and returns it.</summary>
    private static Slider CreateSlider(
        Transform parent,
        string name,
        float minValue,
        float maxValue,
        float value)
    {
        GameObject go = CreateUIObject(name, parent);

        Slider slider = go.AddComponent<Slider>();

        LayoutElement layout = go.AddComponent<LayoutElement>();
        layout.minHeight = RowControlHeight;
        layout.preferredHeight = RowControlHeight;
        layout.flexibleWidth = 1f;

        // --- Background ---------------------------------------------------
        GameObject background = CreateUIObject("Background", go.transform);
        Image backgroundImage = background.AddComponent<Image>();
        backgroundImage.color = TrackGrey;

        RectTransform backgroundRect = background.GetComponent<RectTransform>();
        backgroundRect.anchorMin = new Vector2(0f, 0.25f);
        backgroundRect.anchorMax = new Vector2(1f, 0.75f);
        backgroundRect.offsetMin = Vector2.zero;
        backgroundRect.offsetMax = Vector2.zero;

        // --- Fill Area / Fill ---------------------------------------------
        GameObject fillArea = CreateUIObject("Fill Area", go.transform);
        RectTransform fillAreaRect = fillArea.GetComponent<RectTransform>();
        fillAreaRect.anchorMin = new Vector2(0f, 0.25f);
        fillAreaRect.anchorMax = new Vector2(1f, 0.75f);
        fillAreaRect.offsetMin = new Vector2(5f, 0f);
        fillAreaRect.offsetMax = new Vector2(-15f, 0f);

        GameObject fill = CreateUIObject("Fill", fillArea.transform);
        Image fillImage = fill.AddComponent<Image>();
        fillImage.color = ButtonBlue;

        RectTransform fillRect = fill.GetComponent<RectTransform>();
        fillRect.anchorMin = new Vector2(0f, 0f);
        fillRect.anchorMax = new Vector2(1f, 1f);
        fillRect.offsetMin = Vector2.zero;
        fillRect.offsetMax = Vector2.zero;

        // --- Handle Slide Area / Handle -----------------------------------
        GameObject handleArea = CreateUIObject("Handle Slide Area", go.transform);
        RectTransform handleAreaRect = handleArea.GetComponent<RectTransform>();
        handleAreaRect.anchorMin = Vector2.zero;
        handleAreaRect.anchorMax = Vector2.one;
        handleAreaRect.offsetMin = new Vector2(10f, 0f);
        handleAreaRect.offsetMax = new Vector2(-10f, 0f);

        GameObject handle = CreateUIObject("Handle", handleArea.transform);
        Image handleImage = handle.AddComponent<Image>();
        handleImage.color = HandleWhite;

        RectTransform handleRect = handle.GetComponent<RectTransform>();
        handleRect.anchorMin = new Vector2(0f, 0f);
        handleRect.anchorMax = new Vector2(0f, 1f);
        handleRect.sizeDelta = new Vector2(20f, 0f);

        // --- Wire up ------------------------------------------------------
        slider.fillRect = fillRect;
        slider.handleRect = handleRect;
        slider.targetGraphic = handleImage;
        slider.direction = Slider.Direction.LeftToRight;
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.wholeNumbers = false;
        slider.value = value;

        return slider;
    }
}