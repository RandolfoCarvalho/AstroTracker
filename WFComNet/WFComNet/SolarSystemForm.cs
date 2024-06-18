using System.Drawing;
using System.Windows.Forms;
using System;

public class SolarSystemForm : Form
{
    private SolarSystem solarSystem;
    private Button AcelerarButton;
    private Timer timer;
    private Button button1;
    public static double valor = 0.01;
    //106221.954 é para simular 1 dia virtual = 1 segundo no mundo real
    private double accelerationFactor = 106221.954;
    private double elapsedDays = 0;
    private Button button2;
    private Button button3;
    private double Scale = 100;
    private Button button4;
    private Label elapsedDaysLabel;
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
            DrawPlanet(g, solarSystem.Planets[i], Color.Red);
        }
    }
    private void DrawPlanet(Graphics g, Planet planet, Color color)
    {
       
        float x = (float)(planet.Position.X * Scale) + this.ClientSize.Width / 2;
        float y = (float)(planet.Position.Y * Scale) + this.ClientSize.Height / 2;

        float size;
        // Tamanho do ponto representando o planeta
        size = 10 - (1 / 70);
        
        using (SolidBrush brush = new SolidBrush(color))
        {
            //desenha os planetas
            g.FillEllipse(brush, x - size / 2, y - size / 2, size, size);
            string planetName = planet.Name; // Supondo que o nome do planeta esteja em planet.Name
            float textX = x + size; // Ajuste para posicionar o texto ao lado da bolinha
            float textY = y - size / 2; // Ajuste para posicionar o texto centralizado na altura da bolinha
            using (Font font = new Font("Arial", 8)) // Fonte para o nome do planeta
            {
                g.DrawString(planetName, font, Brushes.White, textX, textY);
            }
            
        }
        using(SolidBrush brush = new SolidBrush(Color.Red))
        {
            foreach (var point in planet.Trajectory)
            {
                float trailX = (float)(point.X * Scale) + this.ClientSize.Width / 2;
                float trailY = (float)(point.Y * Scale) + this.ClientSize.Height / 2;
                g.FillEllipse(brush, trailX - 1, trailY - 1, 2, 2); // Pequenos pontos para a trilha
            }
        }

    }
    private void InitializeComponent()
    {
            this.AcelerarButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.elapsedDaysLabel = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
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
            this.button1.Click += new System.EventHandler(this.EstabilizarButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(806, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "+Zoom";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.PlusZoomButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(899, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "-Zoom";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.LessZoomButton_Click);
            // 
            // elapsedDaysLabel
            // 
            this.elapsedDaysLabel.AutoSize = true;
            this.elapsedDaysLabel.Location = new System.Drawing.Point(34, 21);
            this.elapsedDaysLabel.Name = "elapsedDaysLabel";
            this.elapsedDaysLabel.Size = new System.Drawing.Size(94, 13);
            this.elapsedDaysLabel.TabIndex = 4;
            this.elapsedDaysLabel.Text = "elapsedDaysLabel";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 323);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Pausar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.PausarButton_Click);
            // 
            // SolarSystemForm
            // 
            this.ClientSize = new System.Drawing.Size(986, 518);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.elapsedDaysLabel);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AcelerarButton);
            this.Name = "SolarSystemForm";
            this.Load += new System.EventHandler(this.SolarSystemForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    private void OnTimerTick(object sender, EventArgs e)
    {
        elapsedDays += (timer.Interval / (24.0 * 60 * 60 * 1000)) * accelerationFactor;
        for (int i = 1; i < solarSystem.Planets.Count; i++)
        {
            solarSystem.Planets[i].UpdatePosition(elapsedDays);
        }
        elapsedDaysLabel.Text = $"Dias: {elapsedDays:F2}";
        this.Invalidate();

    }
    private void SolarSystemForm_Load(object sender, EventArgs e)
    { 
    }

    private void AcelerarButton_Click(object sender, EventArgs e)
    {
        accelerationFactor *= 2;
    }

    private void EstabilizarButton_Click(object sender, EventArgs e)
    {
        accelerationFactor = 106221.954;
    }

    private void PlusZoomButton_Click(object sender, EventArgs e)
    {
        Scale += 8;
    }

    private void LessZoomButton_Click(object sender, EventArgs e)
    {
        Scale -= 8;
    }

    private void PausarButton_Click(object sender, EventArgs e)
    {
        accelerationFactor = 0;
    }
}