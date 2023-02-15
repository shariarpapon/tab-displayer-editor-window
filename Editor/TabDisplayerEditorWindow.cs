using UnityEngine;
using UnityEditor;

public class TabDisplayerEditorWindow : EditorWindow
{
    #region General Setup
    private const string WINDOW_TITLE = "Tab Window";
    private const float BUTTON_SECTION_WIDTH = 180;
    private const float BUTTON_SECTION_TOP_OFFSET = 4;
    private static readonly Color BUTTON_SECTION_COLOR = new Color(147f / 255, 160f / 255, 166f / 255);
    private static readonly Color TAB_DISPLAY_SECTION_COLOR = new Color(91f / 255, 100f / 255, 110f / 255);
    private static Tab ActiveTab = null;

    [MenuItem("Tools/Configurator")]
    public static void ShowWindow()
    {
        GetWindow<TabDisplayerEditorWindow>(WINDOW_TITLE);
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        EditorGUI.DrawRect(new Rect(0, 0, BUTTON_SECTION_WIDTH, position.height), BUTTON_SECTION_COLOR);
        DrawButtonSection();

        EditorGUI.DrawRect(new Rect(BUTTON_SECTION_WIDTH, 0, position.width - BUTTON_SECTION_WIDTH, position.height), TAB_DISPLAY_SECTION_COLOR);
        DrawTabDisplaySection();

        GUILayout.EndHorizontal();
    }

    private void DrawButtonSection()
    {
        GUILayout.BeginVertical(GUILayout.Width(BUTTON_SECTION_WIDTH));
        GUILayout.Space(BUTTON_SECTION_TOP_OFFSET);
        for (int i = 0; i < TABS.Length; i++)
        {
            if (GUILayout.Button(TABS[i].name, GUILayout.Width(BUTTON_SECTION_WIDTH - 8)))
            {
                ActiveTab = TABS[i];
            }
            GUILayout.Space(2);
        }
        GUILayout.EndVertical();
    }

    private void DrawTabDisplaySection()
    {
        GUILayout.BeginVertical();
        ActiveTab?.drawer.Invoke();
        GUILayout.EndVertical();
    }

    private sealed class Tab
    {
        public readonly string name;
        public readonly System.Action drawer;

        public Tab(string name, System.Action drawer)
        {
            this.name = name;
            this.drawer = drawer;
        }
    }
    #endregion

    private static readonly Tab[] TABS = new Tab[]
    {
        new Tab("Tab 0", Tab0),
        new Tab("Tab 1", Tab1)
    };

    private static void Tab0() 
    {
        GUILayout.Label("Tab 0 display");
    }

    private static void Tab1() 
    {
        GUILayout.Label("Tab 1 display");
    }
}