using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRUDApi2
{
    public class Livro
    {
        [Key]
        public int LivroId { get; set; }
        public string LivroNome { get; set; } = string.Empty;

        public int Ano { get; set; }
        public string Autor { get; set; } = string.Empty;

        public int? AreaId { get; set; }

        public string AreaNome { get; set; } = string.Empty;

        public string Imagem { get; set; } = string.Empty;

        [JsonIgnore]
        public AreaDeConhecimento? AreaDeConhecimento { get; set; }

        public static List<Livro> ObterLivrosPorAreaDeConhecimento(int areaDeConhecimentoId, List<Livro> todosLivros)
        {
            return todosLivros.Where(livro => livro.AreaId == areaDeConhecimentoId).ToList();
        }

    }

    public class AreaDeConhecimento
    {
        [Key]
        public int AreaId { get; set; }
        public string AreaNome { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Livro> Livros { get; set; } = new List<Livro>();
    }

    public class Autor
    {
        [Key]
        public int AutorId { get; set; }
        public string AutorNome { get; set; } = string.Empty;

        [JsonIgnore]
        private List<Livro> _livros = new List<Livro>();

        public List<Livro> Livros
        {
            get { return _livros; }
            set { _livros = value; }
        }

        // Propriedade calculada para obter o número de livros para um autor
        public int NumeroLivros
        {
            get { return Livros?.Count ?? 0; }
        }


    }

}
