﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestorDeEstudantesT7
{
    public partial class FormAtualizarApagarEstudante : Form
    {
        public FormAtualizarApagarEstudante()
        {
            InitializeComponent();
        }

        private void buttonEnviarFoto_Click(object sender, EventArgs e)
        {
            // Abre janela para pesquisar a imagem no computador.
            OpenFileDialog procurarFoto = new OpenFileDialog();

            procurarFoto.Filter = "Selecione a foto (*.jpg;*.png;*.jpeg;*.gif)|*.jpg;*.png;*.jpeg;*.gif";

            if (procurarFoto.ShowDialog() == DialogResult.OK)
            {
                pictureBoxFoto.Image = Image.FromFile(procurarFoto.FileName);
            }
        }

      
        private void FormAtualizarApagarEstudante_Load(object sender, EventArgs e)
        {

        }


        bool Verificar()
        {
            if ((textBoxNome.Text.Trim() == "") ||
               (textBoxSobrenome.Text.Trim() == "") ||
               (textBoxTelefone.Text.Trim() == "") ||
               (textBoxEndereco.Text.Trim() == "") ||
               (pictureBoxFoto.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            Estudante estudante = new Estudante();


            //Linha exclusiva do botão salvar
            int id = Convert.ToInt32(textBoxId.Text);
            string nome = textBoxNome.Text;
            string sobrenome = textBoxSobrenome.Text;
            DateTime nascimento = dateTimePickerNascimento.Value;
            string telefone = textBoxTelefone.Text;
            string endereco = textBoxEndereco.Text;
            string genero = "Feminino";

            if (radioButtonMasculino.Checked == true)
            {
                genero = "Masculino";
            }

            MemoryStream foto = new MemoryStream();

            // Verificar se o aluno tem entre 10 e 100 anos.
            int anoDeNascimento = dateTimePickerNascimento.Value.Year;
            int anoAtual = DateTime.Now.Year;

            if ((anoAtual - anoDeNascimento) < 10 || (anoAtual - anoDeNascimento) > 80)
            {
                MessageBox.Show("O aluno precisa ter entre 10 e 80 anos.",
                    "Ano de nascimento inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (Verificar())
            {
                pictureBoxFoto.Image.Save(foto, pictureBoxFoto.Image.RawFormat);

                if (estudante.atualizarEstudante(id,nome, sobrenome, nascimento, telefone,
                    genero, endereco, foto))
                {
                    MessageBox.Show("Os dados foram atualizados!", "Sucesso!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Não foi possível salvar!", "Erro!",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            else
            {
                MessageBox.Show("Existem campos não preenchidos!", "Campos não preenchidos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
