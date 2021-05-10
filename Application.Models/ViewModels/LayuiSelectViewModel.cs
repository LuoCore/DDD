using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.ViewModels
{
    public class LayuiSelectViewModel
    {
        public string value { get; set; }
        public string Name { get; set; }
        public bool disabled { get; set; }
        public List<LayuiSelectViewModel> children { get; set; }
    }
}
