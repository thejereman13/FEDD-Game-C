using System;
public interface IClickable {
    void setCallback(Action r);
	bool checkClick(float xPos, float yPos);

}
