int sensor = 0;
bool isShockDetected = false; // 충격 감지 여부를 저장하는 변수

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(2, INPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  sensor = digitalRead(2);
  
  // 센서가 HIGH 값을 감지하고 충격을 감지한 적이 없을 때
  if (sensor == HIGH && !isShockDetected) {
    // 충격을 감지했음을 표시
    isShockDetected = true;
    // 충격을 감지한 경우에만 메시지를 출력
    Serial.println("0.0f");
    // 3초 동안 대기
    delay(100);
  }
  // 센서가 LOW 값을 감지하고 충격을 감지한 적이 있을 때
  else if (sensor == LOW && isShockDetected) {
    // 충격을 감지한 적이 없음을 표시
    isShockDetected = false;
  }
}