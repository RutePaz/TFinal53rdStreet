using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TFinal53rdStreet.Models
{
    public class Cast
    {
        public Cast()
        {
            ListMusical = new HashSet<CastMusical>();

        }

        [Key]
        public int ID_Actor { get; set; }
        [Required(ErrorMessage = " {0} required field!")]
        [RegularExpression("[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+(( |'|-| dos | da | de | e | d')[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+){1,3}",
        ErrorMessage = "O {0} it can only contain letters of blank spaces. Each word has to begin with a capital letter followed by a lower case letters...")]
        public string Name { get; set; }
        public string Image { get; set; }



        public virtual ICollection<CastMusical> ListMusical { get; set; }



        //atributo, da tabela Musical, que será referênciado na tabela Cast
        //[ForeignKey("Musical")]
        //public int MusicalFK { get; set; }
        //chama, neste caso, o musical da tabela Musical
        //public virtual Musical Musical { get; set; }

    }
}