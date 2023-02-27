using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoBD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Demonstração UPDATE, DELETE E INSERT - SQL");
            Console.WriteLine();

            SqlConnection conexao = new SqlConnection(@"Data Source=LUCAS\SQLEXPRESS;Initial Catalog=Backend_MVCandSQL;Integrated Security=True");

            /* ---------- SELECT COM O SQL ---------- */

            conexao.Open();
            string querrySQL = "SELECT * FROM USERS"; /*<-- Comando no SQL para select*/
            SqlCommand command = new SqlCommand(querrySQL, conexao); /*<-- Comando SQL que relaciona o 'querrySQL' com a conexao 'conexao'*/
            SqlDataReader readerCommand = command.ExecuteReader(); /*<-- Ação no comando SQL 'command' (realiza uma leitura)*/

            if (readerCommand.HasRows)
            {

                Console.WriteLine(" ----- DADOS NA TABELA ----- ");
                Console.WriteLine();
                while (readerCommand.Read())
                {

                    Console.WriteLine("ID: {0}, Email: {1}, Senha: {2}, Nome: {3}", readerCommand["ID"], readerCommand["EMAIL"], readerCommand["SENHA"], readerCommand["NOME"]);
                }
            }
            else
            {
                Console.WriteLine("Ops! Ainda não existe nenhum dado no Banco de dados...");
            }
            conexao.Close();

            /* -------------------------------------- */


            /* ---------- UPDATE COM O SQL ---------- */

            Console.WriteLine();
            Console.Write("Digite [S] para a Alteração de um dado: ");
            string quiz = Console.ReadLine().ToUpper();
            Console.WriteLine();

            conexao.Open();

            if (quiz == "S")
            {
                Console.WriteLine(" ---------- Demonstração Update ---------- ");
                Console.WriteLine();
                Console.Write("Cofirme o ID do usuário a alterar: ");
                int id = int.Parse(Console.ReadLine());

                querrySQL = "SELECT * FROM USERS WHERE ID ='" + id + "'";
                command = new SqlCommand(querrySQL, conexao);
                readerCommand = command.ExecuteReader();

                if (readerCommand.HasRows)
                {
                    conexao.Close();

                    Console.Write("Informe o novo E-mail: ");
                    string email = Console.ReadLine();
                    Console.Write("Informe a nova Senha: ");
                    string senha = Console.ReadLine();
                    Console.Write("Informe o Nome: ");
                    string nome = Console.ReadLine();

                    conexao.Open();
                    string querrySQLUPDATE = "UPDATE USERS SET EMAIL = '" + email + "', SENHA = '" + senha + "', NOME = '" + nome + "' WHERE ID = '" + id + "'";
                    SqlCommand update = new SqlCommand(querrySQLUPDATE, conexao);
                    update.ExecuteNonQuery();
                    conexao.Close();

                    Console.WriteLine();
                    Console.WriteLine("Dados atualizados com sucesso!");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("ID informado não foi encontrado no Banco de dados.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Vamos prosseguir...");
                Console.WriteLine();
            }
            conexao.Close();

            /* -------------------------------------- */


            /* ---------- DELETE COM O SQL ---------- */

            Console.WriteLine();
            Console.WriteLine(" ---------- Demonstração Delete ---------- ");
            Console.WriteLine();

            Console.Write("Confime o ID do usuário a excluir ou digite '0' para pular: ");
            int quizNumber = int.Parse(Console.ReadLine());

            conexao.Open();
            querrySQL = "SELECT * FROM USERS WHERE ID = '" + quizNumber + "'";
            command = new SqlCommand(querrySQL, conexao);
            readerCommand = command.ExecuteReader();
            
            if (quizNumber > 0 && readerCommand.HasRows)
            {
                conexao.Close();

                conexao.Open();
                string stringSQLDELETE = "DELETE FROM USERS WHERE ID ='" + quizNumber + "'";
                SqlCommand delete = new SqlCommand(stringSQLDELETE, conexao);
                delete.ExecuteNonQuery();
                Console.WriteLine($"Usuário {quizNumber} excluído da Tabela.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Ops, o ID informado não existe no banco de dados");
                Console.WriteLine();
            }
            conexao.Close();

            /* -------------------------------------- */


            /* ---------- INSERT COM O SQL ---------- */

            Console.WriteLine(" ---------- Demonstração Insert ---------- ");
            Console.WriteLine();

            Console.Write("Confime se deseja adicionar um novo usuário a tabela [S/N]: ");
            string quizInsert = Console.ReadLine().ToUpper();


            if (quizInsert == "S")
            {

                Console.Write("Informe o novo E-mail: ");
                string email = Console.ReadLine();
                Console.Write("Informe a nova Senha: ");
                string senha = Console.ReadLine();
                Console.Write("Informe o Nome: ");
                string nome = Console.ReadLine();

                conexao.Open();
                string querrySQLINSERT = "INSERT INTO USERS(EMAIL, SENHA, NOME) values('" + email + "', '" + senha + "', '" + nome + "')";
                SqlCommand insert = new SqlCommand(querrySQLINSERT, conexao);
                insert.ExecuteNonQuery();
                conexao.Close();
                Console.WriteLine();

                Console.WriteLine($"Usuário {nome} adicionado.");
            }
        }
    }
}


