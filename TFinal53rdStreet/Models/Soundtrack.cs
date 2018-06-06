using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TFinal53rdStreet.Models
{
    public class Soundtrack
    {
        [Key]
        public int ID_Song { get; set; }
        [Required(ErrorMessage = " {0} required field!")]
        [RegularExpression("[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+(( |'|-| dos | da | de | e | d')[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+){1,3}",
        ErrorMessage = "O {0} it can only contain letters of blank spaces. Each word has to begin with a capital letter followed by a lower case letters...")]

        public string SongName { get; set; }
        public string Duration { get; set; }

        //atributo, da tabela Musical, que será referênciado na tabela Soundtrack
        [ForeignKey("Musical")]
        public int MusicalFK { get; set; }
        //chama, neste caso, o musical da tabela Musical
        public virtual Musical Musical { get; set; }

    }
}