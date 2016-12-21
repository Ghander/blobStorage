using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BlobAPI.Models;
using BlobAPI.Providers;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using static BlobAPI.Providers.ResourceProvider.BlobHelper;

namespace BlobAPI.Controllers {
    public class FilesController : ApiController {
        public CloudBlobContainer container = GetWebApiContainer();

        // GET api/<controller>
        public IEnumerable<FileDetails> Get() {
            foreach (CloudBlockBlob blob in this.container.ListBlobs())
                yield return new FileDetails
                {
                    Name = blob.Name,
                    Size = blob.Properties.Length,
                    ContentType = blob.Properties.ContentType,
                    Location = blob.Uri.AbsoluteUri
                };
        }

        // GET api/<controller>/5
        public string Get(int id) {
            return "value";
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var file in provider.Contents)
                {
                    var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                    var buffer = await file.ReadAsByteArrayAsync();
                    //Do whatever you want with filename and its binaray data.
                    var blob = this.container.GetBlockBlobReference(filename);
                    blob.UploadFromByteArray(buffer, 0, buffer.Length - 1);
                }

                return Ok();
            }

            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/<controller>/5
        public void Delete(int id) {
        }
    }
}