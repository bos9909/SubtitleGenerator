# faster-whisper 설치 확인용 코드
from faster_whisper import WhisperModel

# 테스트용으로 작은 모델 사용
model = WhisperModel(
    "base",
    device="cpu",
    compute_type="int8"
)