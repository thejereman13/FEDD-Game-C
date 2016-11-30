using System.Collections;
using System.Collections.Generic;

public class LaserWrapper {
	
	private LinkedList<LaserModel> laserList = new LinkedList<LaserModel>(); // Linked List of all lasers
	private object[] newL;
	private readonly ReadWriteLock lock = new ReentrantReadWriteLock(); // ReadWriteLock for synchronizing the access of the list between threads
	private LaserModel root; // Root node in the laser list
	
	/**
	 * New LaserWrapper with the starting laser init
	 * @param init
	 */
	public LaserWrapper(LaserModel init) {
		laserList.AddLast(init);
		root = init;
	}
	
	/**
	 * Runs recursively the reflection calculations on the last laser in the list
	 */
	private void calculateReflections() {
		newL = ReflectionCalculation.reflect(laserList.Last.Value); // Reflect the last laser in the list
		if (newL != null && (!(newL[1] is Wall) && !(newL[1] is LaserStop) && !(newL[1] is LaserStart)) && laserList.Count < 20){ 	//if the returned reflection is neither null nor off a wall
			laserList.AddLast((LaserModel)newL[0]); // Add the new laser
			calculateReflections(); // Reflect again with that new one
		}
	}
	/**
	 * Run everything necessary to generate a path for the lasers
	 */
	public void runReflections() {
		lock.writeLock().lock(); // Lock access to the list
		
		laserList.Clear(); // Clear and restart the list
		laserList.AddLast(root);
		calculateReflections(); // Run reflections
		
		lock.writeLock().unlock(); // Unlock access
	}
	
	/**
	 * Renders all the lasers in the system
	 */
	public void render() {
		lock.readLock().lock(); // Locks access to the list
		
		// Renders all lasers in the list
		foreach (LaserModel m in laserList){
			m.render();
		}
		lock.readLock().unlock(); // Unlocks the list
	}
	
	public void rotateStart(float angle, float xOffset, float yOffset) {
		try {
			float[] c = laserList.getFirst().getCoords();
			float x = c[0] - xOffset, y = c[1] - yOffset;
			float[] g = new float[] {
					 (float)(x * Math.cos(angle) - y * Math.sin(angle) + xOffset),
					 (float)(x * Math.sin(angle) + y * Math.cos(angle) + yOffset)
			};
			
			laserList.getFirst().setCoords(g);
			float ang = (float)(laserList.getFirst().getAngle() + angle) % ((float)Math.PI * 2f);
			laserList.getFirst().setAngle(ang);
		} catch (Exception e) {}
	}

}
