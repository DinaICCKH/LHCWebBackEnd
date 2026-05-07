namespace DMSWebPortal.Models
{
    public class ApiResponse
    {
        public int Code { get; set; }      // e.g. 200, 400, 500
        public string Status { get; set; } // e.g. "Success", "Error"
        public string Message { get; set; } // custom message
        public List<Set_Object> Set_Obj { get; set; }
    }
    public class Set_Object
    {
        public string ObjType { get; set; }
        public string ObjCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
