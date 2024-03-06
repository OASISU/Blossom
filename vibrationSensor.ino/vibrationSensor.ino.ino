/*
 * 예제22-1
 * 디지털 충격센서를 이용해서 충격이 감지되면 시리얼 통신으로
 * 알려주는 프로그램을 만들어보라!
 * 충격센서는 디지털 2번핀에 연결해라!
 */
int sensor = 0;
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(2,INPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  sensor = digitalRead(2);
  //Serial.println(sensor);
  if(sensor == HIGH){
    //센서가 작동한것
    Serial.println("0.0f");
  }
  delay(100);
}