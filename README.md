# 감정저금통-뇌파를 이용한 기부유도시스템

### 특별학기 신경공학 수강(2019.01.18~2019.01.31)

----------

__주제__: 뇌파를 이용한 기부유도시스템

  - 평소뇌파와 주어진 자극 이후의 P300을 비교하여 기부자의 집중도를 추출하여 개개인에 따른 기부영상, 문구, 기부금액을 시각화 해주어 기부를 유도하는 시스템

__제안배경__: 2019년 기준, 사랑의 온도탑에 쌓인 모금액이 지난 해 같은 기간의 약 80% 수준이라고 보도되었고 이의 배경은  경제적 여유 부족과 당시 여러 사건들로 인한 후원금의 쓰임새에 대한 불신 등이 있었음.             
  _->기부자에게 맞는 기부시스텡을 제공함으로써 이를 해결하고자함_

----------
__프로젝트 내용:__                          
__*참고) emotiv epoc기기없이는 사용이 불가함*__                     

__1. 하드웨어__:                    
<img src="https://user-images.githubusercontent.com/47767202/79405595-da8f3e80-7fcf-11ea-8e63-79c6c278d155.jpeg" width="40%">
> emotiv epoc - 뇌파 측정 센서. 14채널 지원.

-------------

__2. 활용이론__:

<img src="https://user-images.githubusercontent.com/47767202/79638403-fb1deb00-81bf-11ea-9b0a-4e42eecbc1d3.png" width="40%">  

> P300: 특정한 정보를 내포하고 있는 자극을 받은 뒤 일정 시간 동안 일어나는 뇌의 전기적 활동(ERP) 중 하나로 약 300ms 후에 발생하는 뇌파

-----------

__3. 시스템 처리과정__:

- Flow Chart                            
  <img src="https://user-images.githubusercontent.com/47767202/79882335-5f91b200-842d-11ea-8776-dfbe6b663bd3.png" width="60%">
  - 이어폰과 emotiv를 착용한 후 10초간 뇌파의 reference측정
  - 4가지 카테고리(옷,건강,애완동물,가족)를 보여주며 사용자가 자신의 관심사를 생각하게함
  - 위 4가지 카테고리 사진이 1초씩 랜덤으로 4세트를 시청하게함
  - 총 16번 보여지는 동안 가장 높은 집중도가 높았던 카테고리를 보여주고 이에 관련한 기부영상을 1분간 시청하게함
  - 1분가량의 기부영상을 시청하는 동안 사용자의 집중정도(뇌파)를 측정함
  - 영상이 끝난뒤
   1. 영상의 각 구간에는 기부를 유도할 수 있는 맞춤 문구가 설정되어있어, 사용자가 가장 관심있게 시청한(집중도가 높았던) 구간의 문구를 출력함
  1분가량의 기부영상을 여러 구간으로 나누어 각 구간에 기부를 유도 할 수 있는 맞춤 문구를 설정하고, 사용자가 가장 관심있게 시청한(집중도가 높았던) 구간의 
  - 맞춤문구와 함께 사용자의 집중력 그래프를 보여주
