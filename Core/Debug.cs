using System.Diagnostics;

namespace PadZex.Core
{
	public class Debug
	{
		public void Log(string line) => System.Diagnostics.Debug.WriteLine(line);
		public void Log(object line) => System.Diagnostics.Debug.WriteLine(line);
	}
}
