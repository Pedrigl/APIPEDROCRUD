using System;
using System.Collections.Generic;

namespace APIPEDROCRUD.Models;

public partial class Cliente
{
    public int ID { get; }

    public string Nome { get; set; }

    public string Endereco { get; set; }

    public string Cep { get; set; }

    public string Bairro { get; set; }

    public string Cidade { get; set; }

    public string Uf { get; set; }

    public string Telefone { get; set; }
}
