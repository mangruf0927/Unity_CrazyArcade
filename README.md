# 💣 CrazyArcade (모작) 

<img width="600" alt="스크린샷 2025-05-15 오후 2 59 12" src="https://github.com/user-attachments/assets/0e95b4a6-794c-4dd1-b9c0-a97483ba4cfd" />

Unity로 제작한 **크레이지 아케이드 : 문어 대습격 모드 모작**입니다.      
문어 대습격 모드는 총 3개의 스테이지로 구성되어 있으며 각 스테이지마다 등장하는 버럭이와 쫄쫄이 몬스터들을 물리치면 마지막 보스 스테이지로 진입할 수 있습니다. 

유니티 버전 : **Unity 2022.3.9f1**   

[🎮 플레이 영상](https://www.youtube.com/watch?v=mxeJWvXusBQ)   
[📄 개발 문서](https://drive.google.com/file/d/1iaQQgqZrtdyEXk91NMVVFYbqH5sVrqhz/view?usp=sharing)


## 🔧 주요 구현 내용
- FSM(Finite State Machine)을 활용하여 플레이어, 몬스터, 물풍선의 상태 제어
- 이중 배열 기반 타일맵 시스템으로 물풍선 설치 및 아이템, 블록 등 오브젝트 배치 관리 
- 팩토리 패턴을 통해 플레이어 생성 및 초기 설정 자동화
- 전략 패턴으로 적의 행동 전략을 유연하게 설계
