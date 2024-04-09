#include <Servo.h>

Servo myServo;  // 서보 모터 제어를 위한 서보 객체 생성
int pulsePin = A0;  // 심박 센서의 데이터를 읽을 아날로그 핀 설정
int servoPin = 9;   // 서보 모터 연결 핀 설정
int pulseValue; 
int angle = 0;    // 심박 센서로부터 읽은 값을 저장할 변수

void setup() {
  myServo.attach(servoPin);  // 서보 모터 핀을 서보 객체에 연결
  Serial.begin(9600);        // 시리얼 통신 시작
}

void loop() {
  pulseValue = analogRead(pulsePin);  // 심박 센서에서 심박 데이터 읽기
  Serial.println(pulseValue);         // 시리얼 모니터에 심박 데이터 출력
  
  if (pulseValue < 10) {
    for(angle=0; angle<180; angle++){
      myServo.write(angle);
      delay(10);
    }
    for(angle=180; angle>0; angle--){
      myServo.write(angle);
      delay(10);
    } for(angle=0; angle<180; angle++){
      myServo.write(angle);
      delay(10);
    }
    for(angle=180; angle>0; angle--){
      myServo.write(angle);
      delay(10);
    }
  } else {
    // 심박 데이터가 10 이상일 경우 필요한 로직을 추가
  }

  // 다음 심박 데이터 읽기 전에 잠시 대기, 너무 빠른 반복을 피하기 위함
  delay(1000);
}
