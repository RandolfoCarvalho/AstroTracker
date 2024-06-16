using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace WFComNet
{
    public partial class MainForm2 : Form
    {
        private GLControl glControl;
        private float cameraDistance = 5.0f;
        private float cameraAngleX = 0.0f;
        private float cameraAngleY = 0.0f;
        private bool isMouseDown = false;
        private Point lastMousePos;

        public MainForm2()
        {
            InitializeComponent();
            InitializeGLControl();
        }
        //Esta função inicializa e configura o GLControl que será usado para renderização OpenGL dentro do formulário
        private void InitializeGLControl()
        {
            glControl = new GLControl(new GraphicsMode(32, 24, 0, 4))
            {
                Dock = DockStyle.Fill,
                VSync = true
            };

            glControl.Load += GlControl_Load;
            glControl.Paint += GlControl_Paint;
            glControl.Resize += GlControl_Resize;
            glControl.MouseDown += GlControl_MouseDown;
            glControl.MouseUp += GlControl_MouseUp;
            glControl.MouseMove += GlControl_MouseMove;
            glControl.MouseWheel += GlControl_MouseWheel;

            this.Controls.Add(glControl);
        }

        //Este método é chamado quando o GLControl é carregado e inicializa o ambiente OpenGL
        private void GlControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(System.Drawing.Color.Black);
            GL.Enable(EnableCap.DepthTest);
            //Configura a viewport OpenGL para coincidir com as dimensões do GLControl.
            SetupViewport();
        }

        private void GlControl_Resize(object sender, EventArgs e)
        {
            SetupViewport();
        }
        //Configura a viewport e a matriz de projeção OpenGL
        private void SetupViewport()
        {
            int width = glControl.Width;
            int height = glControl.Height;

            GL.Viewport(0, 0, width, height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, width / (float)height, 1.0f, 100.0f);
            GL.LoadMatrix(ref perspective);
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            Matrix4 lookat = Matrix4.LookAt(
                new Vector3(
                    (float)(cameraDistance * Math.Cos(cameraAngleY) * Math.Cos(cameraAngleX)),
                    (float)(cameraDistance * Math.Sin(cameraAngleX)),
                    (float)(cameraDistance * Math.Sin(cameraAngleY) * Math.Cos(cameraAngleX))),
                Vector3.Zero,
                Vector3.UnitY
            );
            GL.LoadMatrix(ref lookat);

            DrawCircle3D(1.0f, 50);

            glControl.SwapBuffers();
        }
        // Desenha um círculo 3D usando OpenGL
        private void DrawCircle3D(float radius, int minSegments)
        {
            // Adjust segments based on desired smoothness and radius
            int segments = Math.Max(minSegments, (int)(radius * 10));

            GL.Begin(PrimitiveType.TriangleFan);

            // Set a distinct color for better visualization (optional)
            GL.Color3(Color.Red);

            // Define vertex in the center, slightly elevated for 3D effect
            GL.Vertex3(0.0f, 0.0f, 0.1f); // Adjust z-coordinate for desired elevation

            // Calculate vertices around the circle's circumference
            for (int i = 0; i <= segments; i++) // Iterate from 0 to segments (inclusive)
            {
                float angle = 2.0f * (float)Math.PI * i / segments;
                float x = radius * (float)Math.Cos(angle);
                float y = radius * (float)Math.Sin(angle);

                // Include z-coordinate for a full 3D circle (adjustable)
                GL.Vertex3(x, y, 0.0f); // Adjust z-coordinate for desired depth
            }

            GL.End();
        }

        private void GlControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                lastMousePos = e.Location;
            }
        }

        private void GlControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void GlControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                float deltaX = e.X - lastMousePos.X;
                float deltaY = e.Y - lastMousePos.Y;

                cameraAngleY += deltaX * 0.01f;
                cameraAngleX += deltaY * 0.01f;

                lastMousePos = e.Location;
                glControl.Invalidate();
            }
        }
        private void GlControl_MouseWheel(object sender, MouseEventArgs e)
        {
            cameraDistance -= e.Delta * 0.01f;
            if (cameraDistance < 1.0f) cameraDistance = 1.0f;
            if (cameraDistance > 100.0f) cameraDistance = 100.0f;
            glControl.Invalidate();
        }
        private void MainForm2_Load(object sender, EventArgs e)
        {
            // Este método pode ser usado para qualquer inicialização necessária quando o formulário carregar
        }
    }
}
