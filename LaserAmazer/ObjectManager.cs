using LaserAmazer.Render;
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
        public Model AddModel(Model m)
        {
            addModels.Add(m);
            return m;
        }

        /**
         * Returns the models Arraylist
         * @return
         */
        public List<Model> GetModels()
        {
            return models;
        }

        public void SetModels(List<Model> models)
        {
            ObjectManager.models = models;
        }

        /**
         * Flushes the buffer List into the primary Lists
         */
        public void UpdateModels()
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
        public void RemoveModel(Model m)
        {
            models.Remove(m);
        }

        /**
         * Returns the model at the given index
         * 
         * @param index
         * @return
         */
        public Model GetModel(int index)
        {
            return models[index];
        }

        /**
         * Calls the render() function on all models and lasers
         */
        public void RenderAll()
        {
            for (int i = 0; i < GetModels().Count; i++)
            {
                models[i].Render();
            }
        }

        public void ClearAll()
        {
            models.Clear();
        }

    }
}