using System;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Xml;

namespace BoTech.UI.Forms.tools;

public class UpdateMaterialIcons
{
    /// <summary>
    /// This is the main method which creates an updated csharp file.
    /// IMPORTANT: please enter your custom output paths. Both dictionaries must be empty.
    /// </summary>
    public static void Main()
    {
        string tempPathRepo = @"C:\Temp\MaterialDesign\Repo\";
        string tempPathCSharp = @"C:\Temp\MaterialDesign\CSharp\";
        new UpdateMaterialIcons().RunUpdate(tempPathRepo, tempPathCSharp);
    }
    /// <summary>
    /// This repo contains all icon created by https://pictogrammers.com
    /// </summary>
    private static readonly string RepositoryUrl = "https://github.com/Templarian/MaterialDesign-SVG.git";
    /// <summary>
    /// The constant header of the file (containing the current date-time stamp)
    /// </summary>
    private static readonly string TopOfCSharpFile =
        "using System;\r\nusing System.Collections.Generic;\r\nusing System.Text;\r\n\r\nnamespace BoTech.UI.Forms.Models\r\n{\r\n    public enum Icon\r\n    {";
    
    private readonly string BottomOfCSharpFile = "\r\n    }\r\n}\r\n";
    /// <summary>
    /// This method does the following:
    /// 1. creating all dictionaries.
    /// 2. cloning the repository.
    /// 3. converts each icon file into a csharp enum entry.
    /// 4. Writes the output to the given dictionary.
    /// </summary>
    /// <param name="tempPathRepo">the temp path for cloning the repo (No cleanup)</param>
    /// <param name="targetCSharpOutputPath">Where the csharp files should be stored in.</param>
    public void RunUpdate(string tempPathRepo, string targetCSharpOutputPath)
    {
        Directory.CreateDirectory(tempPathRepo);
        Directory.CreateDirectory(targetCSharpOutputPath);
        CloneRepository(RepositoryUrl, tempPathRepo);
        DirectoryInfo svgDirectory = GetSvgDirectoryFromRepository(tempPathRepo);
        
        FileInfo[] iconFiles = svgDirectory.GetFiles();
        string csharpFileResult = "";

        ConsoleProgressBarNew bar = new ConsoleProgressBarNew(iconFiles.Length);


        string iconDocString = "";
        string iconName = "";
        for(int fileIndex = 0; fileIndex < iconFiles.Length; fileIndex++)
        {
            bar.Update(fileIndex);
            bar.Update("Converting svg to c#:  " + iconFiles[fileIndex].Name);
            iconName = iconFiles[fileIndex].Name.Replace(".svg", "");
            iconDocString = $"\n        /// <summary>\r\n        /// <see href=\"https://pictogrammers.com/library/mdi/icon/{iconName}/\">See Icon at pictogrammers.com</see>\r\n        /// </summary>";
            csharpFileResult += iconDocString + "\n        " + FormatIconName(iconName) + ",";
        }
        
        bar.Dispose();
        
        Console.WriteLine($"Saving to {targetCSharpOutputPath + "Icons.cs"}...");
        File.WriteAllText(targetCSharpOutputPath + "Icons.cs", TopOfCSharpFile + csharpFileResult + BottomOfCSharpFile);
        
        Console.WriteLine("Done!");
    }
    /// <summary>
    /// Since the icon files contain hyphens ("-") and exclusively lowercase letters, this method converts the format so that each hyphen is followed by an uppercase letter.
    /// For instance this method converts the name "access-point" to AccessPoint
    /// </summary>
    /// <param name="iconName">the name to convert</param>
    /// <returns>the formatted name</returns>
    public string FormatIconName(string iconName)
    {
        StringBuilder result = new StringBuilder();
        bool nextCharToUpper = true;
        foreach (char c in iconName)
        {
            if (c == '-')
            {
                nextCharToUpper = true;
            }
            else
            {
                if (nextCharToUpper)
                {
                    result.Append(char.ToUpperInvariant(c));
                    nextCharToUpper = false;
                }
                else
                {
                    result.Append(c);
                }
            }
        }
        return result.ToString();
    }
    /// <summary>
    /// Returns the directory containing all svg files.
    /// </summary>
    /// <param name="targetPath"></param>
    /// <returns></returns>
    /// <exception cref="Exception">directory not found</exception>
    private DirectoryInfo GetSvgDirectoryFromRepository(string targetPath)
    {
        string svgDirectory = Path.Combine(targetPath, "svg");

        if (!Directory.Exists(svgDirectory))
        {
            throw new Exception($"SVG directory not found: {svgDirectory}");
        }
        return new DirectoryInfo(svgDirectory);
    }
    /// <summary>
    /// Clones any repository to the given path. (Console result of git process will be displayed)
    /// </summary>
    /// <param name="repositoryUrl">The repo to clone</param>
    /// <param name="targetPath">The path which should contain the repo.</param>
    /// <exception cref="InvalidOperationException">Git error</exception>
    /// <exception cref="Exception">Git error</exception>
    private void CloneRepository(string repositoryUrl, string targetPath)
    {
        Console.WriteLine($"Cloning {repositoryUrl} ...");

        var cloneProcessStartInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = $"clone {repositoryUrl} {targetPath}",
            UseShellExecute = false,
            CreateNoWindow = false
        };

        using var cloneProcess = Process.Start(cloneProcessStartInfo);

        if (cloneProcess == null)
        {
            throw new InvalidOperationException("Failed to start git process.");
        }
        
        cloneProcess.WaitForExit();

        if (cloneProcess.ExitCode != 0)
        {
            throw new Exception($"Git clone failed.");
        }

        Console.WriteLine("Cloning DONE");
    }
   
    public class ConsoleProgressBarNew : IDisposable
    {
        private static readonly string[] RunningIndicator = { "🞅", "🞆", "🞇", "🞈", "🞉", "🞈", "🞇", "🞆" };

        private readonly Lock _consoleLock = new();
        private readonly int _maximum;
        private readonly int _runningIndicatorTopPosition;
        private readonly Thread _animationThread;

        private volatile int _percentage = 0;
        private int _oldMessageLength = 0;
        private volatile bool _disposed = false;

        public ConsoleProgressBarNew(int maximum)
        {
            _maximum = maximum;
            _runningIndicatorTopPosition = Console.CursorTop;
            Console.CursorVisible = false;
            _animationThread = new Thread(Animate) { IsBackground = true };
            _animationThread.Start();
        }

        public void Update(int value)
        {
            _percentage = 100 * value / _maximum;

            lock (_consoleLock)
            {
                Console.SetCursorPosition(0, _runningIndicatorTopPosition + 1);
                string filled = new string('═', _percentage);
                string notFilled = new string('━', 100 - _percentage);
                Console.Write(filled + '▶' + notFilled);
            }
        }

        public void Update(string message)
        {
            lock (_consoleLock)
            {
                Console.SetCursorPosition(0, _runningIndicatorTopPosition);
                Console.Write(message + new string(' ', Math.Max(0, _oldMessageLength - message.Length)));
                _oldMessageLength = message.Length;
            }
        }

        private void Animate()
        {
            int currentIndicatorIndex = 0;

            while (!_disposed)
            {
                lock (_consoleLock)
                {
                    if (_disposed) break;

                    Console.SetCursorPosition(101, _runningIndicatorTopPosition + 1);
                    Console.Write(" " + _percentage + "% " + RunningIndicator[currentIndicatorIndex]);
                }

                currentIndicatorIndex = (currentIndicatorIndex + 1) % RunningIndicator.Length;
                Thread.Sleep(150);
            }
        }

        public void Dispose()
        {
            _disposed = true;
            _animationThread.Join();
            Console.CursorVisible = true;
            Console.WriteLine("\n");
        }
    }
}