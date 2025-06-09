using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEL.Services.Utility
{
    public class DeleteFileStream : FileStream
    {
        readonly string path;

        public DeleteFileStream(string path, FileMode mode) : base(path, mode) // NOTE: must create all the constructors needed first
        {
            this.path = path;
        }

        protected override void Dispose(bool disposing) // NOTE: override the Dispose() method to delete the file after all is said and done
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
        }
    }
}
