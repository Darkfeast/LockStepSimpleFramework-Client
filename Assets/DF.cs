using UnityEngine;
using System.IO;
using System.Text;

public enum E_ColorType
{
	Init,
	Normal,
	UI,
	Temp,
	Err,
	Over
}
public class DF : MonoBehaviour
{
	static StringBuilder sb = new StringBuilder();
	public static bool isRecord = false;
	public static bool isPrint=true;
	
	public static void Log(object str, E_ColorType c = E_ColorType.Normal)
	{
		string formatStr = "";
		if (!isPrint) return;
		
		if (c == E_ColorType.Init)
		{
			formatStr = "<color=cyan>" + str + "</color>";
			Debug.Log(formatStr);
		}
		else if (c == E_ColorType.Normal)
		{
			formatStr = "<color=#00c0ff>" + str + "</color>";
			Debug.Log(formatStr);
		}
		else if (c == E_ColorType.UI)//#00FF21a0
		{
			formatStr = "<color=#91EC17>>>>>>>> " + str + "  -----------------</color>";
			Debug.Log(formatStr);
		}
		else if (c == E_ColorType.Temp)
		{
			formatStr = "<color=magenta>>>>>>>> " + str + "  -----------------</color>";
			Debug.Log(formatStr);
		}
		else if (c == E_ColorType.Err)
		{
			formatStr = "<color=#C94A4A>---------err--" + str + "</color>";//FF0000  C94A4A
			Debug.Log(formatStr);
		}
		else if (c == E_ColorType.Over)
		{
			formatStr = "<color=#FFA662> =====" + str + "=====</color>";
			Debug.Log(formatStr); //2F5283   
		}
		
		if(isRecord)
			sb.Append(str+"\n");
	}
	public static string LogLine(E_ColorType c = E_ColorType.Normal)
	{
		string str = "---------------------";
		Log(str,c);
		return str;
	}

	public static void DrawLineY(float y,float t=30,float disX=1000)
    {
        Debug.DrawLine(new Vector3(-disX,y,0), new Vector3(disX,y,0),Color.cyan,t);
    }
    
	
#if UNITY_EDITOR
	[System.Diagnostics.DebuggerNonUserCode]
	[System.Diagnostics.DebuggerStepThrough]
	[UnityEditor.Callbacks.OnOpenAssetAttribute(0)]
	static bool OnOpenAsset(int instanceID, int line)
	{
		string stackTrace = GetStackTrace();
		if (!string.IsNullOrEmpty(stackTrace) && stackTrace.Contains("DF:Log"))
		{
			var matches = System.Text.RegularExpressions.Regex.Match(stackTrace, @"\(at (.+)\)",
				System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			string pathLine = "";
			while (matches.Success)
			{
				pathLine = matches.Groups[1].Value;

				if (!pathLine.Contains("DF.cs"))
				{
					int splitIndex = pathLine.LastIndexOf(":");
					// 脚本路径
					string path = pathLine.Substring(0, splitIndex);
					// 行号
					line = System.Convert.ToInt32(pathLine.Substring(splitIndex + 1));
					string fullPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets"));
					fullPath = fullPath + path;
					// 跳转到目标代码的特定行
					UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(fullPath.Replace('/', '\\'), line);
					break;
				}
				matches = matches.NextMatch();
			}
			return true;
		}
		return false;
	}

	/// <summary>
	/// 获取当前日志窗口选中的日志的堆栈信息
	/// </summary>
	/// <returns></returns>
	static string GetStackTrace()
	{
		// 通过反射获取ConsoleWindow类
		var ConsoleWindowType = typeof(UnityEditor.EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow");
		// 获取窗口实例
		var fieldInfo = ConsoleWindowType.GetField("ms_ConsoleWindow",
			System.Reflection.BindingFlags.Static |
			System.Reflection.BindingFlags.NonPublic);
		var consoleInstance = fieldInfo.GetValue(null);
		if (consoleInstance != null)
		{
			if ((object)UnityEditor.EditorWindow.focusedWindow == consoleInstance)
			{
				// 获取m_ActiveText成员
				fieldInfo = ConsoleWindowType.GetField("m_ActiveText",
					System.Reflection.BindingFlags.Instance |
					System.Reflection.BindingFlags.NonPublic);
				// 获取m_ActiveText的值
				string activeText = fieldInfo.GetValue(consoleInstance).ToString();
				return activeText;
			}
		}
		return null;
	}
#endif


}


