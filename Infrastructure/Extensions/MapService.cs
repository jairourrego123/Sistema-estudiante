using Application.Dtos.AuthDtos;
using Infrastructure.DataSource.Configuracion.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions;

public class MapService
{
    public static UsuarioDto MapUsuarioIdentityToUsuarioDto(UsuarioIdentity userIdentity)
    {
        return new UsuarioDto
        {
            Id = userIdentity.Id,
            Nombre = userIdentity.Nombre,
            Apellido = userIdentity.Apellido,
            Email = userIdentity.Email!
        };
    }

    public static UsuarioIdentity MapRegistroUsuarioDtoToUsuarioIdentity(RegistroDto registroDto)
    {
        return new UsuarioIdentity
        {
            Nombre = registroDto.Nombre,
            Apellido = registroDto.Apellido,
            Email = registroDto.Email,
            UserName = registroDto.Email
        };
    }
}
