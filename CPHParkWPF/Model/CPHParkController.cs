using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPHParkWPF.Model;
public static class CPHParkController {

    public static async Task<string> RunInit(string locationsPath, string jsonPath) {
        string programPath = Configuration.Singleton.GetPath("cphpark");

        (StreamReader consoleOut, StreamReader consoleErr) = await RunProcess(programPath, $"--database \"{jsonPath}\" initialize --locations-to-count \"{locationsPath}\"");

        //Should do some formatting.

        return consoleOut.ReadToEnd() + "\n" + consoleErr.ReadToEnd();
    }

    public static async Task<string> RunMigrate(string altAllePath, string jsonPath) {
        string programPath = Configuration.Singleton.GetPath("cphpark");

        (StreamReader consoleOut, StreamReader consoleErr) = await RunProcess(programPath, $"--database \"{jsonPath}\" migration --alt-alle \"{altAllePath}\"", false);

        //Should do some formatting.

        return consoleOut.ReadToEnd() + "\n" + consoleErr.ReadToEnd();
    }

    public static async Task<string> RunAdd(string filePath, string jsonPath, string timeframe) {
        string programPath = Configuration.Singleton.GetPath("cphpark");

        (StreamReader consoleOut, StreamReader consoleErr) = await RunProcess(programPath, $"--database \"{jsonPath}\" add --input-folder \"{filePath}\" --timeframe \"{timeframe}\"", true);

        //Should do some formatting.

        return consoleOut.ReadToEnd() + "\n" + consoleErr.ReadToEnd();
    }

    public static async Task<string> RunReport(string outputFolder, string jsonPath, string fileName, string reportType) {
        string programPath = Configuration.Singleton.GetPath("cphpark");

        string filePath = Path.Combine(outputFolder, fileName+".xlsx");
        (StreamReader consoleOut, StreamReader consoleErr) = await RunProcess(programPath, $"--database \"{jsonPath}\" report --report-type \"{reportType}\" --output \"{filePath}\"", true);

        //Should do some formatting.

        return consoleOut.ReadToEnd() + "\n" + consoleErr.ReadToEnd();
    }

    private static async Task<(StreamReader, StreamReader)> RunProcess(string exe_path, string args, bool redirectOutputs = true) {

        try {
            return await Task.Run(async () => {
                ProcessStartInfo startInfo = new ProcessStartInfo(exe_path);
                startInfo.CreateNoWindow = redirectOutputs;
                startInfo.UseShellExecute = false;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.Arguments = args;
                startInfo.RedirectStandardOutput = redirectOutputs;
                startInfo.RedirectStandardError = redirectOutputs;

                using (Process? exeProcess = Process.Start(startInfo)) {
                    if (exeProcess != null) {
                        StreamReader commandLineOutput = new StreamReader(Stream.Null);
                        StreamReader commandLineError = new StreamReader(Stream.Null);
                        if (redirectOutputs) {
                            commandLineOutput = exeProcess.StandardOutput;
                            commandLineError = exeProcess.StandardError;
                        }

                        await exeProcess.WaitForExitAsync();
                        return (commandLineOutput, commandLineError);
                    }
                    throw new Exception("exeProcess was null");
                }
            });
            throw new Exception($"Failed to get output and error from process running the program {exe_path} with the arguments {args}");
        } catch {
            // Log error.
            throw;
        }
    }

    /*
    private static async Task<(StreamReader, StreamReader)> RunCommand(string command, bool redirectOutputs = true) {

        try {
            return await Task.Run(async () => {
                ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", "/c" + command);
                startInfo.CreateNoWindow = redirectOutputs;
                startInfo.UseShellExecute = false;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.WorkingDirectory = "";
                startInfo.RedirectStandardOutput = redirectOutputs;
                startInfo.RedirectStandardError = redirectOutputs;

                using (Process? exeProcess = Process.Start(startInfo)) {
                    if (exeProcess != null) {
                        StreamReader commandLineOutput = new StreamReader(Stream.Null);
                        StreamReader commandLineError = new StreamReader(Stream.Null);
                        if (redirectOutputs) {
                            commandLineOutput = exeProcess.StandardOutput;
                            commandLineError = exeProcess.StandardError;
                        }

                        await exeProcess.WaitForExitAsync();
                        return (commandLineOutput, commandLineError);
                    }
                    throw new Exception("exeProcess was null");
                }
            });
        } catch {
            // Log error.
            throw;
        }
    }
    */
}
