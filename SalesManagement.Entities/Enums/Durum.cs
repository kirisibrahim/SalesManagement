using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Entities.Enums
{
    public enum Durum
    {
        Pending,    // Beklemede
        InProgress, // Devam ediyor
        Completed,  // Tamamlandı
        Cancelled   // İptal edildi
    }
}
