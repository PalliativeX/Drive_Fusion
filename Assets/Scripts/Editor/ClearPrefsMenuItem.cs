using UnityEditor;
using UnityEngine;

public static class ClearPrefsMenuItem
{
	[MenuItem("Tools/Clear Prefs &D")]
	public static void ClearPrefs()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log("Player prefs cleared!");
	}
}