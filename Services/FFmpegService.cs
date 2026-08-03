using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SubtitleGenerator.Services
{
    /// <summary>
    /// FFmpeg를 실행하는 서비스.
    ///
    /// 현재는 FFmpeg가 정상적으로 실행되는지만 확인한다.
    /// 나중에는
    /// - 음성 추출
    /// - 자막 삽입
    /// - 영상 변환
    /// 등의 기능을 추가할 예정이다.
    /// </summary>
    public class FFmpegService
    {
        /// <summary>
        /// FFmpeg의 버전 정보를 가져온다.
        ///
        /// 실행에 성공하면
        /// 버전 문자열을 반환한다.
        ///
        /// 실패하면
        /// 오류 메시지를 반환한다.
        /// </summary>
        public string GetVersion()
        {
            // Process는 실행되는 프로그램 자체를 나타낸다.
            Process process = new();

            // ProcessStartInfo는 실행하기 전에 필요한 설정을 담는 객체이다.
            process.StartInfo = new ProcessStartInfo();

            // 실행할 프로그램의 경로.
            process.StartInfo.FileName = @"Tools\ffmpeg.exe";

            // ffmpeg에게 전달할 명령줄 옵션.
            // "-version"은 버전 정보만 출력하고 종료하는 옵션이다.
            process.StartInfo.Arguments = "-version";

            // 여기까지는 설정만 했다.
            // 아직 실행은 하지 않았다.

            return "여기까지 완료";
        }
    }
}
