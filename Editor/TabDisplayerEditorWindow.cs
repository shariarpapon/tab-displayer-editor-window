using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class TabDisplayerEditorWindow : EditorWindow
{
    private const float BUTTON_SECTION_WIDTH = 180;
    private const float BUTTON_SECTION_TOP_OFFSET = 4;
    private readonly Color BUTTON_SECTION_COLOR = new(147f / 255, 160f / 255, 166f / 255);
    private readonly Color TAB_DISPLAY_SECTION_COLOR = new(91f / 255, 100f / 255, 110f / 255);

    private Tab ActiveTab = null;
    private List<Tab> allTabs;

    private void OnEnable()
    {
        OnWindowInitialize();
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

    protected virtual void OnWindowInitialize()
    {
        SetWindowTitle("Tab Window");
    }

    protected virtual void DrawButtonSection()
    {
        GUILayout.BeginVertical(GUILayout.Width(BUTTON_SECTION_WIDTH));
        GUILayout.Space(BUTTON_SECTION_TOP_OFFSET);
        if (allTabs != null)
        {
            for (int i = 0; i < allTabs.Count; i++)
            {
                if (GUILayout.Button(allTabs[i].name, GUILayout.Width(BUTTON_SECTION_WIDTH - 8)))
                {
                    ActiveTab = allTabs[i];
                }
                GUILayout.Space(2);
            }
        }
        GUILayout.EndVertical();
    }

    protected virtual void DrawTabDisplaySection()
    {
        GUILayout.BeginVertical();
        ActiveTab?.drawer.Invoke();
        GUILayout.EndVertical();
    }

    public void SetDefaultTab(int index)
    {
	if(allTabs != null)
	{
        	index %= allTabs.Count;
        	if (index < 0 || allTabs.Count <= 0)
        	{
            		return;
        	}
        	ActiveTab = allTabs[index];
	}
    }

    public void SetDefaultTab(Tab tab)
    {
        if (tab != null && allTabs != null)
        {
            if (!allTabs.Contains(tab))
            {
                allTabs.Add(tab);
            }
            ActiveTab = tab;
        }
    }

    public void NewTab(Tab tab)
    {
	if(tab == null) return;

        if (allTabs == null)
        {
            allTabs = new List<Tab>();
        }
        allTabs.Add(tab);
    }

    public Tab NewTab(string name, System.Action drawer)
    {
        Tab tab = new(name, drawer);
        NewTab(tab);
        return tab;
    }

    public void NewTabs(params Tab[] tabs)
    {
        foreach (Tab tab in tabs)
        {
            NewTab(tab);
        }
    }

    public void ClearTabs()
    {
        allTabs = null;
    }

    public void SetWindowTitle(string title)
    {
        titleContent = new GUIContent(title);
    }

    public sealed class Tab
    {
        public readonly string name;
        public readonly System.Action drawer;

        public Tab(string name, System.Action drawer)
        {
            this.name = name;
            this.drawer = drawer;
        }
    }
}