using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubtitleGenerator.Services;

namespace SubtitleGenerator.ViewModels;

/// <summary>
/// 메인 화면에서 사용하는 ViewModel.
/// 화면에 표시되는 데이터와 버튼 동작을 관리한다.
/// </summary>
public partial class MainViewModel : ObservableObject
{
    /// <summary>
    /// 파일 선택 기능을 담당하는 서비스.
    /// 현재는 ViewModel에서 직접 생성한다.
    /// 나중에 프로젝트가 커지면 DI(의존성 주입)로 개선할 예정이다.
    /// </summary>
    private readonly FileDialogService _fileDialogService = new();

    /// <summary>
    /// 현재 선택된 영상 파일의 경로.
    /// 화면(TextBlock)과 Binding되어 있다.
    /// 값이 변경되면 화면도 자동으로 갱신된다.
    /// </summary>
    [ObservableProperty]
    private string videoPath = "선택된 파일 없음";

    /// <summary>
    /// 현재 프로그램의 작업 상태.
    /// 화면의 TextBlock과 Binding된다.
    /// </summary>
    [ObservableProperty]
    private string status = "대기 중";

    /// <summary>
    /// 작업 진행률(0~100).
    /// ProgressBar와 Binding된다.
    /// </summary>
    [ObservableProperty]
    private int progress = 0;


    /// <summary>
    /// "영상 선택" 버튼을 눌렀을 때 실행되는 명령(Command).
    /// </summary>
    [RelayCommand]
    private void SelectVideo()
    {
        // 현재 상태를 화면에 표시
        Status = "파일 선택 중...";

        // 파일 선택 창을 띄운다.
        string? file = _fileDialogService.OpenVideoFile();

        // 사용자가 취소하지 않았다면 화면에 표시할 경로를 변경한다.
        if (file != null)
        {
            VideoPath = file;

            // 파일 선택 완료
            Status = "파일 선택 완료";
        }
        else
        {
            //취소한 경우
            Status = "취소됨";
            
        }

        // 현재는 작업이 끝났으므로 진행률 100%
        Progress = 100;
    }
}