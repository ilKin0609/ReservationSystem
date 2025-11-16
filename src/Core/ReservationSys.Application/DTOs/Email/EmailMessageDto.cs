using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSys.Application.DTOs.Email;

public record EmailMessageDto
{
    public List<string> To { get; set; } = new();
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
}
