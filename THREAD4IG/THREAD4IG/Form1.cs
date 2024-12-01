using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace THREAD4IG
{
    public partial class Form1 : Form
    {
        private int valoreCountdown; // Valore del countdown
        private bool inPausa; // Flag per indicare se il timer ии in pausa
        private Thread threadCountdown; // Thread per il countdown
        private Label etichettaTempo; // Etichetta per visualizzare il countdown

        public Form1()
        {
            InitializeComponent();
        }

        private void Countdown()
        {
            while (valoreCountdown >= 0)
            {
                if (inPausa) // Se in pausa, attende
                {
                    Thread.Sleep(100);
                    continue;
                }

                // Aggiorna l'etichetta con il valore attuale del countdown
                this.Invoke(new Action(() =>
                {
                    etichettaTempo.Text = valoreCountdown.ToString();
                }));

                // Attende 1 secondo
                Thread.Sleep(1000);

                // Decrementa il valore del countdown
                valoreCountdown--;

                // Quando il countdown termina
                if (valoreCountdown < 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Tempo scaduto!");
                    }));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Ottieni il valore dal controllo NumericUpDown
            valoreCountdown = (int)numericUpDown1.Value;

            // Configura l'etichetta per visualizzare il countdown
            etichettaTempo = new Label
            {
                Location = new Point(10, 50),
                Font = new Font(FontFamily.GenericSansSerif, 36),
                Size = new Size(200, 100)
            };
            this.Controls.Add(etichettaTempo);

            // Ferma un thread precedente, se esiste
            threadCountdown?.Abort();

            // Avvia un nuovo thread per il countdown
            inPausa = false;
            threadCountdown = new Thread(Countdown);
            threadCountdown.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inPausa = true; // Metti il countdown in pausa
        }

        private void button3_Click(object sender, EventArgs e)
        {
            inPausa = false; // Riprendi il countdown
        }
    }
}
