
using System.Data.SqlClient;
using System.Threading.Channels;
using Microsoft.Data.SqlClient;
using TodoList.Models;

class Program
{
    private static string connectionString = "SUA STRING DE CONEXÃO";
    static void Main(string[] args)
    {
        
        bool continuarRodando = true;

        while(continuarRodando) {
            Console.WriteLine("-----TodoList-----");
            Console.WriteLine("Deseja adicionar, remover ou listar as tarefas?");
            Console.WriteLine("1)Adicionar \n2)Remover \n3)Listar");
            Console.Write("Digite Sua resposta: ");
            int resposta = int.Parse(Console.ReadLine());


            switch(resposta) {
                case 1:
                    Console.Clear();
                    AdicionarTarefa();
                    continuarRodando = VerificarSeContinuaRodando();
                    break;

                case 2:
                    Console.Clear();
                    RemoverTarefa();
                    continuarRodando = VerificarSeContinuaRodando();
                    break;

                case 3:
                    Console.Clear();
                    GetTarefas();
                    continuarRodando = VerificarSeContinuaRodando();
                    break;
                default:
                    Console.WriteLine("Digite uma opção válida");
                    break;

            }
        }


    }

    public static bool VerificarSeContinuaRodando() {
        Thread.Sleep(1000);
        Console.Clear();
        Console.WriteLine("Deseja parar o programa ou rodar novamente? \ndigite 1 para parar");
        int escolha = int.Parse(Console.ReadLine());
        if(escolha == 1) {
            return false;
        }
        return true;
    
    }

    private static void GetTarefas() {
        List<Tasks> tarefas = new List<Tasks>();
        string query = "SELECT t.id, t.nome,t.descricao, t.tempoEstimado FROM Tasks t";

        using(SqlConnection con = new SqlConnection(connectionString)) {
            con.Open();
            var cmd = new SqlCommand(query,con);

            using(var reader = cmd.ExecuteReader()) {

                while(reader.Read()) {
                    tarefas.Add(new Tasks {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        EstimatedTime = reader.GetString(3),


                    });


                }
                foreach(var task in tarefas) {
                    Console.WriteLine($"Id tarefa: {task.Id} \nNome da tarefa: {task.Name} \nDescrição {task.Description} \nTempo estimado: {task.EstimatedTime}");
                    Console.WriteLine("----------------------------------");
                }

            }
        }
    }

    public static void RemoverTarefa() {
        

            GetTarefas();
            Console.WriteLine("Qual tarefa deseja remover?");
            int escolha = int.Parse(Console.ReadLine());

            string query = "DELETE FROM Tasks WHERE id = @Id";
            using(SqlConnection con = new SqlConnection(connectionString)) {

            con.Open();

            using(SqlCommand cmd = new SqlCommand(query,con)) {

                cmd.Parameters.AddWithValue("@Id",escolha);

                int rowsAffected = cmd.ExecuteNonQuery();

                if(rowsAffected > 0) {
                    Console.WriteLine("Tarefa removida com sucesso");
                }
            }
        }
        
        
    }




    public static void AdicionarTarefa() {
        string Read(string prompt) {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        (string nomeTarefa, string descricaoTarefa, string tempoEstimado) tuple = (
            nomeTarefa: Read("Qual tarefa deseja Adicionar?"),
            descricaoTarefa: Read("Digite a descrição da tarefa"),
            tempoEstimado: Read("Qual o tempo estimado para a tarefa?")
            );

        
        string query = "INSERT INTO Tasks (Nome, Descricao, TempoEstimado) VALUES (@Nome, @Descricao, @TempoEstimado)";

        using(SqlConnection connection = new SqlConnection(connectionString)) {

            try {
                connection.Open();

                using(SqlCommand command = new SqlCommand(query , connection)) {

                    command.Parameters.AddWithValue("@Nome", tuple.nomeTarefa);
                    command.Parameters.AddWithValue("@Descricao",tuple.descricaoTarefa);
                    command.Parameters.AddWithValue("@TempoEstimado",tuple.tempoEstimado);

                    int isRowAffected = command.ExecuteNonQuery();

                    Console.WriteLine(isRowAffected > 0
                        ? "Dados inseridos"
                        : "Falha ao inserir os dados ");
                }

                
            }catch(Exception ex) {
                Console.WriteLine("Error: " + ex);
            }
              
        }
    }



}
