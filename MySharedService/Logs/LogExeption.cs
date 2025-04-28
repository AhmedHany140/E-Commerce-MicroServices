

using Serilog;

namespace ecommerce.shared.Logs
{
	public static class LogExeption
	{
		public static void LogExeptions(Exception ex)
		{
			LogToFile(ex.Message);
			LogToConsole(ex.Message);
			LogToDebugger(ex.Message);
		}

		public static void LogToFile(string message) => Log.Information(message);


		public static void LogToDebugger(string message) => Log.Debug(message);

		public static void LogToConsole(string message) => Log.Warning(message);
		
	}
}
