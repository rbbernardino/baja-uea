int ledState = LOW;

void setup()
{
	Serial.begin(9600);
  pinMode(13, OUTPUT);
}

void loop()
{
  //Serial.write(1);
	if (Serial.available())
	{
		Serial.write(Serial.read());
	}
}

void toggle() {
  if (ledState == HIGH)
    ledState = LOW;
  else
    ledState = HIGH;

  digitalWrite(13, ledState);
}

