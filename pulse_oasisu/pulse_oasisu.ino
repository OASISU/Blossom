// 아두이노 심박 측정 센서 예제 코드
int pulsePin = 0;          // 심박수 센서의 출력 핀을 아두이노의 A0 핀에 연결
int pulseValue = 0;        // 센서로부터 읽은 심박수 값 저장 변수

void setup() {
  Serial.begin(9600);      // 시리얼 통신 시작(통신 속도 9600bps)
}

void loop() {
  pulseValue = analogRead(pulsePin);  // A0 핀에서 아날로그 값 읽기
  if(pulseValue == 1){
    Serial.println(pulseValue);
    delay(5000);             // 5초 동안 대기
    pulseValue = analogRead(pulsePin);
    int resultValue = analogRead(pulsePin);
    Serial.println(resultValue);
  }
           // 읽은 값 시리얼 모니터에 출력
  
  delay(1000);             // 1초 동안 대기
}
