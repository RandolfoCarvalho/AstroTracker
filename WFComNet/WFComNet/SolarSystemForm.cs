using System.Drawing;
using System.Windows.Forms;
using System;

public class SolarSystemForm : Form
{
    private SolarSystem solarSystem;
    private Button button1;
    private Timer timer;
    private int elapsedMilliseconds = 0;

    public SolarSystemForm()
    {
        this.solarSystem = new SolarSystem();
        this.timer = new Timer();
        this.timer.Interval = 100; // Atualiza a cada 100 milissegundos
        this.timer.Tick += new EventHandler(OnTimerTick);
        this.timer.Start();
        //this.DoubleBuffered = true; // Reduz o flickering
        this.Paint += new PaintEventHandler(OnPaint);
        InitializeComponent();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        elapsedMilliseconds += timer.Interval;
        foreach (Planet planet in solarSystem.Planets)
        {
            planet.UpdatePosition(elapsedMilliseconds * 0.1);
        }
        this.Invalidate();
    }

    private void OnPaint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.Clear(Color.Black);

        // Desenhar o Sol no centro
        DrawPlanet(g, solarSystem.Planets[0], Color.Yellow);

        // Desenhar os planetas
        foreach (Planet planet in solarSystem.Planets)
        {
            DrawPlanet(g, planet, Color.Red);
        }
    }
    private void DrawPlanet(Graphics g, Planet planet, Color color)
    {
        float scale = 10; // Escala para ajustar a visualização
        float x = (float)(planet.Position.X * scale) + this.ClientSize.Width / 2;
        float y = (float)(planet.Position.Y * scale) + this.ClientSize.Height / 2;
        float size = 10; // Tamanho do ponto representando o planeta

        using (SolidBrush brush = new SolidBrush(color))
        {
            g.FillEllipse(brush, x - size / 2, y - size / 2, size, size);
            string planetName = planet.Name; // Supondo que o nome do planeta esteja em planet.Name
            float textX = x + size; // Ajuste para posicionar o texto ao lado da bolinha
            float textY = y - size / 2; // Ajuste para posicionar o texto centralizado na altura da bolinha

            using (Font font = new Font("Arial", 8)) // Fonte para o nome do planeta
            {
                g.DrawString(planetName, font, Brushes.White, textX, textY);
            }
        }
    }

    private void InitializeComponent()
    {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Acelerar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SolarSystemForm
            // 
            this.ClientSize = new System.Drawing.Size(986, 386);
            this.Controls.Add(this.button1);
            this.Name = "SolarSystemForm";
            this.Load += new System.EventHandler(this.SolarSystemForm_Load);
            this.ResumeLayout(false);

    }

    private void SolarSystemForm_Load(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
        foreach (Planet planet in solarSystem.Planets)
        {
            planet.UpdatePosition(100);
            // Atualiza a posição do planeta
        }
        this.Invalidate();
    }
}