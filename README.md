SubtitleGenerator

動画を選択するだけで、FFmpegによる音声抽出、Whisperによる高精度な音声認識（STT）、そしてFFmpegによる字幕のハードサブ焼き付けまでをワンストップで自動処理する、モダンなC# WPFデスクトップアプリケーションです。
🚀 主な機能 (Key Features)

    動画ファイルの直感的な選択: ダイアログを通じた簡単な動画ファイル選択およびプレビュー表示。

    非同期パイプライン処理 (async/await): 重い動画・音声処理中もUIが固まらず、スムーズな操作性を維持。

    リアルタイム進捗トラッキング (IProgress<T>):

        音声抽出 (0% ~ 40%)

        Whisperによる字幕生成 (40% ~ 70%)

        動画への字幕ハードサブ合成 (70% ~ 100%)
        を段階的に正確なプログレスバーで可視化。

    外部ツール連携: FFmpegおよびWhisperの実行状態やエラーログをUI上でリアルタイム確認可能。

🛠 技術スタック (Tech Stack)

    言語: C# (.NET 8.0-windows)

    フレームワーク: WPF (Windows Presentation Foundation)

    アーキテクチャ: MVVM (Model-View-ViewModel)

    主要ライブラリ:

        CommunityToolkit.Mvvm ([ObservableProperty], [RelayCommand] によるボイラープレート削減)

    外部ツール / エンジン:

        FFmpeg: 音声抽出・字幕合成

        Whisper: 音声文字起こし (STT)

📂 アーキテクチャの設計 (Architecture)

保守性と拡張性を高めるため、責務ごとに明確に分離された設計を採用しています。

    Views/: ユーザーインターフェース (MainWindow.xaml)

    ViewModels/: 画面の状態管理とコマンドロジック (MainViewModel.cs)

    Services/: 外部プロセス制御およびビジネスロジック

        FileDialogService: ファイル選択管理

        FFmpegService: FFmpegラッパー（音声抽出・合成）

        WhisperService: Whisperラッパー（SRT字幕生成）
