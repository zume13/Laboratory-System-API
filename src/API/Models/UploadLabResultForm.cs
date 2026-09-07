using Microsoft.AspNetCore.Http;

namespace Laboratory_Management_API.Models
{
    public class UploadLabResultForm
    {
        public string SampleId { get; set; } = default!;
        public string SubFolder { get; set; } = default!;
        public IFormFile File { get; set; } = default!;
    }
}