using System.Diagnostics;

namespace PadZex.Core
{
	public static class Debug
	{
		public static void Log(string line) => System.Diagnostics.Debug.WriteLine(line);
		public static void Log(object line) => System.Diagnostics.Debug.WriteLine(line);
	}
}
