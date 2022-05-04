using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoCSharp
{
    public partial class form_progressBar : Form
    {
        public form_progressBar()
        {
            InitializeComponent();
        }

        private Task ProcessandoDados(List<string> list, IProgress<relatorioProgresso> progress)
        {
            int index = 1;
            int totalProgresso = list.Count;
            var relatorioProgresso = new relatorioProgresso();
            return Task.Run(() => {
                for(int i = 0; i < totalProgresso; i++)
                {
                    relatorioProgresso.percentualCompleto = index++ * 100 / totalProgresso;
                    progress.Report(relatorioProgresso); //confirmar variavel
                    Thread.Sleep(10);
                }            
            });
        }

        private async void btn_Iniciar_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for(int i = 0; i < 100; i++)            
                list.Add(i.ToString());
            lbl_Status.Text = "Trabalhando...";
            var progresso = new Progress<relatorioProgresso>(); //da onde vem progresso
            progresso.ProgressChanged += (o, report) => {
                lbl_Status.Text = String.Format("Processando...{0}%",report.percentualCompleto);
                progressBar.Value = report.percentualCompleto;
                progressBar.Update();
            };
            await ProcessandoDados(list, progresso);
            lbl_Status.Text = "Completo!";

        }

        private void btn_Fechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
