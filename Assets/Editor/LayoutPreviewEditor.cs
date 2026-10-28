using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CustomEditor(typeof(LayoutPreview))]
public class LayoutPreviewEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
	}

	[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected | GizmoType.InSelectionHierarchy)]
	private static void DrawUIOverlay(
		LayoutPreview preview,
		GizmoType gizmoType)
	{
		if (Application.isPlaying || preview == null)
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