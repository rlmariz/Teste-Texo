# Texo.Catalog.Api
> Api de Teste para processo seletivo Texo.

Projeto Azure:
https://dev.azure.com/rlmariz/Teste-Texo

Repositório Git:
https://dev.azure.com/rlmariz/_git/Teste-Texo

Clone Git:
https://rlmariz@dev.azure.com/rlmariz/Teste-Texo/_git/Teste-Texo


## Parametrização

A porta padrão está configurada como 5000.

A base de dados para ser carregada pode ser configurada no arquivo appsettings.json.

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "DataBase": "movielist.csv"
}
```

Está sendo considerado como serapação de produtores os caracteres "," e "and".

## Requisições Get
| Endereço                                  |  Descrição                |
| ------                                    | ------                    |
| http://localhost:5000/movie               | Retorna lista de todos os indicados.|
| http://localhost:5000/movie/10            | Retorna uma indicação pelo código, sendo o código a sequencia no arquivo que foi carregado.|
| http://localhost:5000/movie/winners       | Retorna lista de todos os vencedores.|
| http://localhost:5000/movie/wins          | Todos produtores vencedores com lista de anos que foram vencedores, menor e maior intervalo que foi venvedor.|
| http://localhost:5000/movie/winsstatistic | Lista produtor com maior intervalo entre dois prêmios consecutivos, e o que obteve dois prêmios mais rápido.|

## Testes de Integração

Os testes de integração foram realizados com base de dados com cenários conhecido(movietest.csv), sendo:

* 20 - Indicações
* 12 - Vencedores

Resultado Esperado
```
{
    "Min": [
        {
            "Producer": "Producer 7",
            "Interval": 1,
            "PreviousWin": 2020,
            "FollowingWin": 2021
        },
        {
            "Producer": "Producer 8",
            "Interval": 1,
            "PreviousWin": 1980,
            "FollowingWin": 1981
        },
        {
            "Producer": "Producer 9",
            "Interval": 1,
            "PreviousWin": 1980,
            "FollowingWin": 1981
        }
    ],
    "Max": [
        {
            "Producer": "Producer 1",
            "Interval": 15,
            "PreviousWin": 2003,
            "FollowingWin": 2018
        },
        {
            "Producer": "Producer 2",
            "Interval": 15,
            "PreviousWin": 1988,
            "FollowingWin": 2003
        }
    ]
} 
```

## Postman
Existe um arquivo com requisições postman Texo.Postman_Collection.json.