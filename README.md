# 💣 CrazyArcade (모작)

<img width="600" alt="CrazyArcade 플레이 화면" src="https://github.com/user-attachments/assets/0e95b4a6-794c-4dd1-b9c0-a97483ba4cfd" />

Unity로 제작한 **크레이지 아케이드 '문어 대습격' 모드의 2D 탑다운 액션 모작 게임**입니다.

총 3개의 스테이지에서 몬스터와 최종 보스를 처치하는 게임으로 게임 진행 구조와 캐릭터 및 몬스터의 행동 로직을 직접 설계하고 구현했습니다.

🎬 [플레이 영상](https://www.youtube.com/watch?v=mxeJWvXusBQ) · 📄 [개발 문서](https://drive.google.com/file/d/1iaQQgqZrtdyEXk91NMVVFYbqH5sVrqhz/view?usp=sharing)

> 본 프로젝트는 학습 목적으로 제작한 모작 프로젝트입니다.

---

## 🗂️ 프로젝트 정보

| 항목 | 내용 |
|---|---|
| 개발 기간 | 2024.07 ~ 2024.09 |
| 개발 인원 | 1인 개발 |
| 개발 환경 | Unity 2022.3.21f1, C#, Visual Studio Code |
| 주요 기술 | FSM, 2차원 배열 기반 타일맵, 웨이포인트 |
| 버전 관리 | Git, GitHub Desktop |

## 💻 핵심 구현

### 1. 2차원 배열 기반 물풍선 폭발 및 연쇄 반응

장애물 종류와 위치에 따라 폭발 범위를 일관되게 처리하기 위해 15×13 격자의 상태를 2차원 배열로 관리했습니다.

- 물풍선 위치에서 상하좌우 4방향으로 폭발 범위 탐색
- 박스, 벽 등 좌표별 오브젝트 종류에 따라 폭발 확산 및 파괴 여부 결정
- 다른 물풍선을 만나면 `READY` 상태로 전환해 연쇄 폭발 대상 수집
- 대상 탐색과 실제 폭발 실행을 분리해 연쇄 반응 처리

🔗 [StageMap](Assets/Scripts/Map/StageMap.cs) · [MapSettingCenter](Assets/Scripts/Center/MapSettingCenter.cs) · [BalloonController](Assets/Scripts/Balloon/BalloonController.cs)

---

### 2. FSM 기반 상태 전이 관리

플레이어의 이동, 갇힘, 사망, 클리어 상태에 따른 행동과 전이 조건을 분리하기 위해 FSM을 구현했습니다.

- 각 State에서 `OnEnter()`, `Update()`, `FixedUpdate()`, `OnExit()`를 통해 상태별 동작 관리
- `StateMachine`에서 현재 상태와 전이 가능 여부 관리
- 입력 전이와 게임 로직 전이를 별도의 `HashSet`으로 관리
- 상태별 허용 전이를 검사해 이동 입력이 갇힘이나 사망 상태를 덮어쓰지 않도록 구성

🔗 [IPlayerState](Assets/Scripts/Interface/IPlayerState.cs) · [PlayerStateMachine](Assets/Scripts/Player/PlayerStateMachine.cs) · [PlayerTrapState](Assets/Scripts/Player/PlayerState/PlayerTrapState.cs)

---

### 3. 웨이포인트 기반 보스 패턴

보스의 이동 경로와 공격 조건을 코드 수정 없이 조정할 수 있도록 웨이포인트 기반 패턴 시스템을 구현했습니다.

- 웨이포인트를 통해 보스 이동 경로 구성
- `BossAttackInstruction`에서 공격 종류, 횟수, 방향 설정
- 웨이포인트 도착 시 `ATTACK` 상태로 전환해 설정된 공격 실행
- 공격 후 `WAIT` 상태에서 남은 횟수에 따라 재공격 또는 이동 결정

🔗 [BossAttackInstruction](Assets/Scripts/Boss/BossAttackInstruction.cs) · [BossController](Assets/Scripts/Boss/BossController.cs) · [BossAttackState](Assets/Scripts/Boss/BossState/BossAttackState.cs)

---

## 🛠️ 트러블슈팅

### 1. 물풍선 연쇄 폭발 타이밍 문제

**문제**

여러 물풍선이 연쇄 폭발할 때 일부 물풍선이 중복으로 폭발하거나 폭발 시점이 어긋나는 문제가 발생했습니다.

**원인**

폭발 대상을 탐색하는 도중 일부 물풍선이 `POP` 상태로 전환되면서 대상 탐색과 실제 폭발 실행이 같은 흐름에서 섞이고 있었습니다.

**해결**

- 탐색 중 발견한 물풍선을 `READY` 상태로 전환해 연쇄 폭발 대상 수집
- 이미 `READY` 상태인 물풍선은 중복 처리하지 않도록 검사
- 대상 탐색 완료 후 `PopBalloons()`에서 수집된 물풍선을 `POP` 상태로 전환

**결과**

연쇄 폭발 대상 탐색과 실행 시점을 분리하고 중복 상태 전환을 방지해 폭발 타이밍이 탐색 순서에 영향을 받지 않도록 개선했습니다.

🔗 [MapSettingCenter](Assets/Scripts/Center/MapSettingCenter.cs) · [BalloonReadyState](Assets/Scripts/Balloon/BalloonState/BalloonReadyState.cs) · [BalloonPopState](Assets/Scripts/Balloon/BalloonState/BalloonPopState.cs)
