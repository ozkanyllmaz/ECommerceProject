using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.DTOs.Common
{
    // struct çünkü içi boş bir struct belleğin Heap bölgesinde yer kaplamaz ve 
    // Çöp toplayıcıyı (Garbage Collector) yormaz
    public struct NoContentDto
    {
    }
}
