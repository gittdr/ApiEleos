using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEleos.Models
{
    public class FacLabControler
    {
        public ModelFact modelFact = new ModelFact();
        public void enviarNotificacion(string leg, string titulo, string mensaje)
        {
            this.modelFact.enviarNotificacion(leg, titulo, mensaje);
        }
        public void registrarEvidencias(string segmento, string obtDocs, string filenamef)
        {
            this.modelFact.registrarEvidencias(segmento, obtDocs, filenamef);
        }
        public void registrarEvidenciasConErrores(int identificador, string downloadI)
        {
            this.modelFact.registrarEvidenciasConErrores(identificador, downloadI);
        }
    }
}
