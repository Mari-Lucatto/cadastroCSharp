using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AppCadastro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ClassDao dao = new ClassDao(); //instanciar

        void LimparCampos()
        {
            txtNome.Clear();
            txtDescr.Clear();
            txtValor.Clear();
            txtQtde.Clear();
            txtForn.Clear();
            //insere no label o numero do proximo registro a cadastrar
            //lblNumCli.Text = dao.NumRegistro().ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //estabelecer conexão com o bd
            dao.Conecte("bdestoque", "tabela_prod");
            dao.PreencheTabela(dataGridView1);
            LimparCampos();
        }

        private void BtInserir_Click(object sender, EventArgs e)
        {
            //inserir (salvar)
            if (txtNome.Text == "" || txtDescr.Text == "" || txtValor.Text == "" || txtQtde.Text == "" || txtForn.Text == "")
            {
                MessageBox.Show("Campos em Branco", "AVISO");
            }
            else
            {
                dao.Insere(txtNome.Text, txtDescr.Text, decimal.Parse(txtValor.Text), int.Parse(txtQtde.Text), txtForn.Text);
                MessageBox.Show("Registro gravado com sucesso", "Informação do Sistema");
                LimparCampos();
                dao.PreencheTabela(dataGridView1);
                BtInserir.Enabled = false;
            }
        }

        void ExibirDados()
        {
            lblNumCli.Text = dao.campos.id_prod.ToString();
            txtNome.Text = dao.campos.nome_prod;
            txtDescr.Text = dao.campos.descr_prod;
            txtValor.Text = dao.campos.valor_unit.ToString();
            txtQtde.Text = dao.campos.qtde_estoque.ToString();
            txtForn.Text = dao.campos.fornecedor.ToString();
            btAlterar.Enabled = true;
            btDeletar.Enabled = true;
            BtInserir.Enabled = false;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numLinha = e.RowIndex; //retorna o número da linha selecionada
            if (numLinha >= 0)
            {
                int idCliente = int.Parse(dataGridView1.Rows[numLinha].Cells[0].Value.ToString());
                dao.Consulta(idCliente);
                ExibirDados();
            }
        }

        private void BtConsultar_Click(object sender, EventArgs e)
        {
            //consultar por nome
            if (txtNome.Text != "")
            {
                //dao.Consulta(txtNome.Text);
                //ExibirDados();
                dao.PreencheTabela(dataGridView1, txtNome.Text);
            }
            else if (txtId.Text != "")
            {
                //dao.Consulta(int.Parse(txtId.Text));
                //ExibirDados();
                dao.PreencheTabela(dataGridView1, int.Parse(txtId.Text));
            }
        }

        private void BtNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
            BtInserir.Enabled = true;
            btAlterar.Enabled = false;
            btDeletar.Enabled = false;
            dao.PreencheTabela(dataGridView1);
        }

        private void BtAlterar_Click(object sender, EventArgs e)
        {
            //alterar 
            dao.Atualiza(int.Parse(lblNumCli.Text), txtNome.Text, txtDescr.Text, decimal.Parse(txtValor.Text), 
            int.Parse(txtQtde.Text), txtForn.Text);

            dao.PreencheTabela(dataGridView1);
            MessageBox.Show("Registro alterado com sucesso", "AVISO");
        }

        private void BtDeletar_Click(object sender, EventArgs e)
        {
            //deletar
            if (MessageBox.Show("Deseja mesmo excluir esse registro?", "AVISO DE EXCLUSÃO",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dao.Deleta(int.Parse(lblNumCli.Text));
                MessageBox.Show("Registro excluído com sucesso");
                dao.PreencheTabela(dataGridView1);
                LimparCampos();
                btAlterar.Enabled = false;
                btDeletar.Enabled = false;
            } else {
                MessageBox.Show("Registro Mantido");
            }
        }

        private void lblNumCli_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

