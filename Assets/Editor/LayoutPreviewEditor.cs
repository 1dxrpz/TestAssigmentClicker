
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CustomEditor(typeof(LayoutPreview))]
public class LayoutPreviewEditor : Editor
{
	private const string PrefKey = "UILayoutPreview.Enabled";

	private static bool PreviewEnabled
	{
		get => EditorPrefs.GetBool(PrefKey, true);
		set => EditorPrefs.SetBool(PrefKey, value);
	}

	[InitializeOnLoadMethod]
	private static void Initialize()
	{
		SceneView.duringSceneGui -= OnSceneGUI;
		SceneView.duringSceneGui += OnSceneGUI;
	}

	private static void OnSceneGUI(SceneView sceneView)
	{
		Handles.BeginGUI();

		var menuRect = new Rect(10, 10, 180, 24);

		GUILayout.BeginArea(menuRect);
		var enabled = EditorGUILayout.ToggleLeft(
			"UI Layout Preview",
			PreviewEnabled
		);
		GUILayout.EndArea();

		if (enabled != PreviewEnabled)
		{
			PreviewEnabled = enabled;
			SceneView.RepaintAll();
		}

		Handles.EndGUI();
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
	}

	[DrawGizmo(
		GizmoType.Selected |
		GizmoType.NonSelected |
		GizmoType.InSelectionHierarchy)]
	private static void DrawUIOverlay(
		LayoutPreview preview,
		GizmoType gizmoType)
	{
		if (!PreviewEnabled || Application.isPlaying || preview == null)
			return;

		if (!(preview.transform is RectTransform rectTransform))
			return;

		var corners = new Vector3[4];
		rectTransform.GetWorldCorners(corners);

		var previousZTest = Handles.zTest;
		Handles.zTest = CompareFunction.Always;

		var outline = preview.color;
		outline.a = 1f;

		Handles.DrawSolidRectangleWithOutline(
			corners,
			preview.color,
			outline
		);

		Handles.zTest = previousZTest;
	}
}
