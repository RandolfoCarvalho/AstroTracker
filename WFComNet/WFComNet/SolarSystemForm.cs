﻿using System.Drawing;
using System.Windows.Forms;
using System;

public class SolarSystemForm : Form
{
    private SolarSystem solarSystem;
    private Button button1;
    private Timer timer;

    public SolarSystemForm()
    {
        this.solarSystem = new SolarSystem();
        this.timer = new Timer();
        this.timer.Interval = 100; // Atualiza a cada 100 milissegundos
        this.timer.Tick += new EventHandler(OnTimerTick);
        this.timer.Start();
        this.DoubleBuffered = true; // Reduz o flickering
        this.Paint += new PaintEventHandler(OnPaint);
        InitializeComponent();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {

        foreach (Planet planet in solarSystem.Planets)
        {
            planet.UpdatePosition(0.01);
            // Atualiza a posição do planeta
        }
        this.Invalidate(); // Força a repintura do formulário
        
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
        float scale = 50; // Escala para ajustar a visualização
        float x = (float)(planet.Position.X * scale) + this.ClientSize.Width / 2;
        float y = (float)(planet.Position.Y * scale) + this.ClientSize.Height / 2;
        float size = 10; // Tamanho do ponto representando o planeta

        using (SolidBrush brush = new SolidBrush(color))
        {
            g.FillEllipse(brush, x - size / 2, y - size / 2, size, size);
        }
    }

    private void InitializeComponent()
    {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(107, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Acelerar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SolarSystemForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
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
            planet.UpdatePosition(0.01);
            // Atualiza a posição do planeta
        }
        this.Invalidate();
    }
}