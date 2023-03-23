#Introdução
API com C# e SQL Server feita para estudos e portfólio feita com .NET 6 e entity framework.

#Informações importantes
O Controlador Login, modela dados da tabela Usuarios, a qual é composta pelo seguinte esquema
{
"Id": int,
"Name": "string",
"User": "string",
"Password": "string",
"LastChange": datetime
}
as senhas são criptografadas usando hash e o User é único, não sendo possível dois registros compartilharem do mesmo.

O controlador Clientes, modela a tabela CLIENTE, a qual é composta pelo seguinte esquema
{
"id": int
"nome": "string",
"endereco": "string",
"cep": "string",
"bairro": "string",
"cidade": "string",
"uf": "string",
"telefone": "string"
}
a uf corresponde a sigla do estado do cliente e só pode conter dois caracteres, e o cep só pode ter 8 caracteres.

#Códigos de erros
Caso o registro com a desejada ID não seja encontrado nos métodos put, háverá retornos 404 Not Found, nos métodos delete, se ouver problemas em deletar o registro desejado, o retorno será 500.
Do contrário todos os retornos são ou 200 OK ou 400 Bad Request

#Teste
Esse projeto está disponível para testes em: https://apipedro.azurewebsites.net/swagger/index.html

