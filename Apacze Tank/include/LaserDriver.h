#ifndef H_LASERDRIVER
#define H_LASERDRIVER
#include <Arduino.h>
#include <stdbool.h>
const int LaserPin = 13;
bool IsShooting = true;
void InitLaser()
{
    pinMode(LaserPin, OUTPUT);
}
void Shoot()
{
    digitalWrite(LaserPin, HIGH);
    delay(50);
    digitalWrite(LaserPin, LOW);
    delay(50);
}
#endif //H_LASERDRIVERs