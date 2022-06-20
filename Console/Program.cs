﻿Console.WriteLine("===================================== DEBUG INFO =====================================");
Console.WriteLine("Environment.CurrentDirectory: {0}", Environment.CurrentDirectory);
Console.WriteLine("Environment.SystemDirectory: {0}", Environment.SystemDirectory);
Console.WriteLine("Environment.OSVersion: {0}", Environment.OSVersion);
Console.WriteLine("Environment.UserName: {0}", Environment.UserName);
Console.WriteLine("Environment.MachineName: {0}", Environment.MachineName);
var machineName = Environment.MachineName; // Example: "AURORA-2021" vs "EC2AMAZON"
var isRunningLocally = machineName.Equals("AURORA-2021");
Console.WriteLine("isRunningLocally: {0}", isRunningLocally);

Console.WriteLine("===================================== CONSTANTS (FOR TEAMCITY) =====================================");
string workDir = @"%teamcity.build.checkoutDir%";

Console.WriteLine("===================================== CONSTANTS (FOR LOCAL TESTING) =====================================");
if (isRunningLocally)
{
    workDir = @"C:\work";
}

Console.WriteLine("=============================== CALC DATE-BASED VERSION ===============================");
var now = DateTime.UtcNow.AddHours(-4);
var year = now.Year.ToString();
var month = now.Month.ToString();
var day = now.Day.ToString();
var hour = (now.Hour * 100).ToString();
var minute = now.Minute.ToString();
var time = (now.Hour * 100) + now.Minute;
var version = $"{year}.{month}.{day}-{time}";
Console.WriteLine($"version: {version}");

Console.WriteLine("=============================== TARGET APP FILE NAME ===============================");
var appPrefix = "HelloTrainer";
var appVersion = version; // From preceding section.
var appExtension = ".exe";
var targetAppFileName = $"{appPrefix}.{appVersion}{appExtension}";
Console.WriteLine($"targetAppFileName: {targetAppFileName}");

Console.WriteLine("=============================== LOCATE SOURCE APP FILE ===============================");
var sourceAppFile = Directory.GetFiles(workDir, "*.exe", SearchOption.AllDirectories).FirstOrDefault();
Console.WriteLine($"sourceAppFile: {sourceAppFile}");

// Get just the path to the file.
var sourceAppFilePath = Path.GetDirectoryName(sourceAppFile);
Console.WriteLine($"sourceAppFilePath: {sourceAppFilePath}");


Console.WriteLine("=============================== MOVE/RENAME APP FILE ===============================");
// Copy sourceAppFileFullPath to targetAppFileName.
var targetAppFile = Path.Combine(sourceAppFilePath, targetAppFileName);
Console.WriteLine($"targetAppFile: {targetAppFile}");
File.Copy(sourceAppFile, targetAppFile, true);

// Verify that the file was copied.
var targetAppFileExists = File.Exists(targetAppFile);
Console.WriteLine($"targetAppFileExists: {targetAppFileExists}");