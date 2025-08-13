using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimplePong
{
    public partial class Main : Form
    {
        Rectangle paddle;
        Rectangle ball;
        Point velocity;
        int score = 0;

        bool leftPressed, rightPressed, spacePressed;
        readonly System.Windows.Forms.Timer timer = new();

        public Main()
        {
            InitializeComponent();

            Text = "Simple Pong – ←/→ di chuyển, R để chơi lại";
            DoubleBuffered = true;

            if (ClientSize.Width == 0 || ClientSize.Height == 0)
                ClientSize = new Size(800, 500);

            Paint += OnPaint;
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            ResetGame();

            timer.Interval = 16;
            timer.Tick += OnUpdate;
            timer.Start();
        }

        void ResetGame()
        {
            int w = ClientSize.Width;
            int h = ClientSize.Height;
            paddle = new Rectangle(w / 2 - 60, h - 28, 120, 12);
            ball = new Rectangle(w / 2 - 8, h / 2 - 8, 16, 16);
            velocity = new Point(4, -4);
            score = 0;
        }

        void OnUpdate(object? sender, EventArgs e)
        {
            // 1) Update input → paddle
            int speed = 7;
            if (leftPressed) paddle.X -= speed;
            if (rightPressed) paddle.X += speed;
            paddle.X = Math.Max(0, Math.Min(ClientSize.Width - paddle.Width, paddle.X));

            // 2) Update ball position
            ball.X += velocity.X;
            ball.Y += velocity.Y;

            // 3) Ball collision with walls
            if (ball.Left <= 0 || ball.Right >= ClientSize.Width)
                velocity.X = -velocity.X;
            if (ball.Top <= 0)
                velocity.Y = -velocity.Y;

            // 4) Ball collision with paddle
            if (velocity.Y > 0 && ball.IntersectsWith(paddle))
            {
                ball.Y = paddle.Top - ball.Height; // Prevent ball from getting stuck
                velocity.Y = -Math.Abs(velocity.Y); // Bounce upward

                // Add spin based on hit position
                int hitPos = (ball.Left + ball.Width / 2) - (paddle.Left + paddle.Width / 2);
                velocity.X += Math.Sign(hitPos);
                velocity.X = Math.Max(-8, Math.Min(8, velocity.X));

                score++;

                // Increase ball speed every 5 points
                if (score % 5 == 0)
                {
                    velocity.X += Math.Sign(velocity.X);
                    velocity.Y += Math.Sign(velocity.Y);
                }
            }

            // Temporary speed boost when space is pressed
            if (spacePressed)
            {
                velocity.X = Math.Sign(velocity.X) * 12; // Fixed speed when holding space
                velocity.Y = Math.Sign(velocity.Y) * 12;
            }
            else
            {
                // Reset speed to normal when space is not pressed
                velocity.X = Math.Clamp(velocity.X, -8, 8);
                velocity.Y = Math.Clamp(velocity.Y, -8, 8);
            }

            // 5) Game over if ball falls below paddle
            if (ball.Top > ClientSize.Height)
            {
                timer.Stop();
                Text = $"Game Over – Điểm: {score} (nhấn R để chơi lại)";
            }

            Invalidate();
        }

        void OnPaint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.FromArgb(25, 25, 28));

            using var brushPaddle = new SolidBrush(Color.DeepSkyBlue);
            using var brushBall = new SolidBrush(Color.OrangeRed);
            g.FillRectangle(brushPaddle, paddle);
            g.FillEllipse(brushBall, ball);

            using var pen = new Pen(Color.FromArgb(60, 255, 255, 255), 1);
            g.DrawLine(pen, ClientSize.Width / 2, 0, ClientSize.Width / 2, ClientSize.Height);

            using var font = new Font("Segoe UI", 12, FontStyle.Bold);
            string hud = $"Score: {score} | Speed: {Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y):0.0}";
            var size = g.MeasureString(hud, font);
            g.DrawString(hud, font, Brushes.White, ClientSize.Width - size.Width - 10, 10);
        }

        void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) leftPressed = true;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) rightPressed = true;
            if (e.KeyCode == Keys.Space) spacePressed = true;

            if (e.KeyCode == Keys.R && !timer.Enabled)
            {
                ResetGame();
                timer.Start();
                Text = "Simple Pong – ←/→ di chuyển, R để chơi lại";
            }
        }

        void OnKeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) leftPressed = false;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) rightPressed = false;
            if (e.KeyCode == Keys.Space) spacePressed = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Optional: Add any additional initialization logic here
        }
    }
}
