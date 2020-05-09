using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Config
{
    public interface IWatermarkConfig
    {
        WatermarkType WatermarkType{get;set;}
        string WatermarkText{get;set;}
        string TextFont{get;set;}
        int TextSize{get;set;}
        string TextColor{get;set;}
        string WatermarkImage{get;set;}
        int WatermarkImageTransparency{get;set;}
        DatumMark DatumMark        {get;set;     }
        int Margin1{get;set;}
        int Margin2{get;set;}
    }
}
