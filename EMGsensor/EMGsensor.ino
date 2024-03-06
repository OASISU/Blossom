void setup(){
  Serial.begin(115200);
  SERVO_1.attach(SERVO_PIN);
}

void loop(){
  int value = analogRead(EMG_PIN);
  if(value > THRESHOLD){
    SERVO_1.write(170);
  }
  else{
    SERVO_1.write(10);
  }
  Serial.println(value);
}