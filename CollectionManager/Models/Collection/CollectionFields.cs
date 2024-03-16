using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Models.Collection
{
    [Keyless]
    public class CollectionFields
    {
        public List<string> intFieldNames { get; set; }
        public List<string> stringFieldNames { get; set; }
        public List<string> boolFieldNames { get; set; }
        public List<string> multilineStringFieldNames { get; set; }
        public List<string> dateFieldNames { get; set; }
        public CollectionFields()
        {
            intFieldNames = new List<string>();
            stringFieldNames = new List<string>();
            dateFieldNames = new List<string>();
            boolFieldNames = new List<string>();
            multilineStringFieldNames = new List<string>();
        }
    }
}
