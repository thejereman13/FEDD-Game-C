
using System.Collections.Generic;

public abstract class Scene {
	
	protected string name;
	protected bool active = false;
	protected List<UIElement> elementList = new List<UIElement>();
	protected double timeStart;
	private double timeStop;
	//private static DecimalFormat timeFormat = new DecimalFormat("#.#");
	
	public Scene(string name) {
		this.name = name;
	}
	
	public abstract void renderObjects();
	
	public virtual void logicLoop() {}
	
	public virtual void renderLoop() {
		if (elementList != null){
			foreach (UIElement e in elementList) {
				e.render();
			}
		}
	}
	
	public string getName() {
		return name;
	}
	
	public bool isActive() {
		return active;
	}
	
	public void setActive(bool active) {
		if (this.active == active) return;
		
		this.active = active;
		
		if (active) {
			timeStart = getTime();
		} else {
			timeStop = getTime();
		}
	}
	
	public virtual void checkClick(float mouseX, float mouseY) {
		foreach (UIElement e in elementList) {
			if (e is IClickable) {
				((IClickable)e).checkClick(mouseX, mouseY);
			}
		}
	}
	
	public double getElapsedTime() {
		if (active) {
			return getTime() - timeStart;
		} else {
			return timeStop - timeStart;
		}
	}
	
	public string getElapsedSeconds() {
        return getElapsedTime().ToString("#0.0");
	}
	
	/**
	 * @return System time in seconds
	 */
	public double getTime() {
        //return (double) System.nanoTime() / (double) 1000000000L;
        return 0d;
	}
	
	public List<UIElement> getElementList() {
		return elementList;
	}
	
}
