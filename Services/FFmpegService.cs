using System.Diagnostics;

namespace SubtitleGenerator.Services
{
    /// <summary>
    /// FFmpeg를 실행하는 서비스.
    ///
    /// 현재는 FFmpeg 실행 및 버전 확인을 담당한다.
    /// </summary>
    public class FFmpegService
    {
        /// <summary>
        /// FFmpeg 버전 정보를 가져온다.
        /// </summary>
        public async Task<string> GetVersionAsync()
        {
            ProcessStartInfo startInfo = new()
            {
                // 프로젝트 내부 Tools 폴더의 FFmpeg 실행 파일
                FileName = @"Tools\ffmpeg.exe",

                Arguments = "-version",

                // 직접 실행
                UseShellExecute = false,

                // 출력 읽기 허용
                RedirectStandardOutput = true,

                // 오류 출력 읽기 허용
                RedirectStandardError = true,

                // 콘솔창 숨김
                CreateNoWindow = true
            };

            using Process process = new()
            {
                StartInfo = startInfo
            };

            process.Start();

            // stdout, stderr 둘 다 읽는다.
            Task<string> outputTask = process.StandardOutput.ReadToEndAsync();
            Task<string> errorTask = process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            string output = await outputTask;
            string error = await errorTask;

            // FFmpeg는 stderr에 정상 출력하는 경우가 있으므로 둘 다 확인
            if (!string.IsNullOrWhiteSpace(output))
            {
                return output;
            }

            return error;
        }

        /// <summary>
        /// 영상 파일에서 음성만 추출하여 wav 파일을 생성한다.
        /// FFmpeg를 사용해서 영상 스트림을 제거하고 오디오만 저장한다.
        /// </summary>
        public async Task<bool> ExtractAudioAsync(
            string inputPath,
            string outputPath)
        {
            // FFmpeg 실행 설정
            ProcessStartInfo info = new()
            {
                // 프로젝트 내부 Tools 폴더의 FFmpeg 실행 파일
                FileName = @"Tools\ffmpeg.exe",

                // 실행 옵션
                // -i : 입력 파일
                // -vn : 영상 스트림 제거
                // -acodec pcm_s16le : wav에서 사용하는 PCM 형식
                Arguments =
                    $"-i \"{inputPath}\" " +
                    "-vn " +
                    "-acodec pcm_s16le " +
                    $"\"{outputPath}\"",

                // 콘솔창 직접 실행 방지
                UseShellExecute = false,

                // FFmpeg 출력 읽기
                RedirectStandardOutput = true,

                // FFmpeg 오류 출력 읽기
                RedirectStandardError = true,

                // 콘솔창 숨김
                CreateNoWindow = true
            };


            // 프로세스 생성
            using Process process = new()
            {
                StartInfo = info
            };


            // FFmpeg 실행
            process.Start();

            // FFmpeg 출력 수집
            Task<string> outputTask =
                process.StandardOutput.ReadToEndAsync();

            Task<string> errorTask =
                process.StandardError.ReadToEndAsync();


            // 종료까지 대기
            await process.WaitForExitAsync();


            // 출력 결과 가져오기
            string output =
                await outputTask;

            string error =
                await errorTask;


            // 종료 코드 0이면 성공
            return process.ExitCode == 0;
        }
    }
}