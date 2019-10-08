using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Xamarin_Test
{
    [Table("PackageDetail")]
    public class PackageTable
    {
        [PrimaryKey,AutoIncrement,Column("Id")]
        public int Id { get; set; }
        [MaxLength(100)]
        public string BarCode { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Depth { get; set; }
    }
}