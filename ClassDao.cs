using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AppCadastro
{

    public class Campos //Inner Class (Classe Interna)
    {
        public int id_prod;
        public string nome_prod;
        public string descr_prod;
        public decimal valor_unit;
        public int qtde_estoque;
        public string fornecedor;
    }


    public class ClassDao
    {
        public ClassDao()
        {
        }

        public Campos campos = new Campos();

        public MySqlConnection minhaConexao;

        public string usuarioBD = "root";
        public string senhaBD = "root";
        public string servidor = "localhost";
        string bdestoque;
        string tabela_prod;

        public void Conecte(string BancoDados, string Tabela)
        {
            bdestoque = BancoDados;
            tabela_prod = Tabela;
            minhaConexao = new MySqlConnection("server=" + servidor + "; database=" + bdestoque + "; uid=" + usuarioBD + "; password=" + senhaBD);
        }

        void Abrir()
        {
            minhaConexao.Open();
        }

        void Fechar()
        {
            minhaConexao.Close();
        }

        public void PreencheTabela(System.Windows.Forms.DataGridView dataGridView)
        {
            Abrir();

            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select * from " + tabela_prod, minhaConexao);

            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, tabela_prod);
            dataGridView.DataSource = dataSet;
            dataGridView.DataMember = tabela_prod;

            Fechar();
        }
        public void PreencheTabela(System.Windows.Forms.DataGridView dataGridView, string busca)
        {
            Abrir();
            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select * from " + tabela_prod
            + " where nome_prod like " + "'" + busca + "%';", minhaConexao);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, tabela_prod);
            dataGridView.DataSource = dataSet;
            dataGridView.DataMember = tabela_prod;
            Fechar();
        }

        public void PreencheTabela(System.Windows.Forms.DataGridView dataGridView, int busca)
        {
            Abrir();
            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select * from " + tabela_prod
            + " where id_prod = " + busca + ";", minhaConexao);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, tabela_prod);
            dataGridView.DataSource = dataSet;
            dataGridView.DataMember = tabela_prod;
            Fechar();
        }

        public void Insere(string campoNome, string campoDescr, decimal campoValor, int campoQtde, string campoForn)
        {
            Abrir();
            //insert into tabelaTeste (nome,endereco,salario) values ("Juca", "rua tal", 1230.70)

            MySqlCommand comando = new MySqlCommand("insert into " + tabela_prod
            + " (nome_prod, descr_prod, valor_unit, qtde_estoque, fornecedor) "  +
            "values(@nome_prod, @descr_prod, @valor_unit, @qtde_estoque, @fornecedor)", minhaConexao);
            comando.Parameters.AddWithValue("@nome_prod", campoNome);
            comando.Parameters.AddWithValue("@descr_prod", campoDescr);
            comando.Parameters.AddWithValue("@valor_unit", campoValor);
            comando.Parameters.AddWithValue("@qtde_estoque", campoQtde);
            comando.Parameters.AddWithValue("@fornecedor", campoForn);
            comando.ExecuteNonQuery();
            Fechar();
        }

        public void Consulta(string campoNome)
        {
            //consulta por nome
            Abrir();

            MySqlCommand comando = new MySqlCommand("select * from " + tabela_prod
            + " where nome_prod = '" + campoNome + "'", minhaConexao);
            MySqlDataReader dtReader = comando.ExecuteReader();
            if (dtReader.Read())
            {
                campos.id_prod = int.Parse(dtReader["id_prod"].ToString());
                campos.nome_prod = dtReader["nome_prod"].ToString();
                campos.descr_prod = dtReader["descr_prod"].ToString();
                campos.valor_unit = decimal.Parse(dtReader["valor_unit"].ToString());
                campos.qtde_estoque = int.Parse(dtReader["qtde_estoque"].ToString());
                campos.fornecedor = dtReader["fornecedor"].ToString();

            }
            Fechar();
        }

        public void Consulta(int id_prod)
        {
            //sobrecarga do método de consulta para permitir consulta por id também
            Abrir();

            MySqlCommand comando = new MySqlCommand("select * from " + tabela_prod
            + " where id_prod = '" + id_prod + "'", minhaConexao);
            MySqlDataReader dtReader = comando.ExecuteReader();
            if (dtReader.Read())
            {
                campos.id_prod = int.Parse(dtReader["id_prod"].ToString());
                campos.nome_prod = dtReader["nome_prod"].ToString();
                campos.descr_prod = dtReader["descr_prod"].ToString();
                campos.valor_unit = decimal.Parse(dtReader["valor_unit"].ToString());
                campos.qtde_estoque = int.Parse(dtReader["qtde_estoque"].ToString());
                campos.fornecedor = dtReader["fornecedor"].ToString();


            }
            Fechar();
        }

        public void Atualiza(int id_prod, string campoNome, string campoDescr, decimal campoValor, int campoQtde, string campoForn)
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("update " + tabela_prod
                         + " set nome_prod=@nome_prod, descr_prod=@descr_prod, "
                         + "valor_unit=@valor_unit, " + "qtde_estoque=@qtde_estoque, " + "fornecedor=@fornecedor where id_prod=@id_prod",
                         minhaConexao);

            comando.Parameters.AddWithValue("@id_prod", id_prod);
            comando.Parameters.AddWithValue("@nome_prod", campoNome);
            comando.Parameters.AddWithValue("@descr_prod", campoDescr);
            comando.Parameters.AddWithValue("@valor_unit", campoValor);
            comando.Parameters.AddWithValue("@qtde_estoque", campoQtde);
            comando.Parameters.AddWithValue("@fornecedor", campoForn);
            comando.ExecuteNonQuery();

            Fechar();
        }

        public void Deleta(int id_prod)
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("delete from "
                                                    + tabela_prod + " where id_prod = @id_prod", minhaConexao);
            comando.Parameters.AddWithValue("@id_prod", id_prod);
            comando.ExecuteNonQuery();

            Fechar();
        }

        public int NumRegistro()
        {
            Abrir();

            //MAX retorna o número do último valor do id
            MySqlCommand comando = new MySqlCommand("SELECT MAX(id_prod) FROM "
                                                    + tabela_prod, minhaConexao);

            //ExecuteScalar retorna um dado do tipo object. É preciso converter para string.
            string n = comando.ExecuteScalar().ToString();

            //Agora convertemos o dado para int e somamos um para obter o número do próximo registro
            int num = int.Parse(n) + 1;
            Fechar();

            return num; //retorna o número do próximo registro do autoincrement do id
        }
    }
}
