using LaserAmazer.gui;
using System;
using System.Collections.Generic;

namespace LaserAmazer.level
{
    public abstract class Scene
    {

        protected string name;
        protected bool active = false;
        protected List<UIElement> elementList = new List<UIElement>();
        protected double timeStart;
        private double timeStop;

        public Scene(string name)
        {
            this.name = name;
        }

        public abstract void RenderObjects();

        public virtual void LogicLoop() { }

        public virtual void RenderLoop()
        {
            if (elementList != null)
            {
                foreach (UIElement e in elementList)
                {
                    e.render();
                }
            }
        }

        public string getName()
        {
            return name;
        }

        public bool IsActive()
        {
            return active;
        }

        public void SetActive(bool active)
        {
            if (this.active == active) return;

            this.active = active;

            if (active)
            {
                timeStart = GetTime();
            }
            else
            {
                timeStop = GetTime();
            }
        }

        public virtual void CheckClick(float mouseX, float mouseY)
        {
            foreach (UIElement e in elementList)
            {
                if (e is IClickable)
                {
                    ((IClickable)e).checkClick(mouseX, mouseY);
                }
            }
        }

        public double GetElapsedTime()
        {
            if (active)
            {
                return GetTime() - timeStart;
            }
            else
            {
                return timeStop - timeStart;
            }
        }

        public string GetElapsedSeconds()
        {
            return GetElapsedTime().ToString("#0.0");
        }

        /**
         * @return System time in seconds
         */
        public double GetTime()
        {
            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            return t.TotalSeconds;
        }

        public List<UIElement> getElementList()
        {
            return elementList;
        }

    }
}