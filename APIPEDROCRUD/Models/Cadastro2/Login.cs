using System;
using System.Collections.Generic;

namespace APIPEDROCRUD.Models;

public partial class Login
{
    public int Id { get; }

    public string Name { get; set; } = null!;

    public string User { get; set; } = null!;

    public string Password { get; set; } = null!;

    internal DateTime LastChange { get; set; }

}
