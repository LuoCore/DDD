using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.ViewModels.Layui
{
    public class SelectViewModel
    {
        public string value { get; set; }
        public string Name { get; set; }
        public bool disabled { get; set; }
        public List<SelectViewModel> children { get; set; }
    }
}
