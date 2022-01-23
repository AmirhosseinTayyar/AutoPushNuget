using System.Diagnostics;

var nugetServer = string.Empty;
var packageDirectoryPath = string.Empty;

Console.WriteLine("Please Enter Nuget Server Upload Address :");
nugetServer = Console.ReadLine();
Console.WriteLine("Please Enter Package Directory Path :");
packageDirectoryPath = Console.ReadLine();
Console.WriteLine();

Console.Write("Start Finding Packages ... ");
string[] files = Directory.GetFiles(packageDirectoryPath, "*.nupkg", SearchOption.AllDirectories);

Console.WriteLine("Finish Finding Packages");
Console.WriteLine();


Console.WriteLine("*** Start Uploading Packages ***");
Console.WriteLine();

foreach (string file in files)
{
    Process process = new();
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.UseShellExecute = false;
    process.StartInfo.RedirectStandardInput = true;
    process.StartInfo.RedirectStandardOutput = true;
    process.StartInfo.FileName = "cmd.exe";

    process.Start();
    Console.Write($"Start Uploading {Path.GetFileName(file)} Package ... ");

    process.StandardInput.WriteLine($"dotnet nuget push -s {nugetServer} {Path.GetFullPath(file)}");
    TimeSpan timeSpan = new TimeSpan(0, 0, 5);
    Thread.Sleep(timeSpan);

    process.Close();
    Console.Write($"Finish Uploading .");

    Console.WriteLine();
}

Console.WriteLine("*** Finish Uploading Packages ***");
Console.WriteLine();
Console.ReadKey();

