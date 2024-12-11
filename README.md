![image](https://github.com/user-attachments/assets/17363c26-574a-4b94-bb8c-0fd0baca2a81)

# 😄**어서오세요, 여긴 OSS 25입니다!**

**OSS 25**는 다마고치 형식의 게임으로, 귀여운 편의점 음식들을 15일 동안 키우며 진화시키는 게임입니다.

유명 편의점 **GS25**에서 프로젝트 이름을 착안해 제작하였습니다.
Android기반으로 설계된 **Unity** 게임입니다.

**미니게임**을 통해 추가 재화를 벌고, 플레이 스타일에 따라 다마고치의 진화 결과가 달라집니다.

최고의 희귀 다마고치 ✨**궁극의 고치**✨를 목표로 키워보세요!
<br>
<br>
- **개발 도구**: Unity, C#
- **버전 관리**: Git, GitHub

<br>

## **목차**

- [시작하기](#시작하기)
    - [실행 영상](#실행-영상)
- [게임 구조](#게임-구조)
    - [주요 Scene](#주요-Scene)
    - [게임 기능](#게임-기능)
    - [밸런스 패치](#밸런스-패치)
    
- [문제 발생 및 해결](#문제-발생-및-해결)
    - [애니메이션 이벤트 등록 불가](#애니메이션-이벤트-등록-불가)
    - [함수와 애니메이션의 속도 차이](#함수와-애니메이션의-속도-차이)
- [레퍼런스 및 참고자료](#레퍼런스-및-참고자료)
- [한계 및 개선점](#한계-및-개선-사항)
- [라이센스](#라이센스)

<br>

## 시작하기

### 설치 및 실행

1. 안드로이드 환경을 준비해주세요.
2. build된 APK파일을 Android 기기에 다운받습니다. <br> 
   *usb를 케이블 혹은 google drive 등의 서비스를 이용하여 다운받으세요* <br>
   [헷갈린다면](https://learnandcreate.tistory.com/858)
4. 다운 받은 APK 파일을 선택하여 설치합니다.
5. 설치가 완료되었다면 플레이를 즐겨보세요!

<br><br>


### 실행 영상

https://youtu.be/SsHIEsEp8Fk

<br><br><br>


## 게임 구조

Run, Card 두 개의 미니게임과 다마고치 UI를 다룹니다. 각 모듈은 독립적으로 작동하며, 점수 및 상태는 중앙 `DataManager`를 통해 관리됩니다.


### 주요 Scene

1. **Room**
    ![image](https://github.com/user-attachments/assets/76ed1e83-1038-4cc0-b441-cc3004c5c07c)
   <br>
   
    - 다마고치를 키우는 메인 화면입니다.
    - 플레이어는 코인을 사용해 다양한 행동(밥 주기, 운동하기 등)을 실행하며 스탯을 관리합니다.
  
      <br><br>
      
3. **Run**
       ![image](https://github.com/user-attachments/assets/687b77c0-0cc9-4e21-9c56-e94d3a13c548)
   <br>
   
    - 크롬의 공룡 달리기 게임을 참고한 미니게임입니다.
    - **2단 점프**와 **슬라이드**를 활용해 장애물을 피하며 생존 시간을 늘려야 합니다.
    - 생존 시간에 따라 코인이 지급됩니다.
  
      <br><br>

4. **Card**
    ![image](https://github.com/user-attachments/assets/3acf4ef0-04e4-4ddc-89b1-091f592d6c88)  |  ![image](https://github.com/user-attachments/assets/700da9dd-37a4-4d9c-b0bb-209d1dfc016c)
   --- | --- | 
    <br>
   
    - 제한 시간 35초 안에 동일한 카드 쌍을 찾아 맞추는 미니게임입니다.
    - 맞춘 카드 쌍의 수에 따라 코인을 획득할 수 있습니다.

<br><br>


<br>

## 게임 기능
게임의 주요 기능을 정리했습니다.
<br>

## 1. 게임 데이터 관리 
**DataManager**
DataManager는 게임 데이터를 관리하고 유지하는 핵심 클래스입니다. 
데이터 보존을 위해 싱글톤 패턴과 JSON 파일 형식을 사용합니다. 


### 싱글톤 패턴 
싱글톤 패턴을 사용하여 게임의 어떤 씬에서든지 하나의 인스턴스만 존재하도록 합니다. money와 게임 진행 상태 등의 데이터를 보존할 수 있고, 씬 전환 시에도 데이터 손실 발생을 막습니다.
```
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 데이터 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }
```

### JSON 파일
데이터는 JSON 파일 형식으로 저장되고 로드됩니다. 
```
    public void SaveData()
    {
        ...
        string json = JsonUtility.ToJson(this, true);  // 객체를 JSON으로 직렬화
        File.WriteAllText(path, json);  // JSON 파일로 저장
    }
```

- 객체를 JSON 형식으로 직렬화:

    `JsonUtility.ToJson` 메서드를 사용해 `DataManager` 클래스의 모든 필드를 JSON 문자열로 변환합니다.



- JSON 데이터를 파일로 저장:

    `File.WriteAllText` 메서드를 사용해 변환된 JSON 데이터를 지정된 경로(path)에 저장합니다.

<br>


```
    public void LoadData()
    {
        if (File.Exists(path))
        {
            ...
            string json = File.ReadAllText(path);  // 저장된 파일 읽기
            JsonUtility.FromJsonOverwrite(json, this);  // JSON 파일을 객체로 덮어쓰기
        }
    }
}
...
```

- JSON 데이터 읽기:

    `File.ReadAllText`로 파일 내용을 문자열로 읽습니다.
- JSON 데이터를 객체로 복원:

    `JsonUtility.FromJsonOverwrite`를 사용하여 JSON 데이터를 현재 DataManager 인스턴스의 필드에 복사합니다.





<br><br>

## 2. **UI와 데이터 연결**

### **DamagochiUI**:
플레이어가 다마고치의 스탯(배고픔, 애정, 청결도 등) 확인할 수 있는 UI를 제공합니다. `Slider`와 `Text` UI 요소를 활용하여 캐릭터의 상태를 표시하고 업데이트합니다.
또한 Button으로 스탯에 영향을 줍니다.

#### UI 데이터 업데이트
```
    ...
    public void UpdateStats(int hunger, int affection, int clean, int intelligence, int willpower, int social)
{
    hungerBar.value = hunger;
    affectionBar.value = affection;
    cleanBar.value = clean;
    intelligenceBar.value = intelligence;
    wilpowerBar.value = willpower;
    sociaBar.value = social;
}
```
슬라이더를 통해 현재 스탯 값을 시각적으로 표현합니다. 

<br><br>

## 3. **미니게임 구현**
각각의 미니게임은 플레이어의 점수에 따라 `money`의 값이 주어집니다.
`money`의 값이 `DataManager`를 통해 저장됩니다.
```
void SaveScoreToMoneyManager()
{
    DataManager.instance.money += currentScore;
}
```

### **Card**
주어진 35초 내에 9쌍의 카드를 맞추는 카드 매칭 게임입니다.
뒤집은 두 카드가 일치하면 점수를 획득합니다. 일치하는 쌍 마다 +3점씩 얻고, 모두 일치하였을 시 추가 점수 3점을 획득합니다.
점수는 그대로 `money`로 저장됩니다.

**GameManager**
`CheckMatchRoutine`은 두 쌍의 카드가 일치하는지 확인하는 코루틴입니다. 
```
...
private IEnumerator CheckMatchRoutine(Card card1, Card card2)
{
    isFlipping = true;  // 다른 카드 클릭 방지
    yield return new WaitForSeconds(0.6f);  // 딜레이 추가

    if (card1.cardID == card2.cardID)  // 카드 ID 매칭 확인
    {
        card1.SetMatched();
        card2.SetMatched();
        matchesFound++;
        currentScore += 3;  // 점수 증가
    }
    else
    {
        card1.FlipCard();  // 매칭 실패 시 카드 다시 뒤집기
        card2.FlipCard();
    }

    isFlipping = false;  // 카드 클릭 가능 상태로 복원
    flippedCard = null;

    if (matchesFound == totalMatches) GameOver(true);  // 모든 매칭 완료 시 게임 종료
}
...
```




### **Run**
미니게임 Run은 플레이어가 점프와 slide 버튼으로 장애물들을 피해 최대한 오래 생존해야하는 게임입니다. 
3번 이상 장애물과 충돌할 시 게임은 종료됩니다. 생존시간에 비례하는 점수를 계산하고 게임 종료 시 점수의 10분의 1을 `money`로 저장합니다.
장애물들은 새와 선인장이 있으며, 시간이 지남에 따라 속도가 빨라집니다.


**ScoreCalculationRoutine: 점수 및 속도 증가**
```
//RunManager
IEnumerator ScoreCalculationRoutine()
{
    while (isGameRunning)
    {
        obstacleSpeed += increaseRate * Time.deltaTime;  // 장애물 속도 증가
        currentScore += timeScoreRate * Time.deltaTime;  // 점수 증가
        UpdateScoreUI();  // 점수 UI 업데이트
        yield return null;  // 매 프레임 실행
    }
}

```
매 프레임마다 점수를 증가시키고 `increaseRate`의 비율에 따라 장애물의 속도를 높입니다.


**tag로 상태 확인**
```
//RunManager
public void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.tag == "Ground")
    {
        isGrounded=true;
        rb.gravityScale = gravityScale;
    }
}

public void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.tag == "Obstacle" && !isAttacked)
    {
        StartCoroutine(TakeDamage());
    }
}
```
태그를 활용하여 장애물과의 충돌을 감지하고 캐릭터의 상태를 업데이트합니다.



**점프 구현**
```
//RunManager
public void Jump()
{
    if (!isSliding)  // 슬라이드 중에는 점프 불가능
    {
        if (isGrounded)  // 캐릭터가 땅에 닿아 있는 경우
        {
            rb.velocity = Vector2.up * jumpForce;  // 위로 점프
            isGrounded = false;  // 땅에 닿은 상태 해제
            canDoubleJump = true;  // 더블 점프 가능 상태로 전환
        }
        else if (canDoubleJump)  // 공중에 있고 더블 점프가 가능한 경우
        {
            rb.velocity = Vector2.up * jumpForce;  // 위로 점프
            canDoubleJump = false;  // 더블 점프 사용 불가 상태로 전환
        }
    }
}
 
```
캐릭터가 슬라이드 중이면 점프를 허용하지 않습니다. 점프 횟수를 2번으로 제한합니다.
<br><br>

## 4. **씬 전환**

### **SceneManager**
게임의 씬 전환을 관리합니다. 버튼 클릭 시 다른 씬으로 이동할 수 있도록 구현되어 있습니다.

**LoadScene**
Scene을 불러올 수 있는 함수입니다.
```
public void LoadScene(string sceneName)
{
    UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
}
```
```
//GameManager 
public void OK()
{
    UnityEngine.SceneManagement.SceneManager.LoadScene("Room"); 
} 
```
card 미니게임에서 OK 버튼 클릭시 Room으로 씬 전환


```
//RunManager
public void OnOKButtonClicked()
{
    UnityEngine.SceneManagement.SceneManager.LoadScene("Room");
}
```
Run 미니게임에서 버튼 클릭 시 Room으로 씬 전환


<br><br>


## 5. **게임 엔딩 시스템**

### **EndingManager**
게임의 종료 상태에 따라 다양한 엔딩을 결정합니다. 스탯 값에 따라 플레이어의 엔딩을 분기 처리합니다. 높은 스탯을 가진 경우 **특별한 엔딩**을 보여줍니다.


```
 int DetermineEnding()
 {
     // 스탯 값 가져오기
     int affection = DataManager.instance.affection;
     int health = DataManager.instance.health;
     int cleanliness = DataManager.instance.cleanliness;
     int intelligence = DataManager.instance.intelligence;
     int sociality = DataManager.instance.sociality;
     int willpower = DataManager.instance.willpower;

     // 모든 스탯이 45 이상이면 특별 엔딩
     if (affection >= 45 && health >= 45 && cleanliness >= 45 &&
         intelligence >= 45 && sociality >= 45 && willpower >= 45)
     {
         return 0; // 특별 엔딩
     }

     // 애정도와 체력 확인
     if (affection < 40 || health < 40)
     {
         return 1; // 평범한 고치
     }

     // 직업 엔딩 결정 (40 이상인 스탯 기준)
     int careerEnding = DetermineCareer(cleanliness, intelligence, sociality, willpower);
     if (careerEnding != -1)
     {
         return careerEnding + 2; // 2번부터 직업 엔딩
     }

     return 1; // 기본 엔딩으로 대체
 }
```
최종 스탯을 비교하여 엔딩 분기를 선택합니다. 







<br><br>



## 밸런스 패치

### 코인

- 매일 **30 코인**이 기본 지급되며, 미니게임을 통해 추가 코인(최대 30)을 획득할 수 있습니다.
<br><br>

### 스탯

스탯은 총 6가지이며, 그중 4가지(3-6번)는 진화과정에 영향을 미칩니다.

1. 체력 
2. 애정도
3. 청결도
4. 지능
5. 의지력
6. 사회성
<br>
초기 스탯값 설정은 다음과 같습니다.

<br>

| 스탯 | max | day마다,  | 0일 시 패널티 | 시작(기본 제공) |
| --- | --- | --- | --- | --- |
| 허기짐/체력 | 50 | -5 | 게임 종료 | 20 |
| 애정도  | 50 | 0 | 게임 종료 | 0 |
| 청결도 | 50 | -5 | x | 20 |
| 지능 | 50 | 0 | x | 0 |
| 의지력 | 50 | 0 | x | 0 |
| 사회성 | 50 | 0 | x | 0 |

<br>

### 진화 조건
![image](https://github.com/user-attachments/assets/f438d00d-b30b-4361-ace2-3361ed8156ab) | ![image](https://github.com/user-attachments/assets/ca9b0fc7-6942-4446-b3f8-d65478a9c126) | ![image](https://github.com/user-attachments/assets/67c320ce-38a4-4ac2-aae5-5cf0130dd5e2)
--- | --- | --- |

<br> 

| 진화 | 애정도&체력 | 청결도 | 지능 | 의지력 | 사회성 |
| --- | --- | --- | --- | --- | --- |
| 평범 고치 | 40미만  | - | - | - | - |
| 청소왕 고치 | 40이상 | 40이상 | - | - | - |
| 용사 고치  | 40이상 | - | - | 40이상 | - |
| 인싸 고치 | 40이상 | - | - | - | 40이상 |
| 박사 고치 | 40이상 | - | 40이상 | - | - |
| 궁극의 고치 | 45이상 | 45이상 | 45이상 | 45이상 | 45이상 |

<br>




### 행동 버튼

다마고치의 성장을 위해선 행동 버튼을 적절히 사용해야 합니다.
행동 버튼 사용 시 골드가 소모되고, 특정 스탯을 올려줍니다.

| 행동 | 증가 | 소모 골드 | 횟수제한 |
| --- | --- | --- | --- |
| 밥주기 | 체력+10 | 10 | 1 |
| 쓰다듬기 | 애정도+1 | 0 | 1 |
| 청소하기 | 청결도+10, 애정도+1 | 20 | 1 |
| 책읽기 | 지능+5, 애정도+1 | 20 | 1 |
| 운동하기 | 의지력+5, 애정도+1 | 20 | 1 |
| 놀이터 가기 | 사회성+5, 애정도+1, 골드 0-30 | 0 | 1 |
| 돈벌기:
카드, 공룡런 | 골드 0-30 | 0 | 1 |

<br><br><br>

## 문제 발생 및 해결

### 애니메이션 이벤트 등록 불가

- 애니메이터는 ‘Bt animator’ 오브젝트에 위치했으나, 각각의 애니메이션을 트리거하는 스크립트가 하위 오브젝트인 ‘Pet’에 위치했다.
- 초반에는 문제를 느끼지 못했으나, 후에 애니메이션 이벤트를 쓰게되자 문제가 생김
- 애니메이터가 속한 오브젝트의 메서드만 이벤트로 등록할 수 있었던 것!
- 애니메이션 제어 함수를 ‘Bt animator’ 오브젝트로 옮겼으나, 모든 버튼에 일일이 다시 트리를 할당해야 하는 번거로움이 있었다.
- 이런 문제를 방지하기 위하여 고려 중인 방안들
    - 애니메이션 트리거 구조 개선
        - 애니메이션 컨트롤러와 트리거 스크립트를 동일한 오브젝트에 배치하여 애니메이션 이벤트와 스크립트 간의 연결 문제를 근본적으로 해결한다.
        - 애니메이션 이벤트를 호출하는 중간 프록시 스크립트를 작성하여 트리거 위치와 상관없이 이벤트를 통합 관리한다.
    - 디자인 패턴 적용
        - 오브젝트의 종속성을 벗어나는 효과적인 방안으로 알고 있으나, 아직 제대로 숙지하지 못한 분야이므로 더욱 공부가 필요하다. ⇒ chat gpt에 물어본 결과 Observer 패턴, Event-Driven 구조를 활용할 수 있을 것으로 보인다
    - 초기 설계 단계에서 구조를 명확히 할 것
        - 어떤 오브젝트에서 어떤 스크립트를 다룰지, 그에 맞게 어떤 컴포넌트를 달아줘야 할지를 초기 설계 단계에서 확실히 한다
    - 스크립트 자동화
        - Unity 에디터 스크립트를 작성하여 버튼에 필요한 트리거나 이벤트를 자동으로 연결하는 작업을 자동화한다

<br><br>

### 함수와 애니메이션의 속도 차이

- 함수는 매우 빠른 시간 안에 일을 처리하는데 반해, 유저의 눈에 보일 애니메이션은 위치나 상태를 서서히 변경시킨다
- 때문에 아래와 같은 경우가 생긴다



https://github.com/user-attachments/assets/07df9203-138d-41d4-b33f-b694b1c19b08



- 하루가 지나는 순간, 다마고치의 스프라이트는 순식간에 변경되었으나 날이 저무는 애니메이션은 여전히 재생 중이다
<br>
<details><summary>1차 수정</summary>


https://github.com/user-attachments/assets/38712d09-dbfa-4477-9a02-b375deb0a13a



  - 함수를 코루틴으로 변경하여, 만약 다마고치가 알이 아니라면 스프라이트가 적용되는 데 약간의 시간을 두었다.
  - 그러나 이는 애니메이션의 시간에 맞추어 직접 몇 초간의 딜레이가 좋을지 짐작하여 입력해야하는 불편이 있다.
  - 또한 다른 씬에서 Room 씬으로 돌아왔을 때에도 스프라이트가 변하는 데 딜레이가 생긴다는 문제를 발견했다.

</details>


<br><br>
    
<details><summary> 2차 수정 - 현재 </summary>
    - 고치의 상태가 ‘알’일 때를 제외하고 딜레이를 주는 방식에서, 아래와 같이 변경 (코루틴은 유지했다)
    
![스크린샷 2024-12-11 183539](https://github.com/user-attachments/assets/0173e952-fc29-46a2-ac77-54350c9d483b)

    
    ```csharp
    public void AdvanceDay() **//애니메이션과 함께 실행되는 함수**
    {
        **isDayChanging = true; //임의의 부울변수 추가**
        DataManager.instance.currentDay++;
        DataManager.instance.money += 20;
        DataManager.instance.health -= 5;
        DataManager.instance.cleanliness -= 5;
        DataManager.instance.ResetButtonStates();
    
        Debug.Log($"현재 날짜: {DataManager.instance.currentDay}");
        CheckLevel(); **//체크하여 조건에 맞으면 스프라이트 업데이트**
    
        int day = DataManager.instance.currentDay;
        previous.text = (day - 1).ToString();
        next.text = day.ToString();
        DataManager.instance.SaveData();
    
        **isDayChanging=false; //스프라이트 업데이트 후엔 true를 유지할 필요가 없음**
    
        if (DataManager.instance.currentDay > maxDay)
        {
            Debug.Log("엔딩");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ending");
        }
    }
    ```
    
    
 - 날짜를 바꾸는 함수는 애니메이션과 함께 호출되고 있으므로, 다른 오브젝트의 값을 참조하지 않고 변수를 추가하는 것만으로 해결 가능했다.
 - 스프라이트를 변경할 때 날짜가 바뀌고 있다면 딜레이를 주는 방식
 - 그러나 여전히 딜레이 시간을 직접 입력하는 방식에서 파생되는 문제는 해결되지 않는다.

     
- 이를 해결하기 위해서는
    - 애니메이션 이벤트 활용
        - 애니메이션이 끝나갈 즘에 함수를 호출하여 괴리를 줄인다
    - 코루틴의 유예 시간 자동화
        - 애니메이션의 길이를 기준으로 유예 시간을 자동 계산하여 적용한다
    - 상태 기반 설계 (State Machine)
        - 다마고치의 상태를 `Idle`, `Transitioning`, `DayEnd` 등의 상태로 정의하고, 상태 변화에 따라 애니메이션과 함수 호출이 순차적으로 진행되도록 만든다
        - 상태 전환 시 애니메이션이 끝날 때까지 기다렸다가 다음 상태로 전환하는 구조를 설계한다.
        - 가장 좋은 방식이라고 생각한다! 각 상태가 독립적으로 분리되어 관리되므로 코드가 구조적이고 유연해진다. 동작 간의 일관성도 유지할 수 있다
</details>
<br><br><br>

## 레퍼런스 및 참고자료

- **공룡런**: [참고 영상 링크](https://www.youtube.com/watch?v=iENDSs0qXSs&list=PLO-mt5Iu5TebzgxMKYDw40mDxytgFBex0&ab_channel=%EA%B3%A8%EB%93%9C%EB%A9%94%ED%83%88)
- **카드 뒤집기**: [참고 영상 링크](https://www.youtube.com/watch?v=XhfB3ZS3JoM&ab_channel=%EB%82%98%EB%8F%84%EC%BD%94%EB%94%A9)
- GPT 사용

<br><br><br>

## 한계 및 개선 사항

### 미니게임의 피로도

궁극의 고치를 얻기 위해선 13일 이상 매일 미니게임을 해야 하며, 이로 인해 피로감이 발생합니다.

- **해결 방안**:
    1. **미니게임 횟수 제한**: 미니게임 플레이를 **3일에 1번**으로 제한하여 피로도를 낮춤.
    2. **진행 기간 단축**: 전체 게임 기간을 **7일로 축소**하여 플레이 시간을 줄임.
    3. **미니게임 다양화**: 새로운 미니게임을 추가해 반복 플레이의 지루함을 줄임.
    4. **기록 갱신**: 현재 미니게임에는 점수를 기록하는 기능이 없습니다. 최고점수 기록을 통해 승부욕과 경쟁심을 자극.
<br><br>
### 진화 연출 강화

각 단계별 진화 시 이미지만 변경되기 때문에 보다 화려한 애니메이션과 사운드를 추가하여 성취감을 높일 필요가 있습니다.
<br><br>
### 다마고치 기능 추가

다회차 플레이 유도와 최종 진화의 보상감을 주기 위해 다음과 같은 기능을 추가할 계획입니다. 

- 나의 알 보관함: 유저가 최종 진화한 다마고치를 기록할 수 있는 아카이브를 만들계획입니다.
- 사진 찍기 기능: 다마고치와의 추억을 기록할 수 있습니다.
<br><br>
### 스탯에 따른 세부적 이미지 효과

현재는 스탯의 값에 상관없이 다마고치는 일정한 이미지를 유지합니다.

게임의 몰입도와 완성도를 위해 적절한 이미지를 추가할 계획입니다.

- 일정 스탯이 하락하거나 충족되었을 때 다마고치의 이미지 변경
    - 예) 청결도가 15이하 → 더러운 효과 추가
    

<br><br><br>

## 라이센스

이 프로젝트는 MIT License 를 따릅니다.

