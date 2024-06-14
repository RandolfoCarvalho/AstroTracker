using System.Drawing;
using System.Windows.Forms;
using System;

public class SolarSystemForm : Form
{
    private SolarSystem solarSystem;
    private Button AcelerarButton;
    private Timer timer;
    private double elapsedMilliseconds = 0;
    private Button button1;
    public static double valor = 0.01;
    //106221.954 é para simular 1 dia virtual = 1 segundo no mundo real
    private double accelerationFactor = 106221.954;
    private double elapsedDays = 0;
    public SolarSystemForm()
    {
        this.solarSystem = new SolarSystem();
        this.timer = new Timer();
        this.timer.Interval = 50; // Atualiza a cada 100 milissegundos
        this.timer.Tick += new EventHandler(OnTimerTick);
        this.timer.Start();
        this.DoubleBuffered = true; // Reduz o flickering
        this.Paint += new PaintEventHandler(OnPaint);
        InitializeComponent();
    }


    private void OnPaint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.Clear(Color.Black);

        // Desenhar o Sol no centro
        DrawPlanet(g, solarSystem.Planets[0], Color.Yellow);

        //Desenhar os planetas
        for(int i = 1; i < solarSystem.Planets.Count; i++)
        {
            DrawTrajectory(g, solarSystem.Planets[i], Color.Red);
            DrawPlanet(g, solarSystem.Planets[i], Color.Red);
        }
    }
    private void DrawTrajectory(Graphics g, Planet planet, Color color)
    {
        float scale = 40; // Escala para ajustar a visualização

        using (Pen pen = new Pen(color, 1))
        {
            for (int i = 1; i < planet.Trajectory.Count; i++)
            {
                float x1 = (float)(planet.Trajectory[i - 1].X * scale) + this.ClientSize.Width / 2;
                float y1 = (float)(planet.Trajectory[i - 1].Y * scale) + this.ClientSize.Height / 2;
                float x2 = (float)(planet.Trajectory[i].X * scale) + this.ClientSize.Width / 2;
                float y2 = (float)(planet.Trajectory[i].Y * scale) + this.ClientSize.Height / 2;
                g.DrawLine(pen, x1, y1, x2, y2);
            }
        }
    }
    private void DrawPlanet(Graphics g, Planet planet, Color color)
    {
        float scale = 70; // Escala para ajustar a visualização
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
            this.AcelerarButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AcelerarButton
            // 
            this.AcelerarButton.Location = new System.Drawing.Point(12, 265);
            this.AcelerarButton.Name = "AcelerarButton";
            this.AcelerarButton.Size = new System.Drawing.Size(75, 23);
            this.AcelerarButton.TabIndex = 0;
            this.AcelerarButton.Text = "Acelerar";
            this.AcelerarButton.UseVisualStyleBackColor = true;
            this.AcelerarButton.Click += new System.EventHandler(this.AcelerarButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 294);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Estabilizar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SolarSystemForm
            // 
            this.ClientSize = new System.Drawing.Size(986, 518);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AcelerarButton);
            this.Name = "SolarSystemForm";
            this.Load += new System.EventHandler(this.SolarSystemForm_Load);
            this.ResumeLayout(false);

    }
    private void OnTimerTick(object sender, EventArgs e)
    {
        elapsedDays += (timer.Interval / (24.0 * 60 * 60 * 1000)) * accelerationFactor;
        for (int i = 1; i < solarSystem.Planets.Count; i++)
        {
            solarSystem.Planets[i].UpdatePosition(elapsedDays);
        }
        this.Invalidate();
    }
    private void SolarSystemForm_Load(object sender, EventArgs e)
    { 
    }

    private void AcelerarButton_Click(object sender, EventArgs e)
    {
        accelerationFactor *= 2;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        accelerationFactor = 106221.954;
    }
}