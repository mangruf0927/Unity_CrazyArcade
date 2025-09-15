## 💣 CrazyArcade (모작) 

<img width="600" alt="스크린샷 2025-05-15 오후 2 59 12" src="https://github.com/user-attachments/assets/0e95b4a6-794c-4dd1-b9c0-a97483ba4cfd" />

**개발 기간** : 2024.07 ~ 2024.09 (개인 프로젝트)  
**개발 도구** : Unity (2022.3.21f1), C#, GitHub Desktop, Visual Studio Code


### [프로젝트 개요]
Unity로 제작한 **크레이지 아케이드 : 문어 대습격 모드 모작**입니다.      
문어 대습격 모드는 총 3개의 스테이지로 구성되어 있으며 각 스테이지마다 등장하는 버럭이와 쫄쫄이 몬스터들을 물리치면 마지막 보스 스테이지로 진입할 수 있습니다. 


### [실행 및 자료]
- 플레이 영상 : [YouTube](https://www.youtube.com/watch?v=mxeJWvXusBQ)  
- 개발 문서 : [Google Drive](https://drive.google.com/file/d/1iaQQgqZrtdyEXk91NMVVFYbqH5sVrqhz/view?usp=sharing)  


### [주요 구현 내용]
- FSM(Finite State Machine)으로 상태별 동작과 전이 조건을 분리해 유지보수성과 확장성 확보
- 웨이포인트 기반 보스 패턴을 설계해 이동 경로와 공격 패턴을 데이터 기반으로 제어하고 FSM과 결합
- 2차원 배열 기반 타일맵을 구현해 설치 및 삭제 등 이벤트를 좌표 단위로 관리하고 충돌 및 확산 로직을 단순화
- 팩토리 패턴으로 플레이어 생성 과정을 일괄 관리해 초기화와 UI 연동을 자동화
- 전략 패턴을 FSM과 결합해 스테이지별 몬스터 이동 방식을 분리하고 상황별 로직을 유연하게 적용
