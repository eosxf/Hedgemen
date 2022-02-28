using System;
using System.Diagnostics;
using System.Text;

namespace Hgm.Utilities;

public class DefaultLogger : ILogger
{
	private readonly StringBuilder builder;

	public DefaultLogger()
	{
		builder = new StringBuilder();
	}

	public LogLevel LogLevel { get; set; } = LogLevel.Debug;

	public string Format { get; set; } = "[%T] [%c:%m:%l/%S] %M";

	public string DateTimeFormat { get; set; } = "HH:mm:ss";

	public void Debug(string message)
	{
		if (!ValidLogLevel(LogLevel.Debug)) return;
		var stackFrame = new StackFrame(1, true);
		Add(message, LogLevel.Debug, stackFrame);
	}

	public void Warn(string message)
	{
		if (!ValidLogLevel(LogLevel.Warn)) return;
		var stackFrame = new StackFrame(1, true);
		Add(message, LogLevel.Warn, stackFrame);
	}

	public void Error(string message)
	{
		if (!ValidLogLevel(LogLevel.Error)) return;
		var stackFrame = new StackFrame(1, true);
		Add(message, LogLevel.Error, stackFrame);
	}

	public void Critical(string message)
	{
		if (!ValidLogLevel(LogLevel.Critical)) return;
		var stackFrame = new StackFrame(1, true);
		Add(message, LogLevel.Critical, stackFrame);
	}

	public void Add(string message, LogLevel logLevel)
	{
		if (!ValidLogLevel(logLevel)) return;
		var stackFrame = new StackFrame(1, true);
		Add(message, logLevel, stackFrame);
	}

	private void Add(string message, LogLevel logLevel, StackFrame stackFrame)
	{
		var oldConsoleColor = Console.ForegroundColor;
		var consoleColor = ConsoleColor.White;
		switch (logLevel)
		{
			case LogLevel.Debug: break;
			case LogLevel.Warn:
				consoleColor = ConsoleColor.Yellow;
				break;
			case LogLevel.Error:
				consoleColor = ConsoleColor.Red;
				break;
			case LogLevel.Critical:
				consoleColor = ConsoleColor.Blue;
				break;
		}

		var dateTime = DateTime.Now.ToString(DateTimeFormat);
		var className = stackFrame.GetMethod() != null ? stackFrame.GetMethod().DeclaringType.Name : "null";
		var fileName = stackFrame.GetFileName();
		var methodName = stackFrame.GetMethod() != null ? stackFrame.GetMethod().Name : "null";
		var lineNumber = stackFrame.GetFileLineNumber();

		var consoleMessage = Format
			.Replace("%S", logLevel.ToString())
			.Replace("%T", dateTime)
			.Replace("%c", className)
			.Replace("%n", fileName)
			.Replace("%m", methodName)
			.Replace("%l", lineNumber.ToString())
			.Replace("%M", message);

		Console.ForegroundColor = consoleColor;
		Console.WriteLine(consoleMessage);
		Console.ForegroundColor = oldConsoleColor;

		builder.Append(consoleMessage).Append('\n');
	}

	private bool ValidLogLevel(LogLevel logLevel)
	{
		return logLevel >= LogLevel;
	}
}