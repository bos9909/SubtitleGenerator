using Microsoft.Win32;

namespace SubtitleGenerator.Services;

/// <summary>
/// Windows의 파일 선택 창(OpenFileDialog)을 관리하는 클래스.
/// 
/// 이 클래스의 역할은
/// "영상 파일을 선택해서 경로를 반환하는 것"뿐이다.
/// 화면을 변경하거나 데이터를 저장하지 않는다.
/// </summary>
public class FileDialogService
{
    /// <summary>
    /// 영상 파일을 선택하는 창을 띄운다.
    /// </summary>
    /// <returns>
    /// 선택한 파일의 전체 경로.
    /// 취소하면 null을 반환한다.
    /// </returns>
    public string? OpenVideoFile()
    {
        OpenFileDialog dialog = new();

        // 사용자가 선택할 수 있는 파일 형식
        dialog.Filter =
            "Video Files|*.mp4;*.avi;*.mov;*.mkv|All Files|*.*";

        // 다이얼로그를 띄운다.
        bool? result = dialog.ShowDialog();

        // 사용자가 확인을 눌렀다면 파일 경로를 반환한다.
        if (result == true)
        {
            return dialog.FileName;
        }

        // 취소했다면 null 반환
        return null;
    }
}