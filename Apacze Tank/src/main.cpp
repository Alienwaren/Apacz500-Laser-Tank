#include <Arduino.h>
#include <SoftwareSerial.h>
#include "CommandParser.h"
#include "EngineDriver.h"
#include "LaserDriver.h"

int ledState = LOW;
const long stopInterval = 250;
long actualTime = 0;
long previousTime = 0;
void setup() 
{
    initSerials();
    InitEngines();
    InitLaser();
    ResetEngines();
}

void loop() 
{
    actualTime = millis();
    clearBuffer();
    receiveDataFromSerial(buffer);
    displayBuffer();
    if(actualTime - previousTime >= stopInterval)
    {
        previousTime = actualTime;
        ResetEngines();
    }
    int commandCode = interpretCommands();
    if(commandCode == UP)
    {
        actualTime = 0;
        SetEnginesToForward();
    }else if(commandCode == DOWN)
    {
        actualTime = 0;
        SetEnginesToBackwards();
    }else if(commandCode == LEFT)
    {
        actualTime = 0;
        SetEnginesToLeft();
    }else if(commandCode == RIGHT)
    {
        actualTime = 0;
        SetEnginesToRight();
    }else if(commandCode == SHOOT)
    {
        actualTime = 0;
        Shoot();
    }
    delay(10);
}