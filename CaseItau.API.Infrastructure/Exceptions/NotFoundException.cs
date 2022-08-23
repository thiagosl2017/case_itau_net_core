using CaseItau.API.Infrastructure.Menssage;
using System;

namespace CaseItau.API.Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg) : base(msg) { }
        public NotFoundException() : base(ErroMenssage.NotFound) { }
    }
}
