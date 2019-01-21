#ifndef H_ENGINEDRIVER
#define H_ENGINEDRIVER
#include <avr/pgmspace.h>
#include <Arduino.h>
const int engines[] = {9, 8, 7, 6};

void InitEngines()
{
    for(int i = 0; i < 4; i++)
    {
        pinMode(engines[i], OUTPUT);
    }
}
void SetEnginesToForward()
{
    digitalWrite(engines[0], HIGH);
    digitalWrite(engines[1], LOW);
    digitalWrite(engines[2], HIGH);
    digitalWrite(engines[3], LOW);
}
void SetEnginesToBackwards()
{
    digitalWrite(engines[0], LOW);
    digitalWrite(engines[1], HIGH);
    digitalWrite(engines[2], LOW);
    digitalWrite(engines[3], HIGH);
}
void SetEnginesToRight()
{
    digitalWrite(engines[0], HIGH);
    digitalWrite(engines[1], LOW);
    digitalWrite(engines[2], LOW);
    digitalWrite(engines[3], HIGH);
}
void SetEnginesToLeft()
{
    digitalWrite(engines[0], LOW);
    digitalWrite(engines[1], HIGH);
    digitalWrite(engines[2], HIGH);
    digitalWrite(engines[3], LOW);
}
void ResetEngines()
{
    digitalWrite(engines[0], LOW);
    digitalWrite(engines[1], LOW);
    digitalWrite(engines[2], LOW);
    digitalWrite(engines[3], LOW);
}
#endif //H_ENGINEDRIVER