using Microsoft.AspNetCore.Mvc;
using ServiceFromPrivateTemplate.APIs.Common;
using ServiceFromPrivateTemplate.Infrastructure.Models;

namespace ServiceFromPrivateTemplate.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class FffFindManyArgs : FindManyInput<Fff, FffWhereInput> { }
