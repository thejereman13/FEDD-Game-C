using System;

namespace LaserAmazer.gui
{
    public interface IClickable
    {
        void setCallback(Action r);
        bool checkClick(float xPos, float yPos);
    }
}