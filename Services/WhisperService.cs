using System.Diagnostics;

namespace SubtitleGenerator.Services;

public class WhisperService
{
    public async Task<string> RunWhisperAsync(string audioPath)
    {
        ProcessStartInfo info = new()
        {
            // Windows Python Launcher 사용
            FileName = "py",

            // Python 스크립트와 음성 파일 전달
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


        string output =
            await process.StandardOutput.ReadToEndAsync();

        string error =
            await process.StandardError.ReadToEndAsync();


        await process.WaitForExitAsync();


        return output + error;
    }
}