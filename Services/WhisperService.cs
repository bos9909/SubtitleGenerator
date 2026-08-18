using System.Diagnostics;

namespace SubtitleGenerator.Services;

public class WhisperService
{
    /// <summary>
    /// Python Whisper 스크립트를 실행하여
    /// wav 파일을 자막으로 변환한다.
    /// </summary>

    /// <summary>
    /// Python Whisper를 실행한다.
    /// </summary>
    public async Task<(bool Success, string Message)> GenerateSubtitleAsync(string audioPath)
    {
        ProcessStartInfo info = new()
        {
            FileName = "py",
            Arguments = $"Whisper/whisper.py \"{audioPath}\"",

            UseShellExecute = false,

            RedirectStandardOutput = true,
            RedirectStandardError = true,

            CreateNoWindow = true
        };

        using Process process = new()
        {
            StartInfo = info
        };

        process.Start();

        string output = await process.StandardOutput.ReadToEndAsync();
        string error = await process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        // 종료 코드도 함께 확인
        if (process.ExitCode != 0)
        {
            return (false,
                $"ExitCode : {process.ExitCode}\n\n{error}");
        }

        return (true, output);
    }

}