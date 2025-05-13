### 🔧 기능 구현 내역

#### 1. 🪂 강화 점프 기능 (Reinforced Jump)
- [스페이스바를 누르고 있는 시간에 비례하여 점프 높이가 강화되는 시스템 구현]
- 최소/최대 점프력을 `Mathf.Lerp()`를 통해 선형 보간
- `Input.GetKeyDown`, `GetKey`, `GetKeyUp`을 조합하여 점프 충전과 발사를 분리
- 충전 중 시각 피드백으로 스프라이트 컬러가 점차 노란색으로 깜빡이는 효과 추가
- 바닥 착지를 `Collision Detection = Continuous` + `OnCollisionEnter2D`로 정확하게 감지

#### 2. ☁️ 낙하 구름 기믹 (Falling Cloud Mechanic)
- [플레이어가 특정 구름에 착지하면 일정 시간 후 낙하]
- 낙하 후 일정 시간 동안 비활성화 → 이후 원래 위치로 복귀
- `Rigidbody2D`의 `bodyType`을 `Kinematic ↔ Dynamic`으로 전환하여 자연스러운 낙하 구현
- `SpriteRenderer.enabled`, `Collider2D.enabled` 조절로 시각/물리적 비활성화
- `Coroutine`을 사용해 낙하 → 사라짐 → 재등장 순서를 타이밍 제어
- 모든 구름은 `Cloud` 태그로 통일하고, 낙하 구름은 `FallingCloud.cs` 컴포넌트로 구분

#### 3. 🎵 사운드 매니저 기반 배경음악 및 효과음 재생 기능 (SoundManager Audio Playback)
- [BGM과 SFX를 재생하는 기능 구현]
- SoundManager를 통해 BGM과 SFX를 통합 관리하는 시스템 구축
- AudioSource와 AudioClip을 묶은 AudioUnit 클래스를 사용하여 사운드 관리 구조 개선
- Enum(SoundName) 기반으로 사운드 선택 및 재생, 문자열 오타 가능성 제거
- PlayBGM(), PlaySFX() 메서드를 통해 간편한 사운드 호출 및 재생 처리
- foreach 문에서 명확한 타입 지정(AudioUnit unit)을 통해 코드 가독성 향상
- 사운드 재생 시 일치하는 사운드를 찾으면 즉시 return하여 불필요한 반복 방지
- Inspector를 통해 AudioUnit 추가만으로 손쉽게 BGM/SFX 확장 가능
- [고급화] / [확장가능성]
  - 게임 실행 중 BGM 볼륨 조정 가능
  - 게임 실행 중 SFX 볼륨 조정 가능
  - 슬라이더(Slider) UI를 사용해서 실시간 조정 가능
  - 게임 껐다 켜도 마지막 볼륨 기억 (PlayerPrefs 저장) 가능
  - 옵션 버튼을 눌렀을 때 OptionPanel 열고 닫기 기능 추가
 
#### 4.🎬 타이틀 화면 및 메인 메뉴 전환 기능 (Title Scene and Menu System)
- [타이틀 화면과 메인 메뉴를 통한 게임 시작 흐름 구축]
- TitleScene을 추가하여 게임 시작 전 타이틀 화면 제공
- TitleScene에서 버튼 클릭 시 GameScene으로 자연스러운 전환 구현
- TitleUIController 스크립트를 통해 버튼 이벤트 관리
- SoundManager를 사용하여 타이틀 화면 BGM 재생 및 클릭 효과음 처리
- TitleScene 초기화 시 BGM 자동 재생, 버튼 클릭 시 BGM 중지 처리

#### 5.🛠️ 전역 매니지 시스템 구조 정립 (Global Manager Initialization System)
- [ManagersScene을 통한 전역 오브젝트 초기화 체계 구축]
- ManagersScene(초기화 전용 빈 씬)을 추가하여 매니저 오브젝트 관리
- SoundManager, GameManager를 ManagersScene에 등록
- DontDestroyOnLoad()를 통해 씬 전환 시에도 매니저 오브젝트 유지
- ManagersScene 로딩 후 자동으로 TitleScene 전환 처리
- GameManager를 통한 일관된 씬 이동 관리(f_RestartGame, f_ClearGame 등)
- SoundManager를 통한 통합적인 BGM, SFX 관리 체계 강화
- 씬 전환 시 매니저 중복 생성을 방지하는 싱글톤 패턴 완성
