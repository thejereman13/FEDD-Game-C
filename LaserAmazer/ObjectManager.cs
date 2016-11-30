using System.Collections.Generic;

namespace LaserAmazer
{
    public class ObjectManager
    {

        private static List<Model> models = new List<Model>();
        private List<Model> addModels = new List<Model>();

        /**
         * Adds a passed Model to the stored List
         * 
         * @param Model
         *            m
         */
        public Model addModel(Model m)
        {
            addModels.Add(m);
            return m;
        }

        /**
         * Returns the models Arraylist
         * @return
         */
        public List<Model> getModels()
        {
            return models;
        }

        public void setModels(List<Model> models)
        {
            ObjectManager.models = models;
        }

        /**
         * Flushes the buffer List into the primary Lists
         */
        public void updateModels()
        {
            models.AddRange(addModels);
            addModels.Clear();
            addModels.TrimExcess();
        }

        /**
         * Removed the model object at the index specified
         * 
         * @param index
         */
        public void removeModel(int index)
        {
            models.RemoveAt(index);
        }

        /**
         * Removes the model passed
         * 
         * @param m
         */
        public void removeModel(Model m)
        {
            models.Remove(m);
        }

        /**
         * Returns the model at the given index
         * 
         * @param index
         * @return
         */
        public Model getModel(int index)
        {
            return models[index];
        }

        /**
         * Calls the render() function on all models and lasers
         */
        public void renderAll()
        {
            for (int i = 0; i < getModels().Count; i++)
            {
                models[i].render();
            }
        }

        public void clearAll()
        {
            models.Clear();
        }

    }
}