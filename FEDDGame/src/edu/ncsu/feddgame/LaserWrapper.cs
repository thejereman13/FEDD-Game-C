using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class LaserWrapper {
	
	private LinkedList<LaserModel> laserList = new LinkedList<LaserModel>(); // Linked List of all lasers
	private object[] newL;
	private static readonly ReaderWriterLockSlim l = new ReaderWriterLockSlim(); // ReadWriteLock for synchronizing the access of the list between threads
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
		l.EnterWriteLock(); // Lock access to the list
		
		laserList.Clear(); // Clear and restart the list
		laserList.AddLast(root);
		calculateReflections(); // Run reflections
		
		l.ExitWriteLock(); // Unlock access
	}
	
	/**
	 * Renders all the lasers in the system
	 */
	public void render() {
		l.EnterReadLock(); // Locks access to the list
		
		// Renders all lasers in the list
		foreach (LaserModel m in laserList){
			m.render();
		}
		l.ExitReadLock(); // Unlocks the list
	}
	
	public void rotateStart(float angle, float xOffset, float yOffset) {
		try {
			float[] c = laserList.First.Value.getCoords();
			float x = c[0] - xOffset, y = c[1] - yOffset;
			float[] g = new float[] {
					 (float)(x * Math.Cos(angle) - y * Math.Sin(angle) + xOffset),
					 (float)(x * Math.Sin(angle) + y * Math.Cos(angle) + yOffset)
			};

			laserList.First.Value.setCoords(g);
			float ang = (float)(laserList.First.Value.getAngle() + angle) % ((float)Math.PI * 2f);
			laserList.First.Value.setAngle(ang);
		} catch (Exception e) {}
	}

}
