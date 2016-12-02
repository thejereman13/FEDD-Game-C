using System;

namespace LaserAmazer.Gui
{
    public interface IClickable
    {
        void SetCallback(Action r);
        bool CheckClick(float xPos, float yPos);
    }
}