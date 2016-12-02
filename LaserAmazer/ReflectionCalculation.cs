using LaserAmazer.Gui;
using LaserAmazer.Math;
using LaserAmazer.Render;
using OpenTK;
using System;
using System.Collections.Generic;

namespace LaserAmazer
{
    public class ReflectionCalculation
    {

        private static List<object[]> intersects = new List<object[]>(); // list of arrays : [Model, xIntercept, yIntercept, slope of intersected line segment]
        private static float[] coords;
        private static object[] closest;

        /**
         * Calculates the path of travel of the laser and sets the laser to such a
         * path
         * 
         * @param laser
         * @param models
         */
        public static object[] Reflect(LaserModel laser)
        {
            FindIntersects(laser, GameInstance.objectManager.GetModels());

            // If there exists at least one valid intersection

            if (intersects.Count != 0)
            {
                closest = GetClosestIntersection(); // Find the closest one

                // Pythagorean theorem to find length of vector
                float length = (float)MathExtension.Hypotenuse((float)closest[1] - coords[0], (float)closest[2] - coords[1]);

                laser.SetLength(length); // Modify the laser to the correct length

                // Calculates reflected vector using the laser's vector and a new vector representing the side of the Model
                Vector2d resultantV = ReflectionVector(laser.vect, new Vector2d(10d, 10d * (float)closest[3]));

                ReflectionCallback((Model)closest[0], laser);

                return new Object[] { CreateModel.CreateReflectedLaser((float) closest[1], (float) closest[2], resultantV),
                    closest[0] };
            }

            return null;
        }

        /**
         * Returns a vector reflected from the incidence vector off of the surface
         * vector
         * 
         * @param incidence
         * @param surface
         * @return
         */
        private static Vector2d ReflectionVector(Vector2d incidence, Vector2d surface)
        {
            /*Vector2d resultant = new Vector2d();
            Vector2d normal;

            // Check for vertical sides of blocks, because JOML is stupid about infinite slopes
            if (surface.Y == double.NegativeInfinity) {
                normal = new Vector2d(1, 0);
            } else if (surface.Y == double.PositiveInfinity) {
                normal = new Vector2d(-1, 0);
            } else {
                // Normalize a perpendicular vector otherwise
                normal = surface.perpendicular().normalize();
            }
            normal.mul(2d * incidence.dot(normal));

            // Vector calculations to find reflected ray
            incidence.sub(normal, resultant);

            return resultant;
            */
            return surface;
        }


        static float[] v;
        static float sl;
        static float xIntercept;
        static float yIntercept;

        private static void FindIntersects(LaserModel laser, List<Model> models)
        {
            intersects.Clear(); // Remove existing intersects from the list
            intersects.TrimExcess();
            float slope = (float)System.Math.Tan(laser.GetAngle());
            coords = laser.GetCoords();
            int xDir = laser.xDir;
            int yDir = laser.yDir;
            if (slope < .01f && slope > 0)
            {
                slope = .01f;
            }
            else if (slope > -.01f && slope < 0)
            {
                slope = -.01f;
            }

            // For all Models in the scene
            foreach (Model m in models)
            {
                // Don't intersect with lasers or UI Elements
                if (!(m is LaserModel) && !(m is UIElement))
                {
                    for (int side = 0; side < m.sideCount; side++)
                    {
                        v = GetY1X1(m, side);
                        sl = GetSlope(v);

                        // If the laser is vertical
                        if (slope == float.PositiveInfinity || slope == float.NegativeInfinity)
                        {

                            // If both are vertical
                            if (sl == float.PositiveInfinity || sl == float.NegativeInfinity)
                            {
                                xIntercept = 100f; // They will never intercept
                                yIntercept = 100f;
                            }
                            else
                            {
                                // Use separate math for vertical laser and non-vertical edge
                                yIntercept = sl * (coords[0] - v[0]) + v[1];
                                xIntercept = coords[0];
                            }
                        }
                        else if (sl < float.PositiveInfinity && sl > float.NegativeInfinity)
                        {
                            // Make sure the line isn't vertical if unsure

                            // Calculate intersection points of the two lines
                            xIntercept = (-sl * v[0] + slope * coords[0] + v[1] - coords[1]) / (slope - sl);
                            yIntercept = sl * (xIntercept - v[0]) + v[1];
                        }
                        else
                        {
                            xIntercept = v[0];

                            // If vertical, use easier methods for finding intersections
                            yIntercept = slope * (xIntercept - coords[0]) + coords[1];
                        }

                        // Check if the point of intersection  is in the correct direction
                        if (((xIntercept - coords[0]) * xDir >= 0) && ((yIntercept - coords[1]) * yDir >= 0))
                        {
                            // Check if the point lies on a side of the polygon
                            if (((xIntercept <= v[0]) && (xIntercept >= v[2]))
                                    || ((xIntercept >= v[0]) && (xIntercept <= v[2])))
                            {

                                //TODO: Check if this works for all polygons (it might, not sure)
                                if (((yIntercept <= v[1]) && (yIntercept >= v[3]))
                                        || ((yIntercept >= v[1]) && (yIntercept <= v[3])))
                                {
                                    intersects.Add(new object[] { m, xIntercept, yIntercept, sl });
                                }
                            }
                        }
                    }
                }
            }
        }

        static float[] midpoint;
        static float length;
        /**
         * Returns the object data of the closest intersection point
         * 
         * @return
         */
        private static object[] GetClosestIntersection()
        {
            // Start with a massive value
            closest = new Object[] { null, float.MaxValue / 2f, float.MaxValue / 2f, 0f };
            length = 0;
            midpoint = new float[2];

            List<Object[]> inters = new List<Object[]>();
            inters.AddRange(intersects);

            // For all intersecting points
            foreach (object[] b in intersects)
            {
                try
                {
                    length = (float)MathExtension.Hypotenuse((float)b[1] - coords[0], (float)b[2] - coords[1]);
                    // If the new object is closer than the old one
                    if ((MathExtension.Hypotenuse((float)b[1] - coords[0], (float)b[2] - coords[1])) < 
                        (MathExtension.Hypotenuse((float)closest[1] - coords[0], (float)closest[2] - coords[1])))
                    {
                        midpoint[0] = ((float)b[1] + coords[0]) / 2f;
                        midpoint[1] = ((float)b[2] + coords[1]) / 2f;
                        // Ensure that the new intersection point isn't at the exact same spot
                        if (length > 0.05f)
                        {
                            bool inside = false;

                            foreach (Model m in GameInstance.objectManager.GetModels())
                            {
                                if (UIUtils.checkIntersection(m.vertices, midpoint[0], midpoint[1]))
                                {
                                    inside = true;
                                }
                            }

                            if (!inside)
                                closest = b; // set the new one to the closest
                        }
                    }
                }
                catch
                {
                }
            }

            return closest;
        }

        /**
         * Returns an array of the correct vertices for the given side of any
         * n-sided polygon model
         * 
         * @param mod
         * @param side
         * @return
         */
        private static float[] GetY1X1(Model mod, int side)
        {
            if (side == 0)
            {
                // If the first side, use last vertex and first
                return new float[] {
                    mod.vertices[mod.vertices.Length - 3],
                    mod.vertices[mod.vertices.Length - 2],
                    mod.vertices[0], mod.vertices[1]
            };
            }
            else if (side > 0)
            {
                // else use the set determined by side number
                return new float[] { mod.vertices[side * 3 - 3], mod.vertices[side * 3 - 2], mod.vertices[side * 3],
                    mod.vertices[side * 3 + 1] };
            }
            else
            {
                return null;
            }
        }

        /**
         * Calculates the slope of a line between the two passed vertices
         * 
         * @param vert
         * @return
         */
        private static float GetSlope(float[] vert)
        {
            return (vert[3] - vert[1]) / (vert[2] - vert[0]);
        }

        /**
         * Executes methods for certain objects in the scene specified below
         * @param m
         * @param l
         */
        private static void ReflectionCallback(Model m, LaserModel l)
        {
            if (m is LaserStop)
            {
                ((LaserStop)m).LaserIntersection();
            }
        }

    }
}