using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apel.Helpers
{
    

    public class Helpers
    {

        public static string hariHelpers(int hariIndex)
        {
            Dictionary<int, string> hariDict = new Dictionary<int, string>()
            {
                { 1, "Senin" },
                { 2, "Selasa" },
                { 3, "Rabu" },
                { 4, "Kamis"},
                { 5, "Jumat"},
                { 6, "Sabtu"},
                { 0, "Minggu"}
            };
            return hariDict[hariIndex];
        }

        public static string bulanHelpers(int monthIndex)
        {
            Dictionary<int, string> monthDict = new Dictionary<int, string>()
            {
                { 1, "Januari" },
                { 2, "Februari" },
                { 3, "Maret" },
                { 4, "April"},
                { 5, "Mei"},
                { 6, "Juni"},
                { 7, "Juli"},
                { 8, "Agustus"},
                { 9, "September"},
                { 10, "Oktober"},
                { 11, "November"},
                { 12, "Desember"}
            };
            return monthDict[monthIndex];
        }
    }





    

    


    
}
