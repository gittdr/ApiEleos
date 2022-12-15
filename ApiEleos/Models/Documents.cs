using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEleos.Models
{
    public class Documents
    {
        public string upload_finished_at { get; set; }
        public string sent_to_email { get; set; }
        public string scanned_by_username { get; set; }
        public string scanned_by_email { get; set; }
        public string scanned_by { get; set; }
        public int number_of_pages { get; set; }
        public int load_number { get; set; }
        public int document_identifier { get; set; }
        public string download_url { get; set; }
    }
}
