using SubtitleGenerator.ViewModels;
using System.Windows;

namespace SubtitleGenerator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // MainWindow가 사용할 ViewModel을 연결한다.
        // DataContext를 설정해야 XAML의 Binding이 동작한다.
        DataContext = new MainViewModel();
    }
}