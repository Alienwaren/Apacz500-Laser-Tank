#ifndef H_COMMANDPARSER
#define H_COMMANDPARSER
#include <Arduino.h>
#include <SoftwareSerial.h>
#include <string.h>
const int MAXSIZE = 100;
const uint8_t RX_DEBUG  = 10;
const uint8_t TX_DEBUG  = 11;
const int UP  = 1;
const int DOWN  = 2;
const int LEFT = 3;
const int RIGHT  = 4;
const int SHOOT = 5;
SoftwareSerial debugSerial(RX_DEBUG, TX_DEBUG);
char buffer[MAXSIZE] = {0};


void initSerials()
{
    Serial.begin(9600);
    debugSerial.begin(115200);
}
void clearBuffer()
{
    for(int i = 0; i < MAXSIZE; i++)
    {
        buffer[i] = '\0';
    }
}
void displayBuffer()
{
    uint8_t actualStringSize = strlen(buffer);
    if(actualStringSize > 0)
    {
        debugSerial.write(buffer, actualStringSize);
        debugSerial.println();
    }
}
void receiveDataFromSerial(char *buffer)
{
    int actualChar = 0;
    while(Serial.available() > 0)
    {
        char receivedCharacter = Serial.read();
        if(actualChar < MAXSIZE)
        {
            buffer[actualChar] = receivedCharacter;
            actualChar++;
        }
    }
}
int interpretCommands()
{
    if(strcmp(buffer, "UP") == 0) 
    {
        return UP;
    }else if(strcmp(buffer, "DOWN") == 0)
    {
        return DOWN;
    }else if(strcmp(buffer, "LEFT") == 0)
    {
        return LEFT;
    }else if(strcmp(buffer, "RIGHT") == 0)
    {
        return RIGHT;
    }else if(strcmp(buffer, "SHOOT") == 0)
    {
        return SHOOT;
    }
    return -1;
}
#endif // H_COMMANDPARSER
