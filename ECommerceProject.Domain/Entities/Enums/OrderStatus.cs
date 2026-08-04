using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Domain.Entities.Enums
{
    public enum OrderStatus
    {
        SiparisAlindi = 1,
        Onaylandı = 2,
        Tamamlandı = 3,
        İptalEdildi = 4,
        İadeEdildi = 5,
        Reddedildi = 6
    }
}
