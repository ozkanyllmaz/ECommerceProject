using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Domain.Entities.Enums
{
    public enum OrderStatus
    {
        SiparisAlindi = 1,
        Hazırlanıyor = 2,
        Kargoda = 3,
        TeslimEdildi = 4,
        İptalEdildi = 5,
        İadeEdildi = 6
    }
}
