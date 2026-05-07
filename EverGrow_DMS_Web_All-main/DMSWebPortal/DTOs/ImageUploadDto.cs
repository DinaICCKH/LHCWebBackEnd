namespace DMSWebPortal.Models
{
    public class ImageUploadDto
    {
        public string ObjType { get; set; }     // 'order' or 'reasonorder'
        public string ObjValue { get; set; }    // AppId (as string)
        public string SalesCode { get; set; }   // For reasonorder
        public IFormFile File { get; set; }
    }
}
