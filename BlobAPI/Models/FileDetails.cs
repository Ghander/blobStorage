using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlobAPI.Models {
    public class FileDetails {
        public string Name { get; set; }
        public long Size { get; set; }
        public string ContentType { get; set; }
        public string Location { get; set; }            
    }
}