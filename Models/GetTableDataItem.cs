using System;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Memoryleek.FunctionCSharpUtils.Models
{
    public class GetTableDataItem: TableEntity
    {
        public string Content { get; set; }
    }
}
