namespace PCC.Api.DTOs
{
    public class PatchRequestPayload
    {
        public string op { get; set; }
        public string path { get; set; }
        public int value { get; set; }

        public PatchRequestPayload(string op, string path, int value)
        {
            this.op = op;
            this.path = path;
            this.value = value;
        }
    }
}