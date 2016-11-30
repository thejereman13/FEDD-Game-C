using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace LaserAmazer.Render
{
    public class Shader
    {

        private int program, vs, fs; // Vertex and fragment shaders

        /**
         * Loads the shaders from their respective .vs or .fs files
         * @param path
         */
        public Shader(string path)
        {
            program = GL.CreateProgram();

            // Vertex shader
            vs = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vs, ReadFile(path + ".vs"));
            GL.CompileShader(vs);

            /*
            if (glGetShaderi(vs, GL_COMPILE_STATUS) != 1) {
                System.err.println(glGetShaderInfoLog(vs)); // Print shader error
                System.exit(1);
            }
            */

            // Fragment shader
            fs = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fs, ReadFile(path + ".fs"));
            GL.CompileShader(fs);

            /* Check for shader error
            if (glGetShaderi(fs, GL_COMPILE_STATUS) != 1) {
                System.err.println(glGetShaderInfoLog(fs)); // Print shader error
                System.exit(1);
            }*/

            GL.AttachShader(program, vs);
            GL.AttachShader(program, fs);

            GL.BindAttribLocation(program, 0, "vertices");
            GL.BindAttribLocation(program, 1, "textures");

            /*
            glLinkProgram(program);
            // Check for link error
            if (glGetProgrami(program, GL_LINK_STATUS) != 1) {
                System.err.println(glGetProgramInfoLog(program));
                System.exit(1);
            }

            glValidateProgram(program);
            // Check for validate error
            if (glGetProgrami(program, GL_VALIDATE_STATUS) != 1) {
                System.err.println(glGetProgramInfoLog(program));
                System.exit(1);
            }
            */
        }

        protected void Finalize()
        {
            GL.DetachShader(program, vs);
            GL.DetachShader(program, fs);
            GL.DeleteShader(vs);
            GL.DeleteShader(fs);
            GL.DeleteProgram(program);
        }

        /**
         * Installs the program object as part of current rendering state
         */
        public void Bind()
        {
            GL.UseProgram(program);
        }

        /**
         * Removes/resets the program object
         */
        public void Unbind()
        {
            GL.UseProgram(0);
        }

        /**
         * Reads a shader file to a String.
         * @param path
         * @return String with the contents of the shader file
         */
        private string ReadFile(string path)
        {
            try
            {
                using (StreamReader input = new StreamReader("/res/shaders/shader"))
                {
                    return input.ReadToEnd();
                }
            }
            catch
            {
            }

            return null;
        }

        public void UpdateUniforms(Camera camera, Matrix4d target)
        {
            SetUniform("sampler", 0);
            SetUniform("projection", camera.getProjection() * target);
        }

        /**
         * Load uniform variables defined in the shader files
         * @param name
         * @param value
         */
        private void SetUniform(string name, int value)
        {
            int location = GL.GetUniformLocation(program, name);

            if (location != -1)
                GL.Uniform1(location, value);
        }

        /**
         * Load uniform variables defined in the shader files
         * @param name
         * @param matrix
         */
        private void SetUniform(string name, Matrix4d matrix)
        {
            int location = GL.GetUniformLocation(program, name);

            if (location != -1)
                GL.UniformMatrix4(location, false, ref matrix);
        }

    }
}