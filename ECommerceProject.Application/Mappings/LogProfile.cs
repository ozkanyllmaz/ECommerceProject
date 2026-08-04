using AutoMapper;
using ECommerceProject.Application.Features.LogDb.Queries.AuditLogs;
using ECommerceProject.Application.Features.LogDb.Queries.ExceptionLogs;
using ECommerceProject.Application.Features.LogDb.Queries.RequestLogs;
using ECommerceProject.Domain.Entities.LogEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Mappings
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<ExceptionLog, ExceptionLogsQueryResponse>();
            CreateMap<RequestLog, RequestLogsQueryResponse>();
            CreateMap<AuditLog, AuditLogsQueryResponse>();
        }
    }
}
