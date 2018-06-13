using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TFinal53rdStreet.Models;

namespace TFinal53rdStreet.Controllers
{
    public class MusicalsController : Controller
    {
        //cria uma variavel que representa a Base de Daos

        private MusicalDB db = new MusicalDB();

        // GET: Musicals
        /// <summary>
        /// lista todos os musicais 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //db.Musicals.ToList()->em sql: Select * from Agentes
            //enviar para a View uma lista com todos os Agentes
            var listOfMusicals = db.Musical.ToList();
            return View(listOfMusicals);
            //return View();
        }

        // GET: Musicals/Details/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            //se se escrever 'int?' é possível 
            //não fornecer o valor para o ID e não há erro

            //para caso nao ter sido fornecido o ID
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            //procura na BD, o Musical cujo ID foi fornecido 
            Musical musical = db.Musical.Find(id);

            //proteção para o caso de não ter sido encontrado qq Musical que tenha o ID fornecido
            if (musical == null)
            {
                //o musical não foi encontrado, logo gera-se um erro
                return RedirectToAction("Index");
            }
            //entrega á View os dados do Agente encontrado
            return View(musical);
        }

        // GET: Musicals/Create
        public ActionResult Create()
        {
            //apresneta a View para se inserir um novo Agente 
            return View();
        }

        // POST: Musicals/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //anotador para proteção por roubo de indentidade 
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Musical,Title,Synopsis,Director,Duration,OpeningNight,Ticket")] Musical musical, HttpPostedFileBase uploadPoster)
        {
            //escrever os dados de um novo musical na BD

            //especificar o ID do novo Musical 
            //testar se há registos na tabela Musical
            int idNewMusical = 0;
            try
            {
                idNewMusical = db.Musical.Max(m => m.ID_Musical) + 1;
            }
            catch (Exception)
            {
                idNewMusical = 1;
            }

            //guardar o ID do novo musical 
            musical.ID_Musical = idNewMusical;

            //especificar (escolher) o nome do ficheiro
            string imageName = "Musical_" + idNewMusical + ".jpg";

            //variável auxiliar
            string path = "";

            //validar se a imagem foi fornecida 
            if (uploadPoster != null)
            {
                //valida de o que foi fornecido é uma imagem 
                //****************
                //criar i caminho completo até ao sitio onde o ficheiro será guardado
                path = Path.Combine(Server.MapPath("~/images/"), imageName);
                //guardar o nome do ficheiro na BD 
                musical.Poster = imageName;
            }
            else
            {
                //quando não foi fornecida nenhuma imagem, gera um erro
                ModelState.AddModelError("", "Plese, upload an image for the Musical");
                //devolver o controlo á View 
                return View(musical);
            }
            //ModelState.IsValid : confronta os dados fornecidos d View com as exigências do Modelo
            if (ModelState.IsValid)
            {
                try
                {
                    //adiciona um novo musical
                    db.Musical.Add(musical);
                    //faz commit ás alterações
                    db.SaveChanges();
                    //escrever o ficheiro com a fotografia no disco rígido, na pasta 'images'
                    uploadPoster.SaveAs(path);

                    //Redirecionamento para a página de Index
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "An Error occurred with the addition of the new Musical");
                }

            }
            //se houver um erro, reapresenta os dados do Agente na View
            return View(musical);
        }

        // GET: Musicals/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            //procura na BD, o Musical cujo ID foi fornecido 
            Musical musical = db.Musical.Find(id);

            //proteção para o caso de não ter sido encontrado
            if (musical == null)
            {
                //o musical não foi encontrado, gerando uma mensagem de erro
                ModelState.AddModelError("", "Musical not found");

                //redireciona para o Index
                return RedirectToAction("Index");
            }

            //entrega á View os dados do Musical encontrado
            return View(musical);
        }

        // POST: Musicals/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="musical"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Musical,Title,Synopsis,Director,Duration,OpeningNight,Ticket,Poster")] Musical musical, HttpPostedFileBase uploadPoster)
        {
            if (ModelState.IsValid)
            {
                //neste cado já existe um Agente 
                //apenas quero EDITAR os seus dados
                db.Entry(musical).State = EntityState.Modified;
                //efetuar 'Commit'
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(musical);
        }

        // GET: Musicals/Delete/5
        /// <summary>
        /// apresenta na View os dados de um Musical, co  vista á sua, eventual, eliminação
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            //verificar se foi fornecido um ID válido
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            //pesquisar pelo Musical, cuji ID foi fornecido
            Musical musical = db.Musical.Find(id);
            //verificar se o Musical foi encontrado 
            if (musical == null)
            {
                //o Musical não existe redirecionar para a página inicial
                return RedirectToAction("Index");
            }
            return View(musical);
        }

        // POST: Musicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Musical musical = db.Musical.Find(id);
            try
            {
                //remove o Agente da BD
                db.Musical.Remove(musical);
                //Commit
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("It's not possible to remove the Musical nº {0}-{1}", id, musical.Title));
            }
            return View(musical);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);

        }
    }
}
