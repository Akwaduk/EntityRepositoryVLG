namespace BlazingScaffolds.Gates
{
    public class GateAccessType
    {
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanDelete { get; set; }

        public GateAccessType(bool canRead, bool canWrite, bool canDelete)
        {        
            CanRead = canRead;
            CanWrite = canWrite;
            CanDelete = canDelete;
        }
    }
}
