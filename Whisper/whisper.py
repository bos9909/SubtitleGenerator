# faster-whisper 모델을 사용하기 위한 라이브러리
from faster_whisper import WhisperModel

# C#에서 전달받은 인자를 사용하기 위해 필요
import sys

# 파일 경로 처리를 위해 사용
import os


def main():

    # C#에서 wav 파일 경로를 전달받는다.
    if len(sys.argv) < 2:
        print("음성 파일 경로가 없습니다.")
        return


    audio_path = sys.argv[1]


# SRT 저장 폴더
# 입력 파일과 같은 폴더를 결과 저장 위치로 사용
output_folder = os.path.dirname(audio_path)

# 입력 파일 이름만 가져온다.
file_name = os.path.splitext(os.path.basename(audio_path))[0]

# sample.srt 생성
output_path = os.path.join(output_folder,file_name + ".srt")

# 폴더가 없으면 생성
os.makedirs(output_folder, exist_ok=True )


# Whisper 모델 로드
#
# base:
# - 테스트하기 적당한 크기
# - CPU에서도 실행 가능
#
model = WhisperModel(
    "base",
    device="cpu",
    compute_type="int8"
)


    print("음성 분석 시작")


    # 음성 인식 실행
    segments, info = model.transcribe(audio_path)


    print( f"감지 언어: {info.language}" )


    # 생성할 SRT 파일 경로
    output_path = os.path.join(output_folder,"subtitle.srt")


    # SRT 생성
    with open(
        output_path,
        "w",
        encoding="utf-8"
    ) as file:


        # Whisper 결과는 여러 segment로 나뉘어 반환된다.
        for index, segment in enumerate(segments):

            # SRT 번호
            file.write(
                f"{index + 1}\n"
            )


            # 시작 시간 ~ 종료 시간
            file.write(
                f"{convert_time(segment.start)} --> "
                f"{convert_time(segment.end)}\n"
            )


            # 인식된 문장
            file.write(
                f"{segment.text.strip()}\n\n"
            )


    print(
        f"SRT 생성 완료: {output_path}"
    )



# 초 단위를 SRT 시간 형식으로 변경
def convert_time(seconds):

    hour = int(seconds // 3600)

    minute = int(
        (seconds % 3600) // 60
    )

    second = int(
        seconds % 60
    )

    millisecond = int(
        (seconds % 1) * 1000
    )


    return (
        f"{hour:02}:"
        f"{minute:02}:"
        f"{second:02},"
        f"{millisecond:03}"
    )



if __name__ == "__main__":
    main()